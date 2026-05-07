
namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Справочник типов зависимостей
    /// </summary>
    /// <remarks>
    /// <para>OneToMany - один ко многим</para>
    /// <para>OneToOnePicker - один к одному через выпадающий список</para>
    /// <para>OneToOneSelectionList - один к одному через выборный список</para>
    /// </remarks>
    public enum DependencyType
    {
        /// <summary>
        /// Один ко многим
        /// </summary>
        /// <value>0</value>
        OneToMany = 0,
        /// <summary>
        /// Один к одному через выпадающий список
        /// </summary>
        /// <value>1</value>
        OneToOnePicker = 1,
        /// <summary>
        /// Один к одному через выборный список
        /// </summary>
        /// <value>2</value>
        OneToOneSelectionList = 2

    }
}
