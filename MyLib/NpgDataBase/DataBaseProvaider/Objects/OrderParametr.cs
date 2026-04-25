using DataBaseProvaider.Classes.Abstract;
using DataBaseProvaider.Enums;

namespace DataBaseProvaider.Objects
{
    /// <summary>
    /// Объект сортировки колонки
    /// </summary>
    public class OrderParametr : BaseParametrCollection, IDisposable
    {
        /// <summary>
        /// Наименование сортируемой колонки
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Тип сортировки
        /// </summary>
        public OrderType OrderType { get; set; } = OrderType.Asc;

        internal bool IsTemp { get; set; } = false;

        /// <summary>
        /// Создание объекта по наименованию колонки, по умолчанию будет выстовлена сортировка по возрастанию
        /// </summary>
        /// <param name="nameColumn">Наименование колонки</param>
        public OrderParametr(string nameColumn)
        { 
            ColumnName = nameColumn;
        }

        /// <summary>
        /// Создание объекта по наименованию колонки и типу сортировки
        /// </summary>
        /// <param name="nameColumn">Наименование колонки</param>
        /// <param name="order">Тип сортировки</param>
        /// <param name="isTemp">Указывает, что OrderParametr будет стерт из коллекции после первого вызова ToStringOrders</param>
        public OrderParametr(string nameColumn, OrderType order, bool isTemp = false)
        {
            ColumnName = nameColumn;
            OrderType = order;
            IsTemp = isTemp;
        }

        /// <summary>
        /// Создание пустого объекта
        /// </summary>
        [Obsolete("Этот конструктор предназначен только для использования в XAML. Используйте конструктор с параметрами.", false)]
        public OrderParametr() { }

        /// <summary>
        /// Деструктор/Очистка памяти от объекта
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);

        public static IEnumerable<OrderParametr> operator +(IEnumerable<OrderParametr> parametrs, OrderParametr condition)
        {
            return parametrs.Append(condition);
        }

        public static IEnumerable<OrderParametr> operator -(IEnumerable<OrderParametr> parametrs, OrderParametr condition)
        {
            return parametrs.Except([condition]);
        }

        /// <summary>
        /// Сравнение объектов только по Id
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as OrderParametr);
        }

        /// <summary>
        /// Сравнение объектов только по Id (типизированная версия)
        /// </summary>
        public bool Equals(OrderParametr other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }
    }
}
