using DataBaseProvaider.Enums;

namespace DataBaseProvaider.Objects
{
    /// <summary>
    /// Параметр фильтрации
    /// </summary>
    public class ConditionsParametr : IDisposable
    {
        /// <summary>
        /// Наименование колонки
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Условный оператор
        /// </summary>
        public virtual ConditionalOperators Operator { get; set; }

        /// <summary>
        /// Логический оператор следующий за условным выражением, по умолчанию <see cref="LogicOperators.None"/>
        /// </summary>
        public LogicOperators LogicOperator { get; set; } = LogicOperators.None;

        /// <summary>
        /// Сравнивоемое/Искомое значение
        /// </summary>
        public object Value { get; set; } = null;

        /// <summary>
        /// Указвает, что параметр следует считать поисковым и назначать в Value значение из поля поиск
        /// </summary>
        public virtual bool IsSerhing { get; set; } = false;

        /// <summary>
        /// Создание объекта по наименовании колонки, условному оператору и искомому/сравниваемому значению
        /// </summary>
        /// <param name="columnName">Наименование колонки</param>
        /// <param name="operators">Условный оператор</param>
        /// <param name="value">Искомое/Сравниваемое значение</param>
        public ConditionsParametr(string columnName, ConditionalOperators operators, object value) 
        { 
            ColumnName = columnName;
            Operator = operators;
            Value = value;
        }

        /// <summary>
        /// Создание объекта по наименовании колонки, условному оператору, логическому оператору и искомому/сравниваемому значению
        /// </summary>
        /// <param name="columnName">Наименование колонки</param>
        /// <param name="operators">Условный оператор</param>
        /// <param name="logic">Логический оператор</param>
        /// <param name="value">Искомое/Сравниваемое значение</param>
        public ConditionsParametr(string columnName, ConditionalOperators operators, LogicOperators logic, object value)
        {
            ColumnName = columnName;
            Operator = operators;
            LogicOperator = logic;
            Value = value;
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        [Obsolete("Этот конструктор предназначен только для использования в XAML. Используйте конструктор с параметрами.", false)]
        public ConditionsParametr() { } 

        /// <summary>
        /// Деструктор/Очистка памяти от объекта
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
