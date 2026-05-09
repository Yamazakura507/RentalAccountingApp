using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
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
        private bool isRepair = true;
        private string titleHeader;
        private string tagHeader;
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
            InitInsert(modelType);
            this.UpdateChanged += (s, e) => action?.Invoke();
            isRepair = view.GetType().GetProperties().Any(i => i?.GetCustomAttribute<ViewModelAttribute>()?.RemovingFlag ?? false);
        }

        public DBModelComplexEditor(object model, Action action, Form parentForm) : this(parentForm)
        {
            Init(model);
            this.UpdateChanged += (s, e) => action?.Invoke();
            isRepair = view.GetType().GetProperties().Any(i => i?.GetCustomAttribute<ViewModelAttribute>()?.RemovingFlag ?? false);
        }

        /// <summary>
        /// Действие при инициализации в режиме добавления
        /// </summary>
        /// <param name="modelType">Тип представления</param>
        /// <param name="action">Действие при обновлении</param>
        /// <param name="parentForm">Родительская форма</param>
        public async void InitInsert(Type modelType)
        {
            await LoadInfoModel(modelType);

            this.Text = String.Format("{0} [ДОБАВЛЕНИЕ]", titleHeader);
            this.Icon = Resources.add;

            view = (IView)Activator.CreateInstance(modelType);
            await LoadInfoDependency();

            model = (IModel)Activator.CreateInstance(view.ModelType);
        }

        /// <summary>
        /// Действие при инициализации в режиме изменений
        /// </summary>
        /// <param name="model">объект представления модели</param>
        public async void Init(object model)
        {
            view = (IView)model;

            await LoadInfoModel(view.GetType(), view);
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
        private async Task LoadInfoModel(Type modelType, IView view = null)
        {
            dmlceEditor.Parametrs.Clear();

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

                        if (view is not null)
                        {
                            valueParametr = property.GetValue(view);

                            if (valueParametr is Task task)
                            {
                                await task;

                                PropertyInfo resultProperty = task.GetType().GetProperty("Result");
                                valueParametr = resultProperty?.GetValue(task);
                            }
                        }

                        string title = property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
                        CheckAttribute checkAttribute = property.GetCustomAttribute<CheckAttribute>();
                        bool isNull = Nullable.GetUnderlyingType(property.PropertyType) != null;
                        SettingFilter filter = null;

                        if (checkAttribute != null)
                        {
                            isNull = checkAttribute.IsNull;

                            if (!String.IsNullOrEmpty(checkAttribute.RegexPattern)) 
                                filter = new(checkAttribute.RegexCheck, checkAttribute.NotChecibleMessage);
                        }

                        if (vmAttribute.IsEdit)
                        {
                            dmlceEditor.Parametrs.Add(
                            new(title, valueParametr, property.Name, property.PropertyType)
                            {
                                IsNull = isNull,
                                SettingFilter = filter
                            });
                        }

                        if (vmAttribute.Headline)
                        {
                            titleHeader = title;
                            tagHeader = property.Name;
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

            PropertyInfo[] properties = view.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

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
                        switch (dependency.DependencyType)
                        {
                            case DependencyType.OneToMany:
                                dependencyCollection.AddRange((await (Task<IEnumerable<int>>)property.GetValue(view)).Select(i => new DependencyInfo(i)));
                                break;
                            case DependencyType.OneToOnePicker:
                            case DependencyType.OneToOneSelectionList:
                            case DependencyType.OneToOneSelectionNewObject:
                                int? id = (int?)property.GetValue(view);

                                if (id is not null)
                                {
                                    dependencyCollection.Add(new DependencyInfo(id.Value));

                                    if (dependency.DependencyType == DependencyType.OneToOneSelectionNewObject)
                                    {
                                        IView viewDependecy = (IView)Convert.ChangeType(await dependency.DependencyViewType.GetResultByType<object>([id.Value], nameof(DBProvider.GetModel)), dependency.DependencyViewType);
                                        dmlceEditor.FormNewObjectDependency = new DBModelComplexEditor(viewDependecy, null, this);
                                    }
                                }
                                break;
                        }
                    }

                    dependencyCollection.IsNullableDependency = Nullable.GetUnderlyingType(property.PropertyType) != null;

                    if (dependency.DependencyType == DependencyType.OneToOneSelectionNewObject)
                        dmlceEditor.FormNewObjectDependency ??= new DBModelComplexEditor(dependency.DependencyViewType, null, this);

                    dmlceEditor.DependencyParametrs.Add(dependencyCollection);
                }
            }
        }

        /// <summary>
        /// Обновление заголовка формы
        /// </summary>
        private void UpdateTitle()
        {
            if (dmlceEditor.EditorMode != EditorMode.Insert)
            {
                this.Text = String.Format("{0}:{1} [РЕДАКТИРОВАНИЕ{2}]",
                                            titleHeader,
                                            valHeader,
                                            dmlceEditor.EditorMode == EditorMode.UpdateOrRepair
                                                ? " - УДАЛЁН"
                                                : String.Empty);
                this.Icon = Resources.editorIcon;
            }
            else
            {
                this.Text = String.Format("{0} [ДОБАВЛЕНИЕ]", titleHeader);
                this.Icon = Resources.add;
            }
        }

        private async void dbmlEditorOnDeleteOrRepairChanged(object sender, Action<EditorMode> e)
        {
            await model.Delete();
            OnUpdateChanged(true);
            e?.Invoke(isRepair ? EditorMode.UpdateOrRepair : EditorMode.Insert);
            UpdateTitle();
        }

        private async void dbmlEditorOnInsertChanged(object sender, EventArgs e)
        {
            CheckOnSetParametr();
            if (!await IsCustomCheck()) return;
            IModel model = await this.model.Insert();
            await CheckResultUpdateModel(model);
        }

        private async void dbmlEditorOnUpdateChanged(object sender, EventArgs e)
        {
            IModel oldModel = CheckOnSetParametr();
            if (!await IsCustomCheck()) return;
            IModel model = await this.model.Update(oldModel);
            await CheckResultUpdateModel(model);
        }

        protected virtual void OnUpdateChanged(bool isDel = false)
        {
            UpdateChanged?.Invoke(this, isDel ? isRepair ? model : null : model);
        }

        /// <summary>
        /// Проверка кастомных разрешений
        /// </summary>
        /// <returns>Результат проверки</returns>
        private async Task<bool> IsCustomCheck()
        {
            List<string> errors = new List<string>();
            PropertyInfo[] properties = view.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                CheckAttribute checkAttribute = property.GetCustomAttribute<CheckAttribute>();

                if (checkAttribute is not null && checkAttribute.NameCustomCheckFunc is not null)
                {
                    bool isValid = await checkAttribute.GetNameCustomCheckFunc(this.model);

                    if (!isValid)
                    {
                        string errorMessage = !string.IsNullOrEmpty(checkAttribute.NotChecibleMessage)
                            ? checkAttribute.NotChecibleMessage
                            : $"Поле '{property.Name}' не прошло проверку";

                        errors.Add(errorMessage);
                    }
                }
            }

            if (errors.Count != 0) InfoViewer.AlertMessege(String.Join("\n\n", errors));

            return errors.Count == 0;
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

            Dictionary<Type, IEnumerable<int?>>  insertDict =
                dmlceEditor.DependencyParametrs
                  .ToDictionary(
                        d => d.DependencyModelType,
                        d => d.Dependencies.Where(i => i.Status == DependencyStatus.Insert)?.Select(i => i.IdDependency));

            CheckDictUpdate(insertDict);
            model.GetType().GetProperty(nameof(IForigenParent.InsertDependencies))?.SetValue(model, insertDict);

            if (dmlceEditor.EditorMode != EditorMode.Insert)
            {
                Dictionary<Type, IEnumerable<int?>> removeDict =
                    dmlceEditor.DependencyParametrs
                            .ToDictionary(
                                d => d.DependencyModelType,
                                d => d.Dependencies.Where(i => i.Status == DependencyStatus.Remove)?.Select(i => i.IdDependency));

                CheckDictUpdate(removeDict);
                model.GetType().GetProperty(nameof(IForigenParent.RemoveDependencies))?.SetValue(model, removeDict.ToDictionary(i => i.Key, i => i.Value.OfType<int>()));
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

                IView view = await model.GetView();

                updateTypeList.Clear();
                valHeader = view.GetType().GetProperty(tagHeader).GetValue(view).ToString();
                UpdateTitle();
                OnUpdateChanged();
            }
        }

        /// <summary>
        /// Проверка с занесением в список очереди на обновление
        /// </summary>
        /// <param name="updateDict">Список с обновлениями</param>
        private void CheckDictUpdate(Dictionary<Type, IEnumerable<int?>> updateDict)
        {
            if (updateDict is null) return;

            foreach (KeyValuePair<Type, IEnumerable<int?>> up in updateDict)
            {
                if (up.Value.Count() > 0 && !updateTypeList.Contains(up.Key))
                {
                    updateTypeList.Add(up.Key);
                }
            }
        }
    }
}
