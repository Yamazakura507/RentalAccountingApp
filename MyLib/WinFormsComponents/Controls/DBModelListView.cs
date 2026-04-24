using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Enums;
using WinFormsComponents.Classes.Interface;
using WinFormsComponents.Classes.Model;
using WinFormsComponents.Classes.Services;

namespace WinFormsComponents.Controls
{
    public partial class DBModelListView : UserControl
    {
        private Type modelType;
        private readonly IFilter searchFilter;
        private readonly IListViewLoader listViewLoader;
        private string parametrRemovingName = null;
        private Loader loader = new();

        /// <summary>
        /// Колекция элементов списка
        /// </summary>
        private BindingList<object> Items { get; set; }

        /// <summary>
        /// Модель БД представление по которому будет получен список
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Type ModelType
        {
            get => modelType;
            set
            {
                if (modelType != value)
                {
                    modelType = value;
                    this.Enabled = modelType is not null;
                }
            }
        }

        /// <summary>
        /// Набор параметров: фильтрации, сортировки, ограничений вывода
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CollectionParametrs Parameters { get; set; }

        /// <summary>
        /// Набор изображений для списка
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageList ImageList { get; set; }

        /// <summary>
        /// Указатель отображения удаленных записей
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ShowRemooving ShowDeleted { get; set; } = ShowRemooving.ExecNotRemoving;

        /// <summary>
        /// Указатель отображения списка
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public VisibleMode VisibleMode { get; set; } = VisibleMode.Row;

        /// <summary>
        /// Цвет отключенного фильтра
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FilterOffColor { get; set; } = Color.MistyRose;

        /// <summary>
        /// Цвет удаленных строк
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RemovingRowColor { get; set; } = Color.MistyRose;

        /// <summary>
        /// Цвет включенного фильтра
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FilterOnColor { get; set; } = Color.LightGreen;

        /// <summary>
        /// Включить фильтр
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsFilter { get; set; } = true;

        /// <summary>
        /// Включить поиск
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsSearch { get; set; } = true;

        /// <summary>
        /// Включить cетку
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsGridLines { get; set; } = true;

        /// <summary>
        /// Отображение общего количества строк
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsShowCountAll { get; set; } = true;

        /// <summary>
        /// Отображение количества выделеных строк
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsShowCountEnter { get; set; } = true;

        /// <summary>
        /// Включить постраничный вывод
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int PageLimit { get; set; } = 0;

        /// <summary>
        /// Событие при добавлении
        /// </summary>
        public event EventHandler<Action<object>> InsertChanged;

        /// <summary>
        /// Событие при обновлении
        /// </summary>
        public event EventHandler<object, Action<object>> UpdateChanged;

        /// <summary>
        /// Настройка взаимодействия с БД(Удаление/Оновление)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        private TermsOfInteractionDB TermsOfInteractionDB { get; set; }

        public DBModelListView()
        {
            InitializeComponent();

            searchFilter = new SearhFilterLoader(FilterOffColor, FilterOnColor);
            listViewLoader = new ListViewLoader(RemovingRowColor);
            Parameters ??= new();
            TermsOfInteractionDB ??= new();
            tsmiPager.DropDown.Closing += PagerDropDownOnClosing;
            loader.AutoSetup(this);
        }

        /// <summary>
        /// Подгрузка стартовой информации
        /// </summary>
        private async Task LoadInfo()
        {
            lvModel.GroupImageList = lvModel.StateImageList = lvModel.LargeImageList = lvModel.SmallImageList = ImageList;
            parametrRemovingName = ModelType.GetProperties().FirstOrDefault(i => i.GetCustomAttribute<ViewModelAttribute>()?.RemovingFlag ?? false)?.Name;
            loader.Visible = true;
            tsmiPagerCheckit.Checked = PageLimit != 0 || Properties.Settings.Default.Limit != 0;
            tsmiAllCountShow.Checked = IsShowCountAll;
            tsmiEnterCountShow.Checked = IsShowCountEnter;

            CreateParametrShowRemoving();
            ShowVisibleMode(VisibleMode);
            ShowGridVisible(IsGridLines);
            CheckSearch();

            if (!await UpdateCountPage(PageLimit == 0 ? Properties.Settings.Default.Limit : PageLimit))
            {
                await LoadListAsync();
            }
        }

        /// <summary>
        /// Загрузка фильтра
        /// </summary>
        private void ColumnLoad()
        {
            Dictionary<string, string> descModel = this.ModelType.GetDescriptionModel();

            lvModel.Columns.Clear();

            lvModel.Columns.AddRange(
                descModel.Select(i =>
                    new ColumnHeader()
                    {
                        Name = i.Key,
                        Text = i.Value
                    }).ToArray());

            if (IsSearch || IsFilter) FilterLoad();
        }

        /// <summary>
        /// Загрузка фильтра
        /// </summary>
        private void FilterLoad()
        {
            tsmiSearh.Visible = IsSearch;
            tsmiFilter.Visible = IsFilter;
            tssFilter.Visible = IsSearch || IsFilter;
            tsmiSearh.DropDownItems.Clear();
            tsmiFilter.DropDownItems.Clear();

            IEnumerable<ConditionsParametr> searchParameters = Parameters.Conditions.Where(i => i.IsSerhing);
            IEnumerable<PropertyInfo> properties = ModelType.GetProperties().Where(i => !i.GetCustomAttribute<ViewModelAttribute>()?.ViewHide ?? true);

            foreach (PropertyInfo property in properties)
            {
                string titleMenu = property.GetCustomAttribute<DescriptionAttribute>()?.Description;

                if (titleMenu is not null)
                {
                    if (IsSearch)
                    {
                        ToolStripMenuItem tsmiSearh = searchFilter.CreateFilter(titleMenu, property.Name,
                        searchParameters.FirstOrDefault(i => i.ColumnName.Equals(property.Name)),
                        UpdateSearhParametrs);

                        this.tsmiSearh.DropDownItems.Add(tsmiSearh);
                    }

                    if (IsFilter && (property.GetCustomAttribute<ViewModelAttribute>()?.FilterOn ?? false))
                    {
                        ToolStripMenuItem tsmiFilter = new(titleMenu, Properties.Resources.filter);

                        this.tsmiFilter.DropDownItems.Add(tsmiFilter);
                    }
                }
            }

            tsmiSearh.Enabled = tsmiSearh.DropDownItems.Count > 0;
            tsmiFilter.Enabled = tsmiFilter.DropDownItems.Count > 0;
        }

        /// <summary>
        /// Проверка наличия поисковых условий
        /// </summary>
        private void CheckSearch()
        {
            tstbSearh.Visible = tsbSearh.Visible = IsSearch && Parameters.SerhingParametrsCount != 0;
        }

        /// <summary>
        /// Метод обновления статуса формата отображения удаленных записей
        /// </summary>
        /// <param name="showRemooving">Статус формата отображения удаленных записей</param>
        private async Task ShowRemoving(ShowRemooving showRemooving)
        {
            ShowDeleted = showRemooving;
            Parameters.Conditions -= Parameters.Conditions.FirstOrDefault(i => i.ColumnName.Equals(parametrRemovingName));
            CreateParametrShowRemoving();

            if (Parameters.Limit == 0) await LoadListAsync();
            else await UpdateCountPage(Parameters.Limit);

        }

        /// <summary>
        /// Метод обновления статуса формата отображения списка
        /// </summary>
        /// <param name="visibleMode">Статус формата отображения списка</param>
        private async void ShowVisibleMode(VisibleMode visibleMode)
        {
            VisibleMode = visibleMode;

            if (VisibleMode == VisibleMode.Tile)
            {
                lvModel.View = View.Tile;
                tsbGrid.Visible = tsbNonGrid.Visible = tsbTileMode.Visible = false;
                tsbRowMode.Visible = true;
            }
            else
            {
                lvModel.View = View.Details;
                tsbTileMode.Visible = true;
                tsbRowMode.Visible = false;
                ShowGridVisible(IsGridLines);
            }
        }

        /// <summary>
        /// Метод обновления статуса отображения сетки
        /// </summary>
        /// <param name="isGridLine">Статус отображения сетки</param>
        private async void ShowGridVisible(bool isGridLine)
        {
            IsGridLines = tsbNonGrid.Visible = isGridLine;
            tsbGrid.Visible = !tsbNonGrid.Visible;
            lvModel.GridLines = IsGridLines;
        }

        /// <summary>
        /// Создание параметра отображения удаленных значений
        /// </summary>
        private void CreateParametrShowRemoving()
        {
            if (parametrRemovingName is null)
            {
                tsmiShowDeleted.Visible = false;
                tsddbFilter.Visible = IsSearch || IsFilter;
            }

            if (!Parameters.Conditions.Any(i => i.ColumnName.Equals(parametrRemovingName)) && ShowDeleted != ShowRemooving.Always)
            {
                Parameters.Conditions += new ConditionsParametr(parametrRemovingName, ConditionalOperators.Equal, LogicOperators.And, ShowDeleted == ShowRemooving.ExecNotRemoving);
            }
        }

        /// <summary>
        /// Обновление списка параметров в ссответствии поисковому фильтру (срабатывает при закрытии фильтра)
        /// </summary>
        /// <param name="newParametr">Новый параметр</param>
        /// <param name="baseParametr">Текущий параметр</param>
        /// <param name="updateParametr">Делегат вызова события для обновления значения текущего фильтра</param>
        private async void UpdateSearhParametrs(ConditionsParametr newParametr, ConditionsParametr baseParametr, UpdateParametrChangedHandler updateParametr = null)
        {
            Parameters.Conditions -= baseParametr;

            if (newParametr is not null)
            {
                Parameters.Conditions += newParametr;
            }

            if (baseParametr != newParametr)
            {
                updateParametr?.Invoke(newParametr);

                CheckSearch();
                await UpdateCountPage(Parameters.Limit);
            }
        }

        /// <summary>
        /// Загрузка списка элементов
        /// </summary>
        private async Task LoadListAsync()
        {
            loader.StartAnimation();
            lvModel.SelectedItems.Clear();

            foreach (ConditionsParametr condition in Parameters.Conditions.Where(i => i?.IsSerhing ?? false))
            {
                condition.Value = tstbSearh.Text;
            }

            Items = await modelType.GetCollectionByType<object>([Parameters], nameof(DBProvider.GetCollectionModel));

            listViewLoader.PopulateListView(lvModel, modelType, Items);

            if (IsShowCountAll)
            {
                tslAllCount.Text = String.Format("Всего: {0}", await modelType.GetResultByType<int>([Parameters.Conditions], nameof(DBProvider.Count)));
            }

            loader.StopAnimation();
        }

        /// <summary>
        /// Метод обнавления постраничного вывода
        /// </summary>
        /// <param name="limit">Ограничение вывода</param>
        /// <param name="offset">Ограничение пропуска</param>
        /// <returns>Процес</returns>
        private async Task UpdatePager(ActionPager actionPager)
        {
            int count = Convert.ToInt32(tslCountPages.Tag);
            int nowPage = 1;
            Int32.TryParse(tstbActualPage.Text, out nowPage);

            switch (actionPager)
            {
                case ActionPager.Next:
                    nowPage += 1;
                    break;
                case ActionPager.Back:
                    nowPage -= 1;
                    break;
                case ActionPager.Start:
                    nowPage = 1;
                    break;
                case ActionPager.End:
                    nowPage = count;
                    break;
                case ActionPager.Enter:
                    if (nowPage > count) nowPage = count;
                    else if (nowPage < 1) nowPage = 1;
                    break;
            }

            Parameters.Offset = (nowPage - 1) * Parameters.Limit;

            tstbActualPage.Text = nowPage.ToString();
            IsCheckPager(nowPage, count);
            await LoadListAsync();
        }

        /// <summary>
        /// Обновление количества страниц
        /// </summary>
        /// <param name="limit">Ограничитель вывода страницы</param>
        /// <returns>Процес</returns>
        private async Task<bool> UpdateCountPage(int limit)
        {
            bool isLimiter = limit != 0;
            tsbStartPage.Visible = tsbEndPage.Visible
                = tsbNextPage.Visible = tsbBackPage.Visible
                = tslCountPages.Visible = tstbActualPage.Visible = tssPager.Visible = isLimiter;

            Parameters.Limit = PageLimit = limit;

            if (isLimiter)
            {
                int nowPage = Convert.ToInt32(tstbActualPage.Text);
                int count = await modelType.GetCountPage(Parameters.Conditions, limit);

                count = count == 0 ? 1 : count;

                tslCountPages.Text = String.Format("/{0}", count.ToString());
                tslCountPages.ToolTipText = String.Format("Всего {0} страниц", count.ToString());
                tslCountPages.Tag = count;

                if (nowPage > count) await UpdatePager(ActionPager.End);
                else await UpdatePager(ActionPager.Enter);
            }

            return isLimiter;
        }

        /// <summary>
        /// Проверка доступности действий страничника
        /// </summary>
        /// <param name="nowPage">Текущая страница</param>
        /// <param name="count">Количество страниц</param>
        public void IsCheckPager(int nowPage, int count)
        {
            tstbActualPage.Enabled = count > 1;
            tsbNextPage.Enabled = tsbEndPage.Enabled = tstbActualPage.Enabled && nowPage < count;
            tsbBackPage.Enabled = tsbStartPage.Enabled = tstbActualPage.Enabled && nowPage > 1;
        }

        /// <summary>
        /// Обновление поискового запроса
        /// </summary>
        /// <returns>Процес</returns>
        private async Task UpdateSearch()
        {
            if (Parameters.Limit == 0) await LoadListAsync();
            else await UpdateCountPage(Parameters.Limit);
        }

        /// <summary>
        /// Проверка выделеных элементов
        /// </summary>
        /// <returns>Кортеж значений isRemove - доступ к удалению, isRepair - доступ к востановлению</returns>
        private (bool isRemove, bool isRepair) SelectedCheck()
        {
            PropertyInfo property = modelType.GetProperty(parametrRemovingName);
            bool isRemoving = true;
            bool isNotRemoving = true;
            bool isSelect = lvModel.SelectedItems.Count > 0;

            foreach (ListViewItem item in lvModel.SelectedItems)
            {
                bool flag = Convert.ToBoolean(property?.GetValue(item.Tag) ?? true);

                if (isNotRemoving) isNotRemoving = flag;
                if (isRemoving) isRemoving = !flag;
            }

            return (isSelect && isNotRemoving, isSelect && isRemoving);
        }

        /// <summary>
        /// Метод удаления/востановления выделеных строк
        /// </summary>
        /// <returns>Процес</returns>
        private async Task DeleteOrRepair()
        {
            await modelType.InvokeMethodByType([TermsOfInteractionDB.GetDeleteParamers(lvModel.SelectedItems, ModelType)], nameof(DBProvider.Delete))
                .ContinueWith(async task =>
                {
                    if (!task.IsFaulted)
                    {
                        await this.Invoke(async () => await LoadListAsync());
                    }
                }).Unwrap();
        }

        /// <summary>
        /// Проверка на наличие отображаемой информации
        /// </summary>
        private void IsInformationBar() => tsIformationBar.Visible = IsShowCountAll || IsShowCountEnter;

        /// <summary>
        /// Заполнение/Скрытие иформации списка
        /// </summary>
        /// <param name="tsmiInformationShow">Элемент меню отвечающий за доступность информации</param>
        /// <param name="parametrs">Набор параметров для элемента меню</param>
        /// <param name="info">Кортеж с выводимой информацией (информаци, формат вывода, элемент вывода)</param>
        private void InformationChecket(ToolStripMenuItem tsmiInformationShow, Dictionary<bool, (string, string, Color)> parametrs, (int valueInfo, string valueFormat, ToolStripLabel lbInfo) info)
        {
            info.lbInfo.Visible = tsmiInformationShow.Checked;

            if (tsmiInformationShow.Checked)
            {
                info.lbInfo.Text = String.Format(info.valueFormat, info.valueInfo);
            }

            FilterFunction.CheckedChangedItemMenu(tsmiInformationShow, parametrs);

            IsInformationBar();
        }

        private async void DBModelListViewOnLoad(object sender, EventArgs e)
        {
            if (!this.Enabled) return;

            loader.StartAnimation();

            ColumnLoad();
            await LoadInfo().ContinueWith(_ => this.Invoke(() => loader.StopAnimation()));
        }

        private async void tstbSearhOnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await UpdateSearch();
            }
        }

        private async void tsbSearhOnClick(object sender, EventArgs e) => await UpdateSearch();

        private void tsmiShowDeletedOnDropDownOpened(object sender, EventArgs e)
        {
            switch (ShowDeleted)
            {
                case ShowRemooving.Always:
                    tsmiShowAlways.Visible = false;
                    tsmiShowExacRemoving.Visible = tsmiShowExacNotRemoving.Visible = true;
                    break;
                case ShowRemooving.ExecRemoving:
                    tsmiShowExacRemoving.Visible = false;
                    tsmiShowAlways.Visible = tsmiShowExacNotRemoving.Visible = true;
                    break;
                case ShowRemooving.ExecNotRemoving:
                    tsmiShowExacNotRemoving.Visible = false;
                    tsmiShowAlways.Visible = tsmiShowExacRemoving.Visible = true;
                    break;
            }
        }

        private async void tsmiRemoveModeShowOnClick(object sender, EventArgs e) => await ShowRemoving(((ToolStripMenuItem)sender).Tag.ToString().StringToEnum<ShowRemooving>());

        private void lvModelOnSelectedIndexChanged(object sender, EventArgs e)
        {
            (bool isRemove, bool isRepair) = SelectedCheck();

            tsbDel.Visible = isRemove;
            tsbRepair.Visible = isRepair;
            tsbEdit.Visible = lvModel.SelectedItems.Count == 1;
            tslEnterCount.Text = String.Format("Выбрано: {0}", lvModel.SelectedItems.Count);
        }

        private void cmsModelOnOpening(object sender, CancelEventArgs e)
        {
            (bool isRemove, bool isRepair) = SelectedCheck();

            tsmiDel.Visible = isRemove;
            tsmiRepair.Visible = isRepair;
            tsmiEdit.Visible = lvModel.SelectedItems.Count == 1;
        }

        private void tsbVisibleModeOnClick(object sender, EventArgs e) => ShowVisibleMode(((ToolStripButton)sender).Tag.ToString().StringToEnum<VisibleMode>());

        private void tsbGridOnClick(object sender, EventArgs e) => ShowGridVisible(!IsGridLines);

        private async void tsbDelOrRepairOnClick(object sender, EventArgs e) => await DeleteOrRepair();

        public async void lvModelOnKeyDown(object sender, KeyEventArgs e)
        {
            bool isComand = false;
            (bool isRemove, bool isRepair) = SelectedCheck();

            switch (e.KeyCode)
            {
                case Keys.Delete
                when isRemove:
                    isComand = true;
                    await DeleteOrRepair();
                    break;
                case Keys.Enter when lvModel.SelectedItems.Count == 1:
                    isComand = true;
                    OnUpdateChanged(lvModel.SelectedItems[0].Tag);
                    break;
                case Keys.R when e.Control && isRepair:
                    isComand = true;
                    await DeleteOrRepair();
                    break;
                case Keys.Insert:
                    isComand = true;
                    OnInsertChanged();
                    break;
                case Keys.A when e.Control && ShowDeleted != ShowRemooving.Always:
                    isComand = true;
                    await ShowRemoving(ShowRemooving.Always);
                    break;
                case Keys.D when e.Control && ShowDeleted != ShowRemooving.ExecRemoving:
                    isComand = true;
                    await ShowRemoving(ShowRemooving.ExecRemoving);
                    break;
                case Keys.V when e.Control && ShowDeleted != ShowRemooving.ExecNotRemoving:
                    isComand = true;
                    await ShowRemoving(ShowRemooving.ExecNotRemoving);
                    break;
                case Keys.G when e.Control && VisibleMode != VisibleMode.Tile:
                    isComand = true;
                    ShowGridVisible(!IsGridLines);
                    break;
                case Keys.U when e.Control:
                    isComand = true;
                    ShowVisibleMode(VisibleMode == VisibleMode.Tile ? VisibleMode.Row : VisibleMode.Tile);
                    break;
                case Keys.E when e.Control && tsbEndPage.Enabled && tsbEndPage.Visible:
                    isComand = true;
                    await UpdatePager(ActionPager.End);
                    break;
                case Keys.H when e.Control && tsbStartPage.Enabled && tsbStartPage.Visible:
                    isComand = true;
                    await UpdatePager(ActionPager.Start);
                    break;
                case Keys.B when e.Control && tsbBackPage.Enabled && tsbBackPage.Visible:
                    isComand = true;
                    await UpdatePager(ActionPager.Back);
                    break;
                case Keys.N when e.Control && tsbNextPage.Enabled && tsbNextPage.Visible:
                    isComand = true;
                    await UpdatePager(ActionPager.Next);
                    break;
                case Keys.P when e.Control:
                    isComand = true;
                    tsmiPagerCheckit.Checked = !tsmiPagerCheckit.Checked;
                    tsmiPager.DropDown.Close();
                    break;
                case Keys.Q when e.Control:
                    isComand = true;
                    tsmiAllCountShow.Checked = !tsmiAllCountShow.Checked;
                    break;
                case Keys.F when e.Control:
                    isComand = true;
                    tsmiEnterCountShow.Checked = !tsmiEnterCountShow.Checked;
                    break;
            }

            e.SuppressKeyPress = isComand;
        }

        private async void tstbActualPageOnKeyPress(object sender, KeyPressEventArgs e)
        {
            e.NumRestrictionTextBox();

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await UpdatePager(ActionPager.Enter);
            }
        }

        private async void tsbActionPageOnClick(object sender, EventArgs e) => await UpdatePager(((ToolStripButton)sender).Tag.ToString().StringToEnum<ActionPager>());

        private async void PagerDropDownOnClosing(object? sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason.Equals(ToolStripDropDownCloseReason.ItemClicked)) e.Cancel = true;
            else
            {
                tsmitbLimitPage.Text = String.IsNullOrEmpty(tsmitbLimitPage.Text) ? "100" : tsmitbLimitPage.Text;
                await UpdateCountPage(tsmiPagerCheckit.Checked ? Convert.ToInt32(tsmitbLimitPage.Text) : 0);

                if (!tsmiPagerCheckit.Checked) await LoadListAsync();
            }
        }

        private void tsmitbLimitPageOnKeyPress(object sender, KeyPressEventArgs e) => e.NumRestrictionTextBox();

        private void tsmiRepairLimitPageOnClick(object sender, EventArgs e) => tsmitbLimitPage.Text = Properties.Settings.Default.Limit.ToString();

        private void tsmiPagerCheckitOnCheckedChanged(object sender, EventArgs e)
        {
            Dictionary<bool, (string, string, Color)> parametrs = new()
            {
                { false, ("Включить(Ctrl+P)", "Включить постраничный вывод(Ctrl+P)", FilterOffColor) },
                { true, ("Выключить(Ctrl+P)", "Выключить постраничный вывод(Ctrl+P)", FilterOnColor) }
            };

            FilterFunction.CheckedChangedItemMenu(tsmiPagerCheckit, parametrs);
            tslPager.Visible = tsmitbLimitPage.Visible = tsmiPagerCheckit.Checked;
            tsmiRepairLimitPage.Visible = tsmiPagerCheckit.Checked && Properties.Settings.Default.Limit != 0;

            if (tsmiPagerCheckit.Checked)
            {
                tsmitbLimitPage.Text = Properties.Settings.Default.Limit != 0 && PageLimit == 0
                                        ? Properties.Settings.Default.Limit.ToString()
                                        : PageLimit == 0
                                            ? "100"
                                            : PageLimit.ToString();
            }
        }

        private void tsmiEnterCountShowOnCheckedChanged(object sender, EventArgs e)
        {
            IsShowCountEnter = tsmiEnterCountShow.Checked;
            Dictionary<bool, (string, string, Color)> parametrs = new()
            {
                { false, ("Отобразить выбраное количество(Ctrl+Q)", "Отобразить выбраное количество строк(Ctrl+Q)", FilterOffColor) },
                { true, ("Скрыть выбраное количество(Ctrl+Q)", "Скрыть выбраное количество строк(Ctrl+Q)", FilterOnColor) }
            };

            InformationChecket(tsmiEnterCountShow, parametrs, (lvModel.SelectedItems.Count, "Выбрано: {0}", tslEnterCount));
        }

        private async void tsmiAllCountShowOnCheckedChanged(object sender, EventArgs e)
        {
            IsShowCountAll = tsmiAllCountShow.Checked;
            Dictionary<bool, (string, string, Color)> parametrs = new()
            {
                { false, ("Отобразить общее количество(Ctrl+F)", "Отобразить общее количество строк(Ctrl+F)", FilterOffColor) },
                { true, ("Скрыть общее количество(Ctrl+F)", "Скрыть общее количество строк(Ctrl+F)", FilterOnColor) }
            };

            InformationChecket(tsmiAllCountShow, parametrs, (await modelType.GetResultByType<int>([Parameters.Conditions], nameof(DBProvider.Count)), "Всего: {0}", tslAllCount));
        }

        private void lvModelOnMouseDoubleClick(object sender, MouseEventArgs e) => OnUpdateChanged(lvModel.SelectedItems[0].Tag);

        protected virtual void OnInsertChanged()
        {
            InsertChanged?.Invoke(this, async (m) => { await LoadListAsync(); });
        }

        protected virtual void OnUpdateChanged(object modelUpdate)
        {
            UpdateChanged?.Invoke(modelUpdate, async (m) => { await LoadListAsync(); });
        }

        private void tsbInsertOnClick(object sender, EventArgs e) => OnInsertChanged();

        private void tsbEditOnClick(object sender, EventArgs e) => OnUpdateChanged(lvModel.SelectedItems[0].Tag);
    }
}