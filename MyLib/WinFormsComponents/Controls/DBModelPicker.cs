using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Classes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Controls
{
    public partial class DBModelPicker : UserControl, ISelected
    {
        private Type modelType;
        private string parametrRemovingName = null;
        private string parametrHeaderName = null;
        private int? selectVal = null;

        /// <summary>
        /// Наименование колонки первичного ключа
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PKColName { get; set; } = "Id";

        /// <summary>
        /// Колекция элементов списка
        /// </summary>
        private BindingList<object> Items { get; set; }

        /// <summary>
        /// Набор параметров: фильтрации, сортировки, ограничений вывода
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CollectionParametrs Parameters { get; set; }

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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Если этот параметр true появляется кнока для отметки пустого значения
        /// </summary>
        public bool IsNullVal { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Возвращает выбраное значение
        /// </summary>
        public int? SelectedVal 
        { 
            get => selectVal;
            set
            {
                if (selectVal != value)
                {
                    selectVal = value;
                    OnSelectedChange();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Иконка элемента
        /// </summary>
        public Image Image { get; set; } = null;

        /// <summary>
        /// Событие изменения выбора
        /// </summary>
        public event EventHandler SelectedChange;

        public DBModelPicker(bool isNulValue = false)
        {
            InitializeComponent();

            Parameters ??= new();
            IsNullVal = isNulValue;
            btNullVal.Visible = isNulValue;
        }

        /// <summary>
        /// Подгрузка стартовой информации
        /// </summary>
        private void LoadInfo()
        {
            cbDBModel.ValueMember = PKColName;

            if (Image is not null) pbIcon.Image = Image;
            else pbIcon.Visible = false;

            LoadBaseParametr();
            CreateParametrShowRemoving();
        }

        /// <summary>
        /// Загрузка списка элементов
        /// </summary>
        public async Task LoadListAsync()
        {
            if (parametrHeaderName is null)
            {
                this.Enabled = false;
                cbDBModel.Text = "Не найден указатель заголовка!";
                return;
            }

            bool isDefault = SelectedVal is not null && !IsNullVal;

            cbDBModel.BeginUpdate();
            cbDBModel.Items.Clear();

            if (isDefault) cbDBModel.SelectedIndexChanged -= cbDBModelOnSelectedIndexChanged;

            Items = await modelType.GetCollectionByType<object>([Parameters], nameof(DBProvider.GetCollectionModel));

            PropertyInfo property = modelType.GetProperty(parametrHeaderName);

            cbDBModel.DisplayMember = property.Name;
            cbDBModel.DataSource = Items;

            if (isDefault) cbDBModel.SelectedIndexChanged += cbDBModelOnSelectedIndexChanged;
            cbDBModel.EndUpdate();
        }

        /// <summary>
        /// Создание параметра скрытия удаленных значений
        /// </summary>
        private void CreateParametrShowRemoving()
        {
            if (parametrRemovingName is null) return;

            if (!Parameters.Conditions.Any(i => i.ColumnName.Equals(parametrRemovingName)))
            {
                Parameters.Conditions = Parameters.Conditions.InsertAt(new ConditionsParametr(parametrRemovingName, ConditionalOperators.Equal, LogicOperators.And, true), 0);
            }
        }

        /// <summary>
        /// Заполнение базовых иформационных значений о модели
        /// </summary>
        private void LoadBaseParametr()
        {
            foreach (PropertyInfo property in modelType.GetProperties())
            {
                ViewModelAttribute vma = property.GetCustomAttribute<ViewModelAttribute>();

                if (vma is not null)
                {
                    if (vma.RemovingFlag) parametrRemovingName = property.Name;
                    if (vma.Headline) parametrHeaderName = property.Name;
                }
            }
        }

        private async void DBModelPickerOnLoad(object sender, EventArgs e)
        {
            LoadInfo();
            await LoadListAsync();

            if (cbDBModel.Items.Count > 0)
            {
                if (SelectedVal is not null) cbDBModel.SelectedValue = SelectedVal;
                else cbDBModel.SelectedIndex = 0;
            }
        }

        private void btNullValOnClick(object sender, EventArgs e)
        {
            if (SelectedVal is null)
            {
                btNullVal.BackgroundImage = Properties.Resources.checkible;
                cbDBModel.Enabled = true;
            }
            else
            {
                btNullVal.BackgroundImage = Properties.Resources.uncheckible;
                SelectedVal = null;
                cbDBModel.Enabled = false;
            }
        }

        private void cbDBModelOnSelectedIndexChanged(object sender, EventArgs e) => SelectedVal = (int?)cbDBModel.SelectedValue;

        public void OnSelectedChange() => SelectedChange?.Invoke(this, EventArgs.Empty);
    }
}
