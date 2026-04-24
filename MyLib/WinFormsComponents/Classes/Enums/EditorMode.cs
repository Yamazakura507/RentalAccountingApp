namespace WinFormsComponents.Classes.Enums
{
    /// <summary>
    /// Режимы редактирования модели
    /// </summary>
    /// <remarks>
    /// <para>Insert - Добавление</para>
    /// <para>Update - Изменение</para>
    /// <para>UpdateOrDelete - Изменение и удаление</para>
    /// <para>UpdateOrRepit - Изменение и востановление</para>
    /// </remarks>
    public enum EditorMode
    {
        /// <summary>
        /// Добавление
        /// </summary>
        /// <value>0</value>
        Insert = 0,
        /// <summary>
        /// Изменение
        /// </summary>
        /// <value>1</value>
        Update = 1,
        /// <summary>
        /// Изменение и удаление
        /// </summary>
        /// <value>2</value>
        UpdateOrDelete = 2,
        /// <summary>
        /// Изменение и востановление
        /// </summary>
        /// <value>3</value>
        UpdateOrRepair = 3
    }
}
