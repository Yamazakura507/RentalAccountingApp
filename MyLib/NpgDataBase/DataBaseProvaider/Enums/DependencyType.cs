
namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Справочник типов зависимостей
    /// </summary>
    /// <remarks>
    /// <para>OneToMany - один ко многим</para>
    /// <para>OneToOne - один к одному</para>
    /// </remarks>
    public enum DependencyType
    {
        /// <summary>
        /// Один ко многим
        /// </summary>
        /// <value>0</value>
        OneToMany = 0,
        /// <summary>
        /// Один к одному
        /// </summary>
        /// <value>1</value>
        OneToOne = 1

    }
}
