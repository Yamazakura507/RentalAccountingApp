using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Controls
{
    public partial class DBModelSelectedList : UserControl, ISelected
    {
        private Type modelType;
        private string parametrHeaderName = null;
        private int? selectVal = null;

        /// <summary>
        /// Наименование колонки первичного ключа
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PKColName { get; set; } = "Id";

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
        /// Список изображений для формы выбора
        /// </summary>
        public ImageList ImageList { get; set; } = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Ключ изображения из списка
        /// </summary>
        public string ImageKey { get; set; } = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Иконка формы списка выбора элемента
        /// </summary>
        public Icon IconSelectedForm { get; set; } = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Заголовок формы выбороа элемента
        /// </summary>
        public string TitleCatalogSelectedForm { get; set; } = null;

        /// <summary>
        /// Событие изменения выбора
        /// </summary>
        public event EventHandler SelectedChange;

        public DBModelSelectedList(bool isNulValue = false)
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
            if (ImageList is not null && ImageKey is not null) pbIcon.Image = ImageList.Images[ImageKey];
            else pbIcon.Visible = false;

            LoadBaseParametr();
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
                    if (vma.Headline) parametrHeaderName = property.Name;
                }
            }
        }

        private async void DBModelPickerOnLoad(object sender, EventArgs e)
        {
            LoadInfo();

            if (SelectedVal is not null)
            {
                await UploadTitleSelected();
                btAdd.BackgroundImage = Properties.Resources.editor;
            }
            else
            {
                lbSelectedName.Text = "<НЕ УКАЗАНО>";
                btAdd.BackgroundImage = Properties.Resources.add;
            }
        }

        /// <summary>
        /// Обновление подписи по выбраному элементу
        /// </summary>
        /// <returns>Процес</returns>
        private async Task UploadTitleSelected()
        {
            CollectionParametrs parametrs = new() { Conditions = [new ConditionsParametr(PKColName, ConditionalOperators.Equal, SelectedVal)] };
            object selectedModel = (await modelType.GetCollectionByType<object>([parametrs], nameof(DBProvider.GetCollectionModel))).First();
            PropertyInfo property = modelType.GetProperty(parametrHeaderName);

            lbSelectedName.Text = property.GetValue(selectedModel).ToString();
        }

        private void btNullValOnClick(object sender, EventArgs e)
        {
            if (!lbSelectedName.Enabled)
            {
                btNullVal.BackgroundImage = Properties.Resources.checkible;
                lbSelectedName.Enabled = btAdd.Enabled = true;
            }
            else
            {
                btNullVal.BackgroundImage = Properties.Resources.uncheckible;
                SelectedVal = null;
                lbSelectedName.Text = "<НЕ УКАЗАНО>";
                lbSelectedName.Enabled = btAdd.Enabled = false;
                btAdd.BackgroundImage = Properties.Resources.add;
            }
        }

        public void OnSelectedChange() => SelectedChange?.Invoke(this, EventArgs.Empty);

        private async void btSelectedOnClick(object sender, EventArgs e)
        {
            (Form modalForm, DBModelListView modalLV) = CreateBaseCatalogModalForm();

            modalForm.ShowDialog();

            if (modalForm.DialogResult.Equals(DialogResult.OK) && modalLV.SelectedModalResult?.Count() > 0)
            {
                SelectedVal = (int)modalLV.SelectedModalResult.First().Value;

                await UploadTitleSelected();
                btAdd.BackgroundImage = Properties.Resources.editor;
            }

            modalForm.Dispose();
        }

        /// <summary>
        /// Создание формы каталога, выбора элементов для добавления в привязку
        /// </summary>
        /// <returns>Форма выбора элемента привязки</returns>
        private (Form, DBModelListView) CreateBaseCatalogModalForm()
        {
            Form modalForm = new()
            {
                Text = String.Format("Выбор [КАТАЛОГ - {0}]", TitleCatalogSelectedForm.ToUpper()),
                Icon = IconSelectedForm,
                MinimumSize = new Size(620, 350),
                StartPosition = FormStartPosition.CenterParent
            };

            TabControl catalogControl = new()
            {
                Dock = DockStyle.Fill,
                ImageList = this.ImageList
            };

            TabPage catalogTabPage = new(TitleCatalogSelectedForm)
            {
                ImageKey = ImageKey
            };

            DBModelListView dependecyLV = new()
            {
                ImageList = this.ImageList,
                ModelType = modelType,
                Dock = DockStyle.Fill,
                MultiSelect = false
            };

            dependecyLV.Parameters = Parameters.Clone();

            if (SelectedVal is not null) 
                dependecyLV.Parameters.Conditions += new ConditionsParametr(PKColName, ConditionalOperators.NotEqual, SelectedVal);

            catalogControl.KeyDown += dependecyLV.lvModelOnKeyDown;

            catalogTabPage.Controls.Add(dependecyLV);
            catalogControl.Controls.Add(catalogTabPage);
            modalForm.Controls.Add(catalogControl);

            return (modalForm, dependecyLV);
        }
    }
}
