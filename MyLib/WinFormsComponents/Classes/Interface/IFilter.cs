using DataBaseProvaider.Classes.Abstract;
using DataBaseProvaider.Objects;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Classes.Interface
{
    /// <summary>
    /// Делегат для обработки изменения состояния фильтра
    /// </summary>
    public delegate void FilterChangedHandler(BaseParametrCollection newSearchParametr, BaseParametrCollection baseSearhParametr, UpdateParametrChangedHandler updateParametr);

    /// <summary>
    /// Делегат для обрабоки обновления текущего параметра
    /// </summary>
    /// <param name="baseSearhParametr">текущий параметр</param>
    public delegate void UpdateParametrChangedHandler(BaseParametrCollection baseSearhParametr);

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
        /// <param name="parametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="parametrType">Тип значения параметра</param>
        /// <param name="onFilterChanged">Обработчик включения фильтра</param>
        ToolStripMenuItem CreateFilter(string columnText, string columnName, BaseParametrCollection parametr, Type parametrType, FilterChangedHandler onFilterChanged = null, SettingFilter settingFilter = null);
    }
}
