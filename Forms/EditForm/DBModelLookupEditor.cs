using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Interface;
using RentalDBModels.Views.Interface;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Enums;
using RentalAccountingApp.Properties;

namespace RentalAccountingApp.Forms.EditForm
{
    public partial class DBModelLookupEditor : Form
    {
        private IModel model;

        /// <summary>
        /// Событие при обновлении/добавлении/удалении
        /// </summary>
        public event EventHandler<IModel> UpdateChanged;

        public DBModelLookupEditor(Form parentForm)
        {
            InitializeComponent();
            this.KeyDown += dbmlEditor.DBModelLookupEditorOnKeyDown;
            parentForm.FormClosing += (s,e) => this.Close(); 
        }

        public DBModelLookupEditor(Type modelType, Action action, Form parentForm) : this(parentForm)
        {
            LoadInfoModel(modelType);

            this.Text = String.Format("{0} [ДОБАВЛЕНИЕ]", dbmlEditor.Parametr.Title);
            model = (IModel)Activator.CreateInstance(((BaseView)Activator.CreateInstance(modelType)).ModelType);
            this.UpdateChanged += (s, e) => action?.Invoke();
            this.Icon = Resources.add;
        }

        public DBModelLookupEditor(object model, Action action, Form parentForm) : this(parentForm)
        {
            Init(model);
            this.UpdateChanged += (s, e) => action?.Invoke();
        }

        /// <summary>
        /// Действие при нициализации в режиме изменений
        /// </summary>
        /// <param name="model">объект представления модели</param>
        public async void Init(object model)
        {
            IView view = (IView)model;

            LoadInfoModel(view.GetType(), view);
            UpdateTitle();

            this.model = await view.GetModel();
            this.Icon = Resources.editorIcon;
        }

        /// <summary>
        /// Загрузка информации о модели
        /// </summary>
        /// <param name="modelType">Тип представления модели</param>
        /// <param name="view">Представление модели</param>
        private void LoadInfoModel(Type modelType, IView view = null)
        {
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ViewModelAttribute vmAttribute = property.GetCustomAttribute<ViewModelAttribute>();

                if (vmAttribute != null)
                {
                    if (vmAttribute.Headline)
                    {
                        string value = null;

                        if (view is not null)
                        {
                            value = property.GetValue(view)?.ToString() ?? string.Empty;
                            dbmlEditor.EditorMode = EditorMode.UpdateOrDelete;
                        }

                        dbmlEditor.Parametr = new(property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty, value, property.Name, typeof(string));
                    }
                    else if (vmAttribute.RemovingFlag && view is not null && !Convert.ToBoolean(property.GetValue(view)))
                    {
                        dbmlEditor.EditorMode = EditorMode.UpdateOrRepair;
                    }
                }
            }
        }

        /// <summary>
        /// Обновление заголовка формы
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = String.Format("{0}:{1} [РЕДАКТИРОВАНИЕ{2}]",
                                        dbmlEditor.Parametr.Title,
                                        dbmlEditor.Parametr.Value,
                                        dbmlEditor.EditorMode == EditorMode.UpdateOrRepair
                                            ? " - УДАЛЁН"
                                            : String.Empty);

            this.Icon = Resources.editorIcon;
        }

        private async void dbmlEditorOnDeleteOrRepairChanged(object sender, EventArgs e)
        {
            await model.Delete();
            OnUpdateChanged();
            UpdateTitle();
        }

        private async void dbmlEditorOnInsertChanged(object sender, EventArgs e)
        {
            CheckOnSetParametr();
            IModel model = await this.model.Insert();
            CheckResultUpdateModel(model);
        }

        private async void dbmlEditorOnUpdateChanged(object sender, EventArgs e)
        {
            IModel oldModel = CheckOnSetParametr();
            IModel model = await this.model.Update(oldModel);
            CheckResultUpdateModel(model);
        }

        protected virtual void OnUpdateChanged()
        {
            UpdateChanged?.Invoke(this, model);
        }

        /// <summary>
        /// Проверка заполнености и запись измененного значения параметра
        /// </summary>
        /// <returns>Процес</returns>
        private IModel CheckOnSetParametr()
        {
            IModel oldModel = this.model.Clone();
            model.GetType().GetProperty(dbmlEditor.Parametr.Tag)?.SetValue(model, dbmlEditor.Parametr.Value);

            return oldModel;
        }

        /// <summary>
        /// Проверка результата, запись изменений при успешном сохранении
        /// </summary>
        /// <param name="model">Измененная модель</param>
        private void CheckResultUpdateModel(IModel model)
        {
            if (model is null)
            {
                InfoViewer.AlertMessege("Сохранение завершилось с ошибкой!");
            }
            else
            {
                this.model = model;
                UpdateTitle();
                OnUpdateChanged();
            }
        }
    }
}
