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
        private readonly IFilterUIService filterUIService;
        private readonly IListViewPopulationService listViewPopulationService;
        private string parametrRemovingName = null;

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
        /// Настройка взаимодействия с БД(Удаление/Оновление)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        TermsOfInteractionDB TermsOfInteractionDB { get; set; }

        public DBModelListView()
        {
            InitializeComponent();

            filterUIService = new FilterUIService(FilterOffColor, FilterOnColor);
            listViewPopulationService = new ListViewPopulationService(RemovingRowColor);
        }

        /// <summary>
        /// Добавление подключения
        /// </summary>
        private void ConnectDB()
        {
            DBProvider.NpgsqlProvider = new(ConnectionInfo.ActiveConnection?.ConnectionBuilder ?? ConnectionInfo.DefaultConnection);
            DBProvider.NpgsqlProvider.HandlerErrror.ErrorReporter = new Progress<string>(async message => InfoViewer.ErrrorMessege(message));
        }

        /// <summary>
        /// Подгрузка стартовой информации
        /// </summary>
        private void LoadInfo()
        {
            lvModel.GroupImageList = lvModel.StateImageList = lvModel.LargeImageList = lvModel.SmallImageList = ImageList;
            Parameters ??= new();
            TermsOfInteractionDB ??= new();
            parametrRemovingName = ModelType.GetProperties().FirstOrDefault(i => i.GetCustomAttribute<ViewModelAttribute>()?.RemovingFlag ?? false)?.Name;

            CreateParametrShowRemoving();
            ShowVisibleMode(VisibleMode);
            ShowGridVisible(IsGridLines);
            CheckSearch();
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
                        ToolStripMenuItem tsmiSearh = filterUIService.CreateSearchFilter(titleMenu, property.Name,
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
            await LoadListAsync();
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
                await LoadListAsync();
            }
        }

        /// <summary>
        /// Асинхронная загрузка списка элементов
        /// </summary>
        private async Task LoadListAsync()
        {
            lvModel.SelectedItems.Clear();

            foreach (ConditionsParametr condition in Parameters.Conditions.Where(i => i?.IsSerhing ?? false))
            {
                condition.Value = tstbSearh.Text;
            }

            Items = await modelType.GetCollectionByType<object>([Parameters], nameof(DBProvider.GetCollectionModel));

            listViewPopulationService.PopulateListView(lvModel, modelType, Items);
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
        async private Task DeleteOrRepair()
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

        private async void DBModelListViewOnLoad(object sender, EventArgs e)
        {
            if (!this.Enabled) return;

            LoadInfo();
            ConnectDB();
            ColumnLoad();
            await LoadListAsync();
        }

        private async void tstbSearhOnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await LoadListAsync();
            }
        }

        private async void tsbSearhOnClick(object sender, EventArgs e) => await LoadListAsync();

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
        }

        private void cmsModelOnOpening(object sender, CancelEventArgs e)
        {
            (bool isRemove, bool isRepair) = SelectedCheck();

            tsmiDel.Visible = isRemove;
            tsmiRepair.Visible = isRepair;
        }

        private void tsbVisibleModeOnClick(object sender, EventArgs e) => ShowVisibleMode(((ToolStripButton)sender).Tag.ToString().StringToEnum<VisibleMode>());

        private void tsbGridOnClick(object sender, EventArgs e) => ShowGridVisible(!IsGridLines);

        async private void tsbDelOrRepairOnClick(object sender, EventArgs e) => await DeleteOrRepair();

        private async void lvModelOnKeyDown(object sender, KeyEventArgs e)
        {
            (bool isRemove, bool isRepair) = SelectedCheck();

            switch (e.KeyCode)
            {
                case Keys.Delete
                when isRemove:
                    await DeleteOrRepair();
                    break;
                case Keys.Enter:
                    break;
                case Keys.Insert:
                    break;
                case Keys.R when e.Control && isRepair:
                    await DeleteOrRepair();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.A when e.Control && ShowDeleted != ShowRemooving.Always:
                    await ShowRemoving(ShowRemooving.Always);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.D when e.Control && ShowDeleted != ShowRemooving.ExecRemoving:
                    await ShowRemoving(ShowRemooving.ExecRemoving);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.V when e.Control && ShowDeleted != ShowRemooving.ExecNotRemoving:
                    await ShowRemoving(ShowRemooving.ExecNotRemoving);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.G when e.Control && VisibleMode != VisibleMode.Tile:
                    ShowGridVisible(!IsGridLines);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.U when e.Control:
                    ShowVisibleMode(VisibleMode == VisibleMode.Tile ? VisibleMode.Row : VisibleMode.Tile);
                    e.SuppressKeyPress = true;
                    break;
            }
        }
    }
}