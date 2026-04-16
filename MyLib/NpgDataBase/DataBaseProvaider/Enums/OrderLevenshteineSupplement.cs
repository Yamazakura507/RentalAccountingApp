namespace DataBaseProvaider.Enums
{
    /// <summary>
    /// Набор указателей размещенгия сортировки по дополнению условия на поиск левинштейна
    /// </summary>
    public enum OrderLevenshteineSupplement
    {
        /// <summary>
        /// Не включать в сортировку
        /// </summary>
        /// <value>0</value>
        None = 0,
        /// <summary>
        /// Указать высший приоритет сортировки
        /// </summary>
        /// <value>1</value>
        Priority = 1,
        /// <summary>
        /// Указать низший приоритет сортировки
        /// </summary>
        /// <value>2</value>
        EndList = 2,
        /// <summary>
        /// Указать точный приоритет сортировки
        /// </summary>
        /// <value>3</value>
        Position = 3
    }
}
