using DataBaseProvaider.Classes.Abstract;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using WinFormsComponents.Classes.Interface;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Classes.Services
{
    public class FilterLoader : IFilter
    {
        /// <summary>
        /// Цвет отключенного фильтра
        /// </summary>
        private readonly Color filterOffColor;
        /// <summary>
        /// Цвет включенного фильтра
        /// </summary>
        private readonly Color filterOnColor;

        /// <summary>
        /// Конструктор сервиса визуализации фильтров
        /// </summary>
        /// <param name="filterOffColor">Цвет отключенного фильтра</param>
        /// <param name="filterOnColor">Цвет включенного фильтра</param>
        public FilterLoader(Color filterOffColor, Color filterOnColor)
        {
            this.filterOffColor = filterOffColor;
            this.filterOnColor = filterOnColor;
        }

        /// <summary>
        /// Формирование поискового фильтра для параметра
        /// </summary>
        /// <param name="columnText">Текст свойства фильтрации</param>
        /// <param name="columnName">Наименование параметра фильтрации</param>
        /// <param name="filterParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="onFilterChanged">Обработчик включения фильтра</param>
        /// <param name="settingFilter">Настройка фильтра</param>
        public ToolStripMenuItem CreateFilter(string columnText, string columnName, BaseParametrCollection filterParametr, Type parametrType, FilterChangedHandler onFilterChanged = null, SettingFilter settingFilter = null)
        {
            ConditionsParametr baseParametr = (ConditionsParametr)filterParametr;
            ToolStripMenuItem menuItem = new(columnText, Properties.Resources.searh);

            Dictionary<bool, (string, string, Color)> checkItemParametrs = new()
            {
                { false, ("Включить", "Включить в параметры фильтрации", filterOffColor) },
                { true, ("Выключить", "Исключить из параметров фильтрации", filterOnColor) }
            };

            ToolStripMenuItem checkItem = checkItemParametrs.CreateOptionItem(filterParametr != null);
            ToolStripControlHost[] controlsHost = GetControlsToTypeOut(parametrType, checkItem.Checked, (ConditionsParametr)filterParametr, settingFilter);

            menuItem.DropDown.Closing += (s, e) =>
            {
                if (e.CloseReason.Equals(ToolStripDropDownCloseReason.ItemClicked)) e.Cancel = true;
                else
                {
                    onFilterChanged?.Invoke(checkItem.Checked ? CreateConditionParametr(menuItem, columnName, parametrType) : null, baseParametr, (p) => baseParametr = (ConditionsParametr)p);
                }
            };
            checkItem.CheckedChanged += (s, e) =>
            {
                checkItem.CheckedChangedItemMenu(checkItemParametrs);

                foreach (ToolStripControlHost control in controlsHost)
                {
                    control.Control.Enabled = checkItem.Checked;
                }
            };

            menuItem.DropDownItems.Add(checkItem);
            menuItem.DropDownItems.AddRange(controlsHost);

            return menuItem;
        }

        /// <summary>
        /// Получение списка контролов фильтрации по типу параметра
        /// </summary>
        /// <param name="type">Тип параметар</param>
        /// <param name="isEnable">Состояние</param>
        /// <param name="filterParametr">Текущий фильтр</param>
        /// <returns>Список контролов фильтрации</returns>
        private ToolStripControlHost[] GetControlsToTypeOut(Type type, bool isEnable, ConditionsParametr filterParametr, SettingFilter settingFilter)
        {
            object max = filterParametr is null ? Activator.CreateInstance(type) : ((object[])filterParametr.Value)[0];
            object min = filterParametr is null ? Activator.CreateInstance(type) : ((object[])filterParametr.Value)[1];

            return type switch
            {
                Type t when t.Equals(typeof(double)) || t.Equals(typeof(int)) => 
                [
                    new (new Label() { Text = "Минимальное значение", Enabled = isEnable }),
                    new (new NumericUpDown() 
                    { 
                        DecimalPlaces = t.Equals(typeof(double)) ? 2 : 0, 
                        Enabled = isEnable, 
                        Value = Convert.ToDecimal(min),
                        Maximum = Convert.ToDecimal(settingFilter?.Maximum ?? 1000),
                        Minimum = Convert.ToDecimal(settingFilter?.Minimum ?? 0)
                    }),
                    new (new Label() { Text = "Максимальное значение", Enabled = isEnable }),
                    new (new NumericUpDown() 
                    { 
                        DecimalPlaces = t.Equals(typeof(double)) ? 2 : 0, 
                        Enabled = isEnable, 
                        Value = Convert.ToDecimal(max),
                        Maximum = Convert.ToDecimal(settingFilter?.Maximum ?? 1000),
                        Minimum = Convert.ToDecimal(settingFilter?.Minimum ?? 0)
                    })
                ]
            };
        }

        /// <summary>
        /// Получение(сборка) поискового параметра по актуальному фильтру
        /// </summary>
        /// <param name="menuItem">Элемент меню к которому относится поисковой фильтр</param>
        /// <param name="columnName">Наименование параметра поиска</param>
        /// <param name="typeParametr">Тип фильтруемого параметар</param>
        /// <returns></returns>
        private ConditionsParametr CreateConditionParametr(ToolStripMenuItem menuItem, string columnName, Type typeParametr)
        {
            ToolStripControlHost tschMin = (ToolStripControlHost)menuItem.DropDownItems[2];
            ToolStripControlHost tschMax = (ToolStripControlHost)menuItem.DropDownItems[4];

            object max = null;
            object min = null;

            switch (typeParametr)
            {
                case Type t when t.Equals(typeof(double)) || t.Equals(typeof(int)):
                    max = ((NumericUpDown)tschMax.Control).Value;
                    min = ((NumericUpDown)tschMin.Control).Value;
                    break;
            }

            object[] filter = [min, max];

            return new ConditionsParametr(columnName, ConditionalOperators.Between, LogicOperators.And, filter)
            { 
                Type = typeParametr
            };
        }
    }
}
