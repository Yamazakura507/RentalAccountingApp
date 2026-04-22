using DataBaseProvaider.Attributes;
using System.ComponentModel;

namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Набор условных опреаторов
    /// </summary>
    /// <remarks>
    /// <para>Equal - Равно, NotEqual - Не равно</para>
    /// <para>More - Больше, Less - Меньше</para>
    /// <para>MoreOrEqual - Больше или равно, LessOrEqual - Меньше или равно</para>
    /// <para>Like - Содержит(регистро зависимый), ILike - Cодержит(регистро не зависимый)</para>
    /// <para>ExactLike - Содержит(регистро зависимый, точный), ExactILike - Cодержит(регистро не зависимый, точный)</para>
    /// </remarks>
    public enum ConditionalOperators
    {
        /// <summary>
        /// Равенство - операция =
        /// </summary>
        /// <value>0</value>
        [Comment("Равно")]
        [Description("=")]
        Equal = 0,
        /// <summary>
        /// Не равенство - операция !=
        /// </summary>
        /// <value>1</value>
        [Comment("Не равно")]
        [Description("<>")]
        NotEqual = 1,
        /// <summary>
        /// Больше - операция &gt;
        /// </summary>
        /// <value>2</value>
        [Comment("Больше")]
        [Description(">")]
        More = 2,
        /// <summary>
        /// Меньше - операция &lt; 
        /// </summary>
        /// <value>3</value>
        [Comment("Меньше")]
        [Description("<")]
        Less = 3,
        /// <summary>
        /// Больше или равно - операция &gt;=
        /// </summary>
        /// <value>4</value>
        [Comment("Больше")]
        [Description(">=")]
        MoreOrEqual = 4,
        /// <summary>
        /// Меньше или равно - операция &lt;=
        /// </summary>
        /// <value>5</value>
        [Comment("Меньше")]
        [Description("<=")]
        LessOrEqual = 5,
        /// <summary>
        /// Содержит(регистро не зависимый) - операция ilike/contains
        /// </summary>
        /// <value>6</value>
        [Comment("Содержит")]
        [Description("ilike")]
        ILike = 6,
        /// <summary>
        /// Содержит(регистро зависимый) - операция like/contains
        /// </summary>
        /// <value>7</value>
        [Comment("Содержит")]
        [Description("like")]
        Like = 7,
        /// <summary>
        /// Содержит(регистро зависимый) - операция like/contains
        /// </summary>
        /// <remarks>
        /// Точное совпадение
        /// </remarks>
        /// <value>8</value>
        [Comment("Содержит")]
        [Description("like")]
        ExactLike = 8,
        /// <summary>
        /// Содержит(регистро не зависимый) - операция ilike/contains
        /// </summary>
        /// <remarks>
        /// Точное совпадение
        /// </remarks>
        /// <value>9</value>
        [Comment("Содержит")]
        [Description("ilike")]
        ExactILike = 9,
        /// <summary>
        /// Искать через растояние левинштейна
        /// </summary>
        /// <remarks>
        /// Этот поиск может оказаться медленее обычного Like или Ilike
        /// </remarks>
        /// <value>10</value>
        [Comment("По левенштейну")]
        [Description("levenshtein_less_equal")]
        Levenshtein = 10
    }
}
