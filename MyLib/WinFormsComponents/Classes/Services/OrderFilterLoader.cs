using DataBaseProvaider.Classes.Abstract;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using WinFormsComponents.Classes.Interface;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Сервис визуализации сортировок
    /// </summary>
    public class OrderFilterLoader : IFilter
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
        /// Конструктор сервиса визуализации сортировок
        /// </summary>
        /// <param name="filterOffColor">Цвет отключенного фильтра</param>
        /// <param name="filterOnColor">Цвет включенного фильтра</param>
        public OrderFilterLoader(Color filterOffColor, Color filterOnColor)
        {
            this.filterOffColor = filterOffColor;
            this.filterOnColor = filterOnColor;
        }

        /// <summary>
        /// Формирование поискового фильтра для параметра
        /// </summary>
        /// <param name="columnText">Текст свойства сортировки</param>
        /// <param name="columnName">Наименование параметра сортировки</param>
        /// <param name="orderParametr">Имеющиеся сведенья о сортировке по выбраному параметру</param>
        /// <param name="onFilterChanged">Обработчик включения сортировки</param>
        /// <param name="settingFilter">Настройка фильтра</param>
        public ToolStripMenuItem CreateFilter(string columnText, string columnName, BaseParametrCollection orderParametr, Type parametrType, FilterChangedHandler onFilterChanged = null, SettingFilter settingFilter = null)
        {
            OrderParametr baseParametr = (OrderParametr)orderParametr;

            ToolStripMenuItem menuItem = new(columnText, 
                                                baseParametr is null 
                                                    ? Properties.Resources.order 
                                                    : baseParametr.OrderType == OrderType.Asc 
                                                        ? Properties.Resources.order_asc
                                                        : Properties.Resources.order_desc);

            Dictionary<bool, (string, string, Color)> checkItemParametrs = new()
            {
                { false, ("Включить", "Включить в параметры сортировки", filterOffColor) },
                { true, ("Выключить", "Исключить из параметров сортировки", filterOnColor) }
            };
            Dictionary<bool, (string, string, Color)>  checkTempParametrs = new()
            {
                { false, ("Не постоянный", "Параметры сортировки применяеться один раз в моменте", filterOffColor) },
                { true, ("Постоянный", "Параметры сортировки применяеться постоянно", filterOnColor) }
            };

            ToolStripMenuItem checkItem = checkItemParametrs.CreateOptionItem(orderParametr != null);
            ToolStripComboBox comboBoxOrderType = FilterFunction.ComboBoxFilterLoad<OrderType>(checkItem.Checked, orderParametr, "Тип сортировки");
            ToolStripMenuItem checkTemp = checkTempParametrs.CreateOptionItem(orderParametr != null || checkItem.Checked);

            menuItem.DropDown.Closing += (s, e) =>
            {
                if (e.CloseReason.Equals(ToolStripDropDownCloseReason.ItemClicked)) e.Cancel = true;
                else
                {
                    onFilterChanged?.Invoke(checkItem.Checked ? CreateOrderParametr(menuItem, columnName) : null, baseParametr, (p) => baseParametr = (OrderParametr)p);
                    checkItem.Checked = checkItem.Checked && checkTemp.Checked;
                }
            };
            checkItem.CheckedChanged += (s, e) =>
            {
                checkItem.CheckedChangedItemMenu(checkItemParametrs);
                comboBoxOrderType.Enabled = checkTemp.Enabled = checkItem.Checked;
                menuItem.Image = checkItem.Checked
                                    ? GetSelectedOperator(comboBoxOrderType, orderParametr as OrderParametr) == OrderType.Asc
                                        ? Properties.Resources.order_asc
                                        : Properties.Resources.order_desc
                                    : Properties.Resources.order;
            };
            comboBoxOrderType.SelectedIndexChanged += (s, e) =>
            {
                OrderType operators = GetSelectedOperator(comboBoxOrderType, orderParametr as OrderParametr);

                menuItem.Image = operators == OrderType.Asc
                                    ? Properties.Resources.order_asc
                                    : Properties.Resources.order_desc;
            };

            menuItem.DropDownItems.Add(checkItem);
            menuItem.DropDownItems.Add(comboBoxOrderType);
            menuItem.DropDownItems.Add(checkTemp);

            return menuItem;
        }

        /// <summary>
        /// Получение(сборка) сортировочного параметра по актуальному фильтру
        /// </summary>
        /// <param name="menuItem">Элемент меню к которому относится сортировочный фильтр</param>
        /// <param name="columnName">Наименование параметра сортировки</param>
        /// <returns></returns>
        private OrderParametr CreateOrderParametr(ToolStripMenuItem menuItem, string columnName)
        {
            string valOper = ((ToolStripComboBox)menuItem.DropDownItems[1]).SelectedItem.ToString();
            bool isTemp = !((ToolStripMenuItem)menuItem.DropDownItems[2]).Checked;

            OrderType operators = Extensions.GetCommitEnumDictionary<OrderType>().First(i => i.Value.Equals(valOper)).Key;

            return new(columnName, operators, isTemp);
        }

        /// <summary>
        /// Получение выбранного оператора
        /// </summary>
        /// <param name="comboBox">Выпаадющий список</param>
        /// <param name="orderParametr">Имеющиеся сведенья о сортировке по выбраному параметру</param>
        /// <returns>Выбранный оператор</returns>
        private OrderType GetSelectedOperator(ToolStripComboBox comboBox, OrderParametr orderParametr)
        {
            Dictionary<OrderType, string> operators = Extensions.GetCommitEnumDictionary<OrderType>();

            return orderParametr?.OrderType ??
                operators.First(kvp => kvp.Value.Equals(comboBox.SelectedItem)).Key;
        }
    }
}
