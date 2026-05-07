using DataBaseProvaider.Classes.Abstract;
using DataBaseProvaider.Enums;

namespace DataBaseProvaider.Objects
{
    /// <summary>
    /// Параметр фильтрации
    /// </summary>
    public class ConditionsParametr : BaseParametrCollection, IDisposable, ICloneable
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
        /// <remarks>
        /// Для оператора Beetwen - значением будет <see cref="object"/>[] из двух значений от и до
        /// Для оператора In - значением будет <see cref="IList"/>
        /// </remarks>
        public object Value { get; set; } = null;

        /// <summary>
        /// Указвает, что параметр следует считать поисковым и назначать в Value значение из поля поиск
        /// </summary>
        public virtual bool IsSerhing { get; set; } = false;

        /// <summary>
        /// Тип сравниваемого параметра
        /// </summary>
        public virtual Type Type { get; set; } = typeof(string);

        /// <summary>
        /// Создание объекта по наименовании колонки, условному оператору и искомому/сравниваемому значению
        /// </summary>
        /// <param name="columnName">Наименование колонки</param>
        /// <param name="operators">Условный оператор</param>
        /// <param name="value">Искомое/Сравниваемое значение<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.Between"/> - 
        /// значением <paramref name="value"/> будет <see cref="object"/>[] из двух значений от и до<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.In"/> - 
        /// значением <paramref name="value"/> будет <see cref="IList"/>
        /// </param>
        /// <remarks>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.Between"/> - 
        /// значением <paramref name="value"/> будет <see cref="object"/>[] из двух значений от и до<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.In"/> - 
        /// значением <paramref name="value"/> будет <see cref="IList"/>
        /// </remarks>
        public ConditionsParametr(string columnName, ConditionalOperators operators, object value = null) 
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
        /// <param name="value">Искомое/Сравниваемое значение<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.Between"/> - 
        /// значением <paramref name="value"/> будет <see cref="object"/>[] из двух значений от и до<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.In"/> - 
        /// значением <paramref name="value"/> будет <see cref="IList"/>
        /// </param>
        /// <remarks>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.Between"/> - 
        /// значением <paramref name="value"/> будет <see cref="object"/>[] из двух значений от и до<br/>
        /// Для оператора <paramref name="operators"/> <see cref="ConditionalOperators.In"/> - 
        /// значением <paramref name="value"/> будет <see cref="IList"/>
        /// </remarks>
        public ConditionsParametr(string columnName, ConditionalOperators operators, LogicOperators logic, object value = null)
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

        public static IEnumerable<ConditionsParametr> operator +(IEnumerable<ConditionsParametr> parametrs, ConditionsParametr condition)
        {
            return parametrs.Append(condition);
        }

        public static IEnumerable<ConditionsParametr> operator -(IEnumerable<ConditionsParametr> parametrs, ConditionsParametr condition)
        {
            return parametrs.Except([ condition ]);
        }

        /// <summary>
        /// Сравнение объектов только по Id
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ConditionsParametr);
        }

        /// <summary>
        /// Сравнение объектов только по Id (типизированная версия)
        /// </summary>
        public bool Equals(ConditionsParametr other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        /// <summary>
        /// Создает глубокую копию объекта
        /// </summary>
        object ICloneable.Clone() => Clone();

        /// <summary>
        /// Создает глубокую копию объекта
        /// </summary>
        public virtual ConditionsParametr Clone() =>  new(ColumnName, Operator, LogicOperator, Value) { IsSerhing = IsSerhing, Type = Type };
    }
}
