using DataBaseProvaider.Attributes;
using RentalAccountingApp.Properties;
using RentalDBModels.Models.Interface;
using RentalDBModels.Views.Abstract;
using RentalDBModels.Views.Interface;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Enums;
using WinFormsComponents.Classes.Model;

namespace RentalAccountingApp.Forms.EditForm
{
    public partial class DBModelComplexEditor : Form
    {
        private string titleHeader;
        private string valHeader;
        private IModel model;
        private IView view;
        private List<Type> updateTypeList = new();

        /// <summary>
        /// Событие при обновлении/добавлении/удалении
        /// </summary>
        public event EventHandler<IModel> UpdateChanged;

        public DBModelComplexEditor(Form parentForm)
        {
            InitializeComponent();
            this.KeyDown += dmlceEditor.DBModelLookupEditorOnKeyDown;
            parentForm.FormClosing += (s, e) => this.Close();
        }

        public DBModelComplexEditor(Type modelType, Action action, Form parentForm) : this(parentForm)
        {
            LoadInfoModel(modelType);

            this.Text = String.Format("{0} [ДОБАВЛЕНИЕ]", titleHeader);
            this.Icon = Resources.add;

            view = (BaseView)Activator.CreateInstance(modelType);
            LoadInfoDependency().Wait();

            model = (IModel)Activator.CreateInstance(view.ModelType);

            this.UpdateChanged += (s, e) => action?.Invoke();
        }

        public DBModelComplexEditor(object model, Action action, Form parentForm) : this(parentForm)
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
            view = (IView)model;

            LoadInfoModel(view.GetType(), view);
            UpdateTitle();

            this.model = await view.GetModel();
            this.Icon = Resources.editorIcon;
            await LoadInfoDependency();
        }

        /// <summary>
        /// Загрузка информации о модели
        /// </summary>
        /// <param name="modelType">Тип представления модели</param>
        /// <param name="view">Представление модели</param>
        private void LoadInfoModel(Type modelType, IView view = null)
        {
            if (view is not null) dmlceEditor.EditorMode = EditorMode.UpdateOrDelete;

            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ViewModelAttribute vmAttribute = property.GetCustomAttribute<ViewModelAttribute>();

                if (vmAttribute != null)
                {
                    if (!vmAttribute.ViewHide)
                    {
                        object valueParametr = null;

                        if (view is not null) valueParametr = property.GetValue(view);

                        string title = property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;

                        dmlceEditor.Parametrs.Add(new(title, valueParametr, property.Name, property.PropertyType));

                        if (vmAttribute.Headline)
                        {
                            titleHeader = title;
                            valHeader = valueParametr?.ToString();
                        }
                    }
                    else if (vmAttribute.RemovingFlag && view is not null && !Convert.ToBoolean(property.GetValue(view)))
                    {
                        dmlceEditor.EditorMode = EditorMode.UpdateOrRepair;
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка информации о привязках
        /// </summary>
        /// <param name="modelType">Тип модели</param>
        private async Task LoadInfoDependency()
        {
            dmlceEditor.DependencyParametrs.Clear();

            PropertyInfo[] properties = view.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                DependencyAttribute dependency = property.GetCustomAttribute<DependencyAttribute>();

                if (dependency is not null)
                {
                    DependencyCollection dependencyCollection = new(property.Name, dependency.Title, dependency.DependencyViewType, dependency.DependencyType)
                    {
                        ImageKey = dependency.ImageKey,
                        DependencyModelType = dependency.DependencyModelType
                    };

                    if (model is not null)
                    {
                        dependencyCollection.AddRange((await (Task<IEnumerable<int>>)property.GetValue(view)).Select(i => new DependencyInfo(i)));
                    }

                    dmlceEditor.DependencyParametrs.Add(dependencyCollection);
                }
            }
        }

        /// <summary>
        /// Обновление заголовка формы
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = String.Format("{0}:{1} [РЕДАКТИРОВАНИЕ{2}]",
                                        titleHeader,
                                        valHeader,
                                        dmlceEditor.EditorMode == EditorMode.UpdateOrRepair
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
            await CheckResultUpdateModel(model);
        }

        private async void dbmlEditorOnUpdateChanged(object sender, EventArgs e)
        {
            IModel oldModel = CheckOnSetParametr();
            IModel model = await this.model.Update(oldModel);
            await CheckResultUpdateModel(model);
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

            foreach (ModelParametrEditor parametr in dmlceEditor.Parametrs)
            {
                model.GetType().GetProperty(parametr.Tag)?.SetValue(model, parametr.Value);
            }

            Dictionary<Type, IEnumerable<int>>  insertDict =
                dmlceEditor.DependencyParametrs
                  .ToDictionary(
                        d => d.DependencyModelType,
                        d => d.Dependencies.Where(i => i.Status == DependencyStatus.Insert)?.Select(i => i.IdDependency));

            CheckDictUpdate(insertDict);
            model.GetType().GetProperty(nameof(IForigenParent.InsertDependencies))?.SetValue(model, insertDict);

            if (dmlceEditor.EditorMode != EditorMode.Insert)
            {
                Dictionary<Type, IEnumerable<int>> removeDict =
                    dmlceEditor.DependencyParametrs
                            .ToDictionary(
                                d => d.DependencyModelType,
                                d => d.Dependencies.Where(i => i.Status == DependencyStatus.Remove)?.Select(i => i.IdDependency));

                CheckDictUpdate(removeDict);
                model.GetType().GetProperty(nameof(IForigenParent.RemoveDependencies))?.SetValue(model, removeDict);
            }

            return oldModel;
        }

        /// <summary>
        /// Проверка результата, запись изменений при успешном сохранении
        /// </summary>
        /// <param name="model">Измененная модель</param>
        private async Task CheckResultUpdateModel(IModel model)
        {
            if (model is null)
            {
                InfoViewer.AlertMessege("Сохранение завершилось с ошибкой!");
            }
            else
            {
                this.model = model;

                foreach (Type updateType in updateTypeList)
                {
                    dmlceEditor.DependencyParametrs.First(i => i.DependencyModelType.Equals(updateType)).OnUpdateChange();
                }

                updateTypeList.Clear();
                valHeader = dmlceEditor.Parametrs.First(i => i.Title.Equals(titleHeader)).Value.ToString();
                UpdateTitle();
                OnUpdateChanged();
            }
        }

        /// <summary>
        /// Проверка с занесением в список очереди на обновление
        /// </summary>
        /// <param name="updateDict">Список с обновлениями</param>
        private void CheckDictUpdate(Dictionary<Type, IEnumerable<int>> updateDict)
        {
            if (updateDict is null) return;

            foreach (KeyValuePair<Type, IEnumerable<int>> up in updateDict)
            {
                if (up.Value.Count() > 0 && !updateTypeList.Contains(up.Key))
                {
                    updateTypeList.Add(up.Key);
                }
            }
        }
    }
}
