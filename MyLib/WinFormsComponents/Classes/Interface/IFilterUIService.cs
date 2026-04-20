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
    /// Инерфейс сервиса создания UI фильтров
    /// </summary>
    internal interface IFilterUIService
    {
        /// <summary>
        /// Создание поискового фильтра
        /// </summary>
        ToolStripMenuItem CreateSearchFilter(string columnText, string columnName, ConditionsParametr searchParametr, FilterChangedHandler onFilterChanged = null);
    }
}
