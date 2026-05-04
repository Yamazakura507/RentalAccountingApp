using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Enums;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Controls
{
    public partial class DBModelComplexEditor : UserControl
    {
        private Loader loader = new() { Size = new(50, 50) };
        private EditorMode editorMode;
        private bool isSizeFixTabDependency = false;

        /// <summary>
        /// Событие при добавлении
        /// </summary>
        public event EventHandler InsertChanged;

        /// <summary>
        /// Событие при обновлении
        /// </summary>
        public event EventHandler UpdateChanged;

        /// <summary>
        /// Событие при удалении
        /// </summary>
        public event EventHandler DeleteChanged;

        /// <summary>
        /// Событие при востановлении
        /// </summary>
        public event EventHandler RepairChanged;

        /// <summary>
        /// Режим редактирования модели
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EditorMode EditorMode 
        { 
            get => editorMode;
            set
            {
                if (editorMode != value)
                {
                    editorMode = value;
                    CheckMode();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Список редактируемых параметров объекта
        /// </summary>
        public ObservableCollection<ModelParametrEditor> Parametrs { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Список зависимостей объекта
        /// </summary>
        public ObservableCollection<DependencyCollection> DependencyParametrs { get; }

        /// <summary>
        /// Набор изображений для привязок
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageList ImageList { get; set; }

        /// <summary>
        /// Иконка модальной формы выбора в справочнике
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Icon BaseCatologIcon { get; set; }

        /// <summary>
        /// Первичный ключ словарей зависимостей
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PKStr { get; set; } = "Id";

        public DBModelComplexEditor()
        {
            InitializeComponent();

            loader.AutoSetup(this);
            loader.Visible = false;
            CheckMode();

            Parametrs = new ObservableCollection<ModelParametrEditor>();
            DependencyParametrs = new ObservableCollection<DependencyCollection>();
            Parametrs.CollectionChanged += ParametrsOnCollectionChanged;
            DependencyParametrs.CollectionChanged += DependencyParametrsOnCollectionChanged;
        }

        private void DependencyParametrsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0; i < e.NewItems.Count; i++)
                    {
                        AddDependency((DependencyCollection)e.NewItems[i], Parametrs.Count + e.NewStartingIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (int i = 0; i < e.NewItems.Count; i++)
                    {
                        CheckOrCreateTabControl().TabPages.RemoveAt(e.NewStartingIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CheckOrCreateTabControl().TabPages.Clear();
                    break;
            }
        }

        private void ParametrsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0; i < e.NewItems.Count; i++)
                    {
                        AddView((ModelParametrEditor)e.NewItems[i], e.NewStartingIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
            }
        }

        /// <summary>
        /// Добавление визуального компонента для редактирования параметар
        /// </summary>
        /// <param name="parametr">Параметр</param>
        /// <param name="index">Индекс добавления</param>
        private void AddView(ModelParametrEditor parametr, int index)
        {
            Label controlTitle = new()
            {
                Text = parametr.Title,
                Font = new("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = true
            };

            Control controlEdit = parametr.Type switch
            {
                Type t when t.Equals(typeof(string)) => CreateTextBox(parametr),
                Type t when t.Equals(typeof(double)) || t.Equals(typeof(int)) => CreateNumericUpDown(parametr),
                Type t when t.Equals(typeof(DateTime)) => CreateDateTimePicker(parametr)
            };

            controlEdit.Dock = DockStyle.Fill;
            controlEdit.Margin = new Padding(0,5,5,5);
            controlEdit.KeyDown += DBModelLookupEditorOnKeyDown;

            if (tlp.RowCount <= index)
            {
                tlp.RowCount++;
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                this.ParentForm.Height += controlEdit.Height + controlEdit.Margin.Top + controlEdit.Margin.Bottom;
                this.ParentForm.MinimumSize = new Size(this.ParentForm.MinimumSize.Width, this.ParentForm.Height);
            }

            tlp.Controls.Add(controlTitle,0,index);
            tlp.Controls.Add(controlEdit,1,index);
        }

        /// <summary>
        /// Добавление зависимости
        /// </summary>
        /// <param name="collection">Параметр зависимости</param>
        /// <param name="index">Индекс зависимости</param>
        private void AddDependency(DependencyCollection collection, int index)
        {
            switch (collection.DependencyType)
            {
                case DependencyType.OneToMany:
                    AddDependencyOneToMany(collection, index - Parametrs.Count);
                    break;
                case DependencyType.OneToOne:
                    break;
            }
        }

        /// <summary>
        /// Добавление зависимости один ко множеству
        /// </summary>
        /// <param name="dependenciesTabControl">Таб панель для зависимостей</param>
        /// <param name="collection">Параметр зависимости</param>
        /// <param name="index">Положение таб страницы</param>
        private void AddDependencyOneToMany(DependencyCollection collection, int index)
        {
            TabControl dependenciesTabControl = CheckOrCreateTabControl();
            TabPage dependencyTabPage = new (collection.Title) 
            { 
                ImageIndex = dependenciesTabControl.ImageList.Images.IndexOfKey(collection.ImageKey),
                TabIndex = index
            };

            DBModelListView dependecyLV = new DBModelListView()
            {
                ImageList = dependenciesTabControl.ImageList,
                ModelType = collection.DependencyViewType,
                Dock = DockStyle.Fill,
                IsRepairRow = false,
                RemovingRowColor = collection.RemoveRowColor,
                IsEditor = true,
                IsRepairEditor = collection.IsEditingDependensies
            };

            dependecyLV.lvModel.KeyDown += dependecyLV.lvModelOnKeyDown;

            (Form modalForm, DBModelListView modalLV) = CreateBaseCatalogModalForm(collection);

            dependecyLV.InsertChanged += (s, e) => 
            {
                modalForm.ShowDialog();

                if (modalForm.DialogResult.Equals(DialogResult.OK) && modalLV.SelectedModalResult?.Count() > 0)
                {
                    collection.AddRange(modalLV.SelectedModalResult.Select(i => (int)i.Value)); 
                    collection.FlagingEditCondition(dependecyLV.Parameters, modalLV.Parameters);
                    dependecyLV.IsRepairEditor = collection.IsEditingDependensies;
                    e?.Invoke();
                }
            };
            dependecyLV.DeleteOrRepairChanged += (s, e) =>
            {
                collection.RemoveOrRepairRange(s.Select(i => (int)i.Value));
                collection.FlagingEditCondition(dependecyLV.Parameters, modalLV.Parameters);
                dependecyLV.IsRepairEditor = collection.IsEditingDependensies;
                e?.Invoke();
            };
            dependecyLV.PreVisualizationChanged += (s, e) => collection.FormatingDependenciesViewList(e);
            dependecyLV.RepairEditingChanged += (s, e) =>
            {
                collection.SetBaseToDependesies();
                collection.FlagingEditCondition(dependecyLV.Parameters, modalLV.Parameters);
                dependecyLV.IsRepairEditor = collection.IsEditingDependensies;
                e?.Invoke();
            };
            collection.UpdateChange += async (s, e) =>
            {
                collection.UpdateDependesiesToBase();
                collection.FlagingEditCondition(dependecyLV.Parameters, modalLV.Parameters);
                dependecyLV.IsRepairEditor = collection.IsEditingDependensies;

                await dependecyLV.LoadListAsync();
            };

            collection.EditConditon(dependecyLV.Parameters, ConditionalOperators.In);

            dependencyTabPage.Controls.Add(dependecyLV);

            if (!isSizeFixTabDependency)
            {
                this.ParentForm.Height += dependecyLV.Height + dependecyLV.Margin.Top + dependecyLV.Margin.Bottom;
                this.ParentForm.MinimumSize = new Size(Math.Max(this.ParentForm.MinimumSize.Width, dependecyLV.Width) + 30, this.ParentForm.Height);
                isSizeFixTabDependency = true;
            }

            collection.SetDependesiesToBase();

            dependenciesTabControl.TabPages.Add(dependencyTabPage);
        }

        /// <summary>
        /// Создание формы каталога, выбора элементов для добавления в привязку
        /// </summary>
        /// <param name="modelType">Тип модели каатлога</param>
        /// <param name="title">Подпись каатлога</param>
        /// <param name="imageIndex">Иконка католога</param>
        /// <param name="dependenciesId">Идентификаторы исключения</param>
        /// <returns>Форма выбора элемента привязки</returns>
        private (Form, DBModelListView) CreateBaseCatalogModalForm(DependencyCollection collection)
        {
            Form modalForm = new()
            {
                Text = String.Format("Выбор [КАТАЛОГ - {0}]", collection.Title.ToUpper()),
                Icon = BaseCatologIcon,
                MinimumSize = new Size(620,350),
                StartPosition = FormStartPosition.CenterParent
            };

            TabControl catalogControl = new()
            {
                Dock = DockStyle.Fill,
                ImageList = this.ImageList
            };

            TabPage catalogTabPage = new(collection.Title)
            {
                ImageKey = collection.ImageKey
            };

            DBModelListView dependecyLV = new()
            {
                ImageList = this.ImageList,
                ModelType = collection.DependencyViewType,
                Dock = DockStyle.Fill
            };

            collection.EditConditon(dependecyLV.Parameters, ConditionalOperators.NotIn);
            catalogControl.KeyDown += dependecyLV.lvModelOnKeyDown;

            catalogTabPage.Controls.Add(dependecyLV);
            catalogControl.Controls.Add(catalogTabPage);
            modalForm.Controls.Add(catalogControl);

            return (modalForm, dependecyLV);
        }

        /// <summary>
        /// Метод проверки режима редактирования элемнта
        /// </summary>
        private void CheckMode()
        {
            tsbInsert.Visible = tsbSave.Visible = tsbRemove.Visible = tsbRepair.Visible = false;

            switch (EditorMode)
            {
                case EditorMode.Insert:
                    tsbInsert.Visible = true;
                    break;
                case EditorMode.Update:
                    tsbSave.Visible = true;
                    break;
                case EditorMode.UpdateOrDelete:
                    tsbSave.Visible = tsbRemove.Visible = true;
                    break;
                case EditorMode.UpdateOrRepair:
                    tsbSave.Visible = tsbRepair.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Создание текстового поля по параметру
        /// </summary>
        /// <param name="parametr">Параметр</param>
        /// <returns>Текствое поле</returns>
        private TextBox CreateTextBox(ModelParametrEditor parametr)
        {
            TextBox textBox = new ();

            Binding binding = new Binding(
                    nameof(TextBox.Text),
                    parametr,
                    nameof(ModelParametrEditor.Value),
                    true,
                    DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += (s, e) =>
            {
                if (e.Value != null)
                {
                    e.Value = e.Value.ToString();
                }
            };

            textBox.DataBindings.Add(binding);

            if (parametr.SettingFilter?.Regex is not null)
            {
                textBox.TextChanged += (s, e) => textBox.RegexTextBoxCheck(parametr.SettingFilter.Regex);
            }
            
            return textBox;
        }

        /// <summary>
        /// Создание числового поля по параметру
        /// </summary>
        /// <param name="parametr">Параметр</param>
        /// <returns>Числовое поле</returns>
        private NumericUpDown CreateNumericUpDown(ModelParametrEditor parametr)
        {
            NumericUpDown numericUpDown = new()
            {
                DecimalPlaces = parametr.Type.Equals(typeof(double)) ? 2 : 0,
                Maximum = Convert.ToDecimal(parametr.SettingFilter?.Maximum ?? 10e7),
                Minimum = Convert.ToDecimal(parametr.SettingFilter?.Minimum ?? 0)
            };

            Binding binding = new Binding(
                    nameof(NumericUpDown.Value),
                    parametr,
                    nameof(ModelParametrEditor.Value),
                    true,
                    DataSourceUpdateMode.OnPropertyChanged);

            binding.Format += (s, e) =>
            {
                if (e.Value != null)
                {
                    e.Value = Convert.ToDecimal(e.Value ?? 0);
                }
            };

            binding.Parse += (s, e) => e.Value = Convert.ChangeType(e.Value, parametr.Type);

            numericUpDown.DataBindings.Add(binding);

            return numericUpDown;
        }

        /// <summary>
        /// Создание поля даты по параметру
        /// </summary>
        /// <param name="parametr">Параметр</param>
        /// <returns>Поле даты</returns>
        private DateTimePicker CreateDateTimePicker(ModelParametrEditor parametr)
        {
            DateTimePicker dateTimePicker = new ()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd MMMM yyyy",
                MaxDate = Convert.ToDateTime(parametr.SettingFilter?.Maximum ?? DateTime.Now),
                MinDate = Convert.ToDateTime(parametr.SettingFilter?.Minimum ?? new DateTime(1991, 12, 25))
            };

            Binding binding = new Binding(
                    nameof(DateTimePicker.Value),
                    parametr,
                    nameof(ModelParametrEditor.Value),
                    true,
                    DataSourceUpdateMode.OnPropertyChanged);

            binding.Format += (s, e) =>
            {
                if (e.Value != null)
                {
                    e.Value = Convert.ToDateTime(e.Value ?? DateTime.Now);
                }
            };

            binding.Parse += (s, e) => e.Value = Convert.ToDateTime(e.Value);

            dateTimePicker.DataBindings.Add(binding);

            return dateTimePicker;
        }

        /// <summary>
        /// Получение таб панели для вывода привязок многие ко многим
        /// </summary>
        /// <returns>Таб панель</returns>
        private TabControl CheckOrCreateTabControl()
        {
            TabControl dependenciesTabControl = tlp.Controls.OfType<TabControl>().FirstOrDefault();

            if (dependenciesTabControl is null)
            {
                dependenciesTabControl = new()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 5, 5, 5),
                    ImageList = this.ImageList
                };

                tlp.RowCount++;
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlp.Controls.Add(dependenciesTabControl, 0, tlp.RowCount - 1);
                tlp.SetColumnSpan(dependenciesTabControl, 2);
                this.ParentForm.Height += dependenciesTabControl.Height + dependenciesTabControl.Margin.Top + dependenciesTabControl.Margin.Bottom;
                this.ParentForm.MinimumSize = new Size(this.ParentForm.MinimumSize.Width, this.ParentForm.Height);
            }

            return dependenciesTabControl;
        }

        /// <summary>
        /// Проверка на пустые значения перед сохранением
        /// </summary>
        /// <returns>Результат проверки</returns>
        private async Task<bool> CheckEmptyValue()
        {
            bool isEmptyVal = false;

            foreach (Control control in tlp.Controls)
            {
                if (control is TextBox && !await((TextBox)control).TextEmptyTextBox())
                {
                    isEmptyVal = true;
                }
            }

            return isEmptyVal;
        }

        protected async virtual void OnInsertChanged()
        {
            if (await CheckEmptyValue()) return;

            loader.StartAnimation();
            InsertChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrDelete;
            CheckMode();
            loader.StopAnimation();
        }

        protected virtual void OnUpdateChanged()
        {
            loader.StartAnimation();
            UpdateChanged?.Invoke(this, EventArgs.Empty);
            loader.StopAnimation();
        }

        protected virtual void OnDeleteChanged()
        {
            loader.StartAnimation();
            DeleteChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrRepair;
            CheckMode();
            loader.StopAnimation();
        }

        protected virtual void OnRepairChanged()
        {
            loader.StartAnimation();
            RepairChanged?.Invoke(this, EventArgs.Empty);
            EditorMode = EditorMode.UpdateOrDelete;
            CheckMode();
            loader.StopAnimation();
        }

        private void tsbSaveOnClick(object sender, EventArgs e) => OnUpdateChanged();

        private void tsbAddOnClick(object sender, EventArgs e) => OnInsertChanged();

        private void tsbRemoveOnClick(object sender, EventArgs e) => OnDeleteChanged();

        private void tsbRepairOnClick(object sender, EventArgs e) => OnRepairChanged();

        public void DBModelLookupEditorOnKeyDown(object sender, KeyEventArgs e)
        {
            bool isComand = false;

            switch (e.KeyCode)
            {
                case Keys.Delete
                when EditorMode == EditorMode.UpdateOrDelete:
                    isComand = true;
                    OnDeleteChanged();
                    break;
                case Keys.S 
                when e.Control && (EditorMode is EditorMode.UpdateOrDelete or EditorMode.UpdateOrRepair or EditorMode.Update):
                    isComand = true;
                    OnUpdateChanged();
                    break;
                case Keys.R 
                when e.Control && EditorMode == EditorMode.UpdateOrRepair:
                    isComand = true;
                    OnRepairChanged();
                    break;
                case Keys.Insert
                when EditorMode == EditorMode.Insert:
                    isComand = true;
                    OnInsertChanged();
                    break;
            }

            e.SuppressKeyPress = isComand;
        }
    }
}
