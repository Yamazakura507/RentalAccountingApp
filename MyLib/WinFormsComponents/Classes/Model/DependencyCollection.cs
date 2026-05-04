using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Reflection;
using WinFormsComponents.Classes.Enums;

namespace WinFormsComponents.Classes.Model
{
    /// <summary>
    /// Зависимость
    /// </summary>
    public class DependencyCollection
    {
        /// <summary>
        /// Тэг зависимости
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Подпись зависимости
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Первичный ключ зависимости
        /// </summary>
        public string PKDependency { get; set; } = "Id";

        /// <summary>
        /// Ключ иконки
        /// </summary>
        public string ImageKey { get; set; } = null;

        /// <summary>
        /// Колеекция первичных ключей зависимости
        /// </summary>
        public List<DependencyInfo> Dependencies { get; set; } = new ();

        /// <summary>
        /// Колеекция первичных ключей зависимости первоначальная
        /// </summary>
        public List<DependencyInfo> BaseDependencies { get; private set; } = new ();

        /// <summary>
        /// Тип представления модели зависимости
        /// </summary>
        public Type DependencyViewType { get; set; }

        /// <summary>
        /// Тип модели зависимости
        /// </summary>
        public Type DependencyModelType { get; set; }

        /// <summary>
        /// Цвет добавляемых строк
        /// </summary>
        public Color InsertRowColor { get; set; } = Color.YellowGreen;
        /// <summary>
        /// Цвет удаляемых строк
        /// </summary>
        public Color RemoveRowColor { get; set; } = Color.MistyRose;
        /// <summary>
        /// Цвет строк по умолчанию
        /// </summary>
        public Color DefaultRowColor { get; set; } = Color.White;

        /// <summary>
        /// Тип зависимости
        /// </summary>
        public DependencyType DependencyType { get; set; }

        /// <summary>
        /// Поле отображение состояния списка зависимостей, если он был изменен от оригинала верент true иначе false
        /// </summary>
        public bool IsEditingDependensies => !Dependencies.All(i => i.Status == DependencyStatus.Default);

        /// <summary>
        /// Событие обновления списка
        /// </summary>
        public EventHandler UpdateChange { get; set; }

        /// <summary>
        /// Конструктор объекта зависимости
        /// </summary>
        /// <param name="title">Заголовок зависиомсти</param>
        /// <param name="dependencyViewType">Тип модели зависимости</param>
        /// <param name="dependencyType">Тип зависимости</param>
        public DependencyCollection(string tag, string title, Type dependencyViewType, DependencyType dependencyType)
        {
            Tag = tag;
            Title = title;
            DependencyViewType = dependencyViewType;
            DependencyType = dependencyType;
        }

        /// <summary>
        /// Конструктор объекта зависимости
        /// </summary>
        /// <param name="title">Заголовок зависиомсти</param>
        /// <param name="pKDependency">Поле первичного ключа</param>
        /// <param name="dependencyViewType">Тип модели зависимости</param>
        /// <param name="dependencyType">Тип зависимости</param>
        public DependencyCollection(string tag, string title, string pKDependency, Type dependencyViewType, DependencyType dependencyType)
        {
            Tag = tag;
            Title = title;
            PKDependency = pKDependency;
            DependencyViewType = dependencyViewType;
            DependencyType = dependencyType;
        }

        /// <summary>
        /// Запись фиксация списка объектов зависимости
        /// </summary>
        public void SetDependesiesToBase() => BaseDependencies = Dependencies.Select(i => new DependencyInfo(i.IdDependency, i.Status)).ToList();

        /// <summary>
        /// Запись востановление оригинального списка объектов зависимости
        /// </summary>
        public void SetBaseToDependesies() => Dependencies = BaseDependencies.Select(i => new DependencyInfo(i.IdDependency, i.Status)).ToList();

        /// <summary>
        /// Обновление списка
        /// </summary>
        public void UpdateDependesiesToBase()
        {
            foreach (DependencyInfo dependency in Dependencies.ToList())
            {
                switch (dependency.Status)
                {
                    case DependencyStatus.Insert:
                        dependency.Status = DependencyStatus.Default;
                        break;
                    case DependencyStatus.Remove:
                        Dependencies.Remove(dependency);
                        break;
                }
            }

            SetDependesiesToBase();
        }

        /// <summary>
        /// Получение объекта условного оператора
        /// </summary>
        /// <param name="operators">Условный оператор</param>
        /// <returns>Объект условного оператора</returns>
        public ConditionsParametr GetCondition(ConditionalOperators operators)
        {
            List<int> dependencies;

            if (operators == ConditionalOperators.NotIn)
            {
                dependencies = Dependencies.Where(i => i.Status != DependencyStatus.Remove).Select(i => i.IdDependency).ToList();

                if (dependencies.Count == 0) return null;
            }
            else
            {
                dependencies = Dependencies.Select(i => i.IdDependency).ToList();
            }

            return new(PKDependency, operators, LogicOperators.Or, dependencies);
        }

        /// <summary>
        /// Изменение коллекции условных операторов
        /// </summary>
        /// <param name="conditions">Коллекция условных операторов</param>
        /// <param name="operators">Условный опрератор</param>
        public void EditConditon(CollectionParametrs parametrs, ConditionalOperators operators)
        {
            ConditionsParametr nowParametr = parametrs.Conditions.FirstOrDefault(i => i.ColumnName.Equals(PKDependency));

            if (nowParametr is not null)
            {
                parametrs.Conditions -= nowParametr;
            }

            if (Dependencies.Count() > 0)
            {
                nowParametr = GetCondition(operators);

                if (nowParametr is not null)
                {
                    parametrs.Conditions += nowParametr;
                }
            }
            else if (operators == ConditionalOperators.In)
            {
                parametrs.Conditions += new ConditionsParametr(PKDependency, ConditionalOperators.Equal, null);
            }
        }

        /// <summary>
        /// Флюгер параметров
        /// </summary>
        /// <param name="summatorParametrs">Список на сумирование</param>
        /// <param name="substractorParametrs">Список на вычитание</param>
        public void FlagingEditCondition(CollectionParametrs summatorParametrs, CollectionParametrs substractorParametrs)
        {
            EditConditon(summatorParametrs, ConditionalOperators.In);
            EditConditon(substractorParametrs, ConditionalOperators.NotIn);
        }

        /// <summary>
        /// Форматирование списка перед визуализацией
        /// </summary>
        /// <param name="items">Список</param>
        public void FormatingDependenciesViewList(BindingList<object> items)
        {
            if (items.Count == 0) return;
            
            PropertyInfo propertyColor = items[0].GetType().GetProperties().FirstOrDefault(i => i.GetCustomAttribute<ViewModelAttribute>().BackColor);

            if (propertyColor is null) return;

            PropertyInfo propertyPK = items[0].GetType().GetProperty(PKDependency);

            foreach (object item in items)
            {
                DependencyInfo dependencyInfo = Dependencies.FirstOrDefault(i => i.IdDependency.Equals(propertyPK.GetValue(item)));

                if (dependencyInfo is not null)
                {
                    Color color = DefaultRowColor;

                    switch (dependencyInfo.Status)
                    {
                        case DependencyStatus.Insert:
                            color = InsertRowColor;
                            break;
                        case DependencyStatus.Remove:
                            color = RemoveRowColor;
                            break;
                    }

                    propertyColor.SetValue(item, color);
                }
            }
        }

        /// <summary>
        /// Добавление объекта зависимости
        /// </summary>
        /// <param name="dependency">Объект зависимости</param>
        public void Add(DependencyInfo dependency) => Dependencies.Add(dependency);

        /// <summary>
        /// Добавление объекта зависимости
        /// </summary>
        /// <param name="idDependency">Идентификатор объекта зависимоти</param>
        public void Add(int idDependency)
        {
            DependencyInfo nowDependency = Dependencies.FirstOrDefault(i => i.IdDependency == idDependency);

            if (nowDependency is null) Dependencies.Add(new(idDependency, DependencyStatus.Insert));
            else nowDependency.Status = DependencyStatus.Default;
        }

        /// <summary>
        /// Добавление объектов зависимости
        /// </summary>
        /// <param name="dependencies">Объекты зависимоти</param>
        public void AddRange(IEnumerable<DependencyInfo> dependencies) => Dependencies.AddRange(dependencies);

        /// <summary>
        /// Добавление объектов зависимости
        /// </summary>
        /// <param name="idDependencies">Идентификаторы объектов зависимоти</param>
        public void AddRange(IEnumerable<int> idDependencies)
        {
            foreach (int idDependency in idDependencies)
            {
                Add(idDependency);
            }
        }

        /// <summary>
        /// Удаление объекта зависимости
        /// </summary>
        /// <param name="dependency">Объект зависимости</param>
        public void Remove(DependencyInfo dependency) => Dependencies.Remove(dependency);

        /// <summary>
        /// Удаление/востановление объекта зависимости
        /// </summary>
        /// <param name="idDependency">Идентификатор объекта зависимоти</param>
        public void RemoveOrRpair(int idDependency) 
        {
            DependencyInfo nowDependency = Dependencies.First(i => i.IdDependency == idDependency);

            switch (nowDependency.Status)
            {
                case DependencyStatus.Insert:
                    Dependencies.Remove(nowDependency);
                    break;
                case DependencyStatus.Remove:
                    nowDependency.Status = DependencyStatus.Default;
                    break;
                case DependencyStatus.Default:
                    nowDependency.Status = DependencyStatus.Remove;
                    break;
            }
        }

        /// <summary>
        /// Удаление объектов зависимости
        /// </summary>
        /// <param name="dependencies">Объекты зависимоти</param>
        public void RemoveRange(IEnumerable<DependencyInfo> dependencies) => Dependencies.RemoveAll(i => dependencies.Contains(i));

        /// <summary>
        /// Удаление/востановление объектов зависимости
        /// </summary>
        /// <param name="idDependencies">Идентификаторы объектов зависимоти</param>
        public void RemoveOrRepairRange(IEnumerable<int> idDependencies)
        {
            foreach (int idDependency in idDependencies)
            {
                RemoveOrRpair(idDependency);
            }
        }

        public void OnUpdateChange() => UpdateChange?.Invoke(this, EventArgs.Empty);
    }
}
