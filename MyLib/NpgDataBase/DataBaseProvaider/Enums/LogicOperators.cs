using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Набор логических операторов
    /// </summary>
    /// <remarks>
    /// <para>And - И</para>
    /// <para>Or - ИЛИ</para>
    /// <para>None - Пустой</para>
    /// </remarks>
    public enum LogicOperators
    {
        /// <summary>
        /// Пустая вставка(конец условия)
        /// </summary>
        /// <value>0</value>
        [Comment("НЕТ")]
        [Description("")]
        None = 0,
        /// <summary>
        /// Оператор И
        /// </summary>
        /// <vlaue>1</vlaue>
        [Comment("Оператор И")]
        [Description("AND")]
        And = 1,
        /// <summary>
        /// Оператор ИЛИ
        /// </summary>
        /// <vlaue>2</vlaue>
        [Comment("Оператор ИЛИ")]
        [Description("OR")]
        Or = 2
        
    }
}
