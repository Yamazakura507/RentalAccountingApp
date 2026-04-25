using DataBaseProvaider.Attributes;

namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Перечесление для сортировки
    /// </summary>
    /// <remarks>
    /// Asc - Повозрастанию,
    /// Desc - Поубыванию
    /// </remarks>
    public enum OrderType
    {
        /// <summary>
        /// Повозрастанию
        /// </summary>
        /// <value>0</value>
        [Comment("По возрастанию")]
        Asc = 0,
        /// <summary>
        /// Поубыванию
        /// </summary>
        /// <value>1</value>
        [Comment("По убыванию")]
        Desc = 1
    }
}
