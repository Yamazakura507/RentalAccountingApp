using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Controls
{
    public partial class DBModelSelectedNewObject : UserControl, ISelected
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

        /// <summary>
        /// Форма при выборе добавления нового объекта
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Form FormNewObjectDependency { get; set; }

        /// <summary>
        /// Событие изменения выбора
        /// </summary>
        public event EventHandler SelectedChange;

        public DBModelSelectedNewObject()
        {
            InitializeComponent();

            IsNullVal = false;
        }

        public DBModelSelectedNewObject(bool isNulValue)
        {
            InitializeComponent();

            IsNullVal = isNulValue;
        }

        /// <summary>
        /// Подгрузка стартовой информации
        /// </summary>
        private void LoadInfo()
        {
            if (ImageList is not null && ImageKey is not null) pbIcon.Image = ImageList.Images[ImageKey];
            else pbIcon.Visible = false;

            LoadBaseParametr();
            InserrReturningEvent();
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
                EmptySetting();
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
            btDelete.Visible = true;
        }

        public void OnSelectedChange() => SelectedChange?.Invoke(this, EventArgs.Empty);

        private async void btInsertOnClick(object sender, EventArgs e) => FormNewObjectDependency.ShowDialog();

        private async void btDeleteOnClick(object sender, EventArgs e)
        {
            ConditionsParametr[] parametrs = [new(PKColName, ConditionalOperators.Equal, SelectedVal)];

            await modelType.InvokeMethodByType([parametrs], nameof(DBProvider.Delete))
                .ContinueWith(async task =>
                {
                    if (task.IsFaulted)
                    {
                        InfoViewer.AlertMessege("Не удалось удалить временную привязку!");
                    }
                });

            SelectedVal = null;
            EmptySetting();

            Type formType = FormNewObjectDependency.GetType();
            Type viewType = formType.GetField("view", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(FormNewObjectDependency).GetType();

            FormNewObjectDependency = (Form)Activator.CreateInstance(formType, viewType, null, this.GetForm());
            InserrReturningEvent();
        }

        /// <summary>
        /// Назначение настроек при пустом значении
        /// </summary>
        private void EmptySetting()
        {
            lbSelectedName.Text = "<НЕ УКАЗАНО>";
            btDelete.Visible = false;
            btAdd.BackgroundImage = Properties.Resources.add;
        }

        /// <summary>
        /// Добавление события обновления формы выбора
        /// </summary>
        private void InserrReturningEvent()
        {
            EventInfo eventInfo = FormNewObjectDependency.GetType().GetEvent("UpdateChanged");

            if (eventInfo != null)
            {
                Action<object, object> action = GetActionReturnningEvent();

                Delegate handler = Delegate.CreateDelegate(
                    eventInfo.EventHandlerType,
                    action.Target,
                    action.Method
                );

                eventInfo.AddEventHandler(FormNewObjectDependency, handler);
            }
        }

        /// <summary>
        /// Получение действий при обратном событии
        /// </summary>
        /// <returns>Действие</returns>
        private Action<object, object> GetActionReturnningEvent()
        {
            return async (s, e) =>
            {
                if (e is null)
                {
                    SelectedVal = null;
                    EmptySetting();
                }
                else
                {
                    SelectedVal = (int)e.GetType().GetProperty(PKColName).GetValue(e);
                    await UploadTitleSelected();
                    btAdd.BackgroundImage = Properties.Resources.editor;
                }
            };
        }
    }
}
