namespace WinFormsComponents.Classes.Enums
{
    /// <summary>
    /// Перечисление состояний отображения удаленных строк
    /// </summary>
    /// <remarks>
    /// <para>Always - Отобразить все строки</para>
    /// <para>ExecRemoving - Отобразить только удаленные</para>
    /// <para>ExecNotRemoving - Отобразить только не удаленные</para>
    /// </remarks>
    public enum ShowRemooving
    {
        /// <summary>
        /// Отобразить все строки
        /// </summary>
        /// <value>0</value>
        Always = 0,
        /// <summary>
        /// Отобразить только удаленные
        /// </summary>
        /// <value>1</value>
        ExecRemoving = 1,
        /// <summary>
        /// Отобразить только не удаленные
        /// </summary>
        /// <value>2</value>
        ExecNotRemoving = 2
    }
}
