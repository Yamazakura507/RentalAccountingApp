using DataBaseProvaider.Objects;

namespace WinFormsComponents.Classes.Interface
{
    /// <summary>
    /// Делегат для обработки изменения состояния фильтра
    /// </summary>
    public delegate void FilterChangedHandler(ConditionsParametr newSearchParametr, ConditionsParametr baseSearhParametr, UpdateParametrChangedHandler updateParametr);

    /// <summary>
    /// Делегат для обрабоки обновления текущего параметра
    /// </summary>
    /// <param name="baseSearhParametr">текущий параметр</param>
    public delegate void UpdateParametrChangedHandler(ConditionsParametr baseSearhParametr);

    /// <summary>
    /// Инерфейс сервиса создания фильтров
    /// </summary>
    internal interface IFilter
    {
        /// <summary>
        /// Формирование фильтра для параметра
        /// </summary>
        /// <param name="columnText">Текст свойства фильтрации</param>
        /// <param name="columnName">Наименование параметра фильтрации</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="onFilterChanged">Обработчик включения фильтра</param>
        ToolStripMenuItem CreateFilter(string columnText, string columnName, ConditionsParametr searchParametr, FilterChangedHandler onFilterChanged = null);
    }
}
