using DataBaseProvaider.Attributes;
using RentalDBModels.Models.Interface;
using RentalDBModels.Views.Interface;
using RentalDBModels.Views.Abstract;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Enums;
using RentalAccountingApp.Properties;

namespace RentalAccountingApp.Forms
{
    public partial class DBModelLookupEditor : Form
    {
        private IModel model;

        /// <summary>
        /// Событие при обновлении/добавлении/удалении
        /// </summary>
        public event EventHandler<IModel> UpdateChanged;

        public DBModelLookupEditor()
        {
            InitializeComponent();
            this.KeyDown += dbmlEditor.DBModelLookupEditorOnKeyDown;
        }

        public DBModelLookupEditor(Type modelType, Action<object> action) : this()
        {
            LoadInfoModel(modelType);
            this.Text = String.Format("{0} [ДОБАВЛЕНИЕ]", dbmlEditor.ParametrTitle);
            model = (IModel)Activator.CreateInstance(((BaseView)Activator.CreateInstance(modelType)).ModelType);
            this.UpdateChanged += (s, e) => action?.Invoke(model);
            this.Icon = Resources.add;
        }

        public DBModelLookupEditor(object model, Action<object> action) : this()
        {
            Init(model);
            this.UpdateChanged += (s, e) => action?.Invoke(model);
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
            this.Icon = Resources.editor;
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
                        if (view is not null)
                        {
                            dbmlEditor.ParametrValue = property.GetValue(view)?.ToString() ?? string.Empty;
                            dbmlEditor.EditorMode = EditorMode.UpdateOrDelete;
                        }

                        dbmlEditor.ParametrTitle = property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
                        dbmlEditor.ParametrTag = property.Name;
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
                                        dbmlEditor.ParametrTitle,
                                        dbmlEditor.ParametrValue,
                                        dbmlEditor.EditorMode == EditorMode.UpdateOrRepair
                                            ? " - УДАЛЁН"
                                            : String.Empty);

            this.Icon = Resources.editor;
        }

        private async void dbmlEditorOnDeleteOrRepairChanged(object sender, EventArgs e)
        {
            await model.Delete();
            OnUpdateChanged();
            UpdateTitle();
        }

        private async void dbmlEditorOnInsertChanged(object sender, EventArgs e)
        {
            await CheckOnSetParametr().ContinueWith(async (task) =>
            {
                if (!task.IsFaulted)
                {
                    await this.Invoke(async () =>
                    {
                        IModel model = await this.model.Insert();
                        CheckResultUpdateModel(model);
                    });
                }
            }).Unwrap();
        }

        private async void dbmlEditorOnUpdateChanged(object sender, EventArgs e)
        {
            await CheckOnSetParametr().ContinueWith(async (task) =>
            {
                if (!task.IsFaulted)
                {
                    await this.Invoke(async () =>
                    {
                        IModel model = await this.model.Update();
                        CheckResultUpdateModel(model);
                    });
                }
            }).Unwrap();
        }

        protected virtual void OnUpdateChanged()
        {
            UpdateChanged?.Invoke(this, model);
        }

        /// <summary>
        /// Проверка заполнености и запись измененного значения параметра
        /// </summary>
        /// <returns>Процес</returns>
        private async Task CheckOnSetParametr()
        {
            if (await dbmlEditor.tbValueParametr.TextEmptyTextBox())
            {
                model.GetType().GetProperty(dbmlEditor.ParametrTag)?.SetValue(model, dbmlEditor.tbValueParametr.Text);
            }
            else await Task.FromException(new Exception());
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
                dbmlEditor.ParametrValue = dbmlEditor.tbValueParametr.Text;
                UpdateTitle();
                OnUpdateChanged();
            }
        }
    }
}
