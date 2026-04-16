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
        /// Оператор И
        /// </summary>
        /// <vlaue>0</vlaue>
        [Description("AND")]
        And = 0,
        /// <summary>
        /// Оператор ИЛИ
        /// </summary>
        /// <vlaue>1</vlaue>
        [Description("OR")]
        Or = 1,
        /// <summary>
        /// Пустая вставка(конец условия)
        /// </summary>
        /// <value>2</value>
        [Description("")]
        None = 2,
    }
}
