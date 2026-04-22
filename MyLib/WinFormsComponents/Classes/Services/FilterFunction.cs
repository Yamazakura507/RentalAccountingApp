using DataBaseProvaider.Objects;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Вспомогательный клас для создания фильтров
    /// </summary>
    public static class FilterFunction
    {
        /// <summary>
        /// Создание чекита для меню
        /// </summary>
        /// <param name="parametrs">Набор параметров с разделением по чекит статусу</param>
        /// <param name="isChecked">Состояние</param>
        /// <returns>Чекит для меню</returns>
        public static ToolStripMenuItem CreateOptionItem(this Dictionary<bool,(string text, string tooltip, Color color)> parametrs, bool isChecked)
        {
            ToolStripMenuItem item = new(parametrs[false].text)
            {
                CheckOnClick = true,
                ToolTipText = parametrs[false].tooltip,
                CheckState = CheckState.Unchecked,
                Image = Properties.Resources.uncheckible,
                BackColor = parametrs[false].color,
                Checked = false
            };

            item.CheckedChanged += (sender, e) =>
            {
                CheckedChangedItemMenu(item, parametrs);
            };

            item.Checked = isChecked;

            return item;
        }

        /// <summary>
        /// Метод базовой работы события при смене чекита у элемента меню
        /// </summary>
        /// <param name="item">Элемент меню</param>
        /// <param name="parametrs">Набор параметров с разделением по чекит статусу</param>
        public static void CheckedChangedItemMenu(this ToolStripMenuItem item, Dictionary<bool, (string text, string tooltip, Color color)> parametrs)
        {
            item.Image = item.Checked ? Properties.Resources.checkible : Properties.Resources.uncheckible;

            item.BackColor = parametrs[item.Checked].color;
            item.ToolTipText = parametrs[item.Checked].tooltip;
            item.Text = parametrs[item.Checked].text;
        }

        /// <summary>
        /// Формирование выпадающего списка фильтрации
        /// </summary>
        /// <typeparam name="TOperation">Тип <see cref="Enum"/> пеерчисления</typeparam>
        /// <param name="isEnable">Информация о видимости</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="toolText">Подсказка</param>
        public static ToolStripComboBox ComboBoxFilterLoad<TOperation>(bool isEnable, ConditionsParametr searchParametr, string toolText = "") where TOperation : Enum
        {
            ToolStripComboBox comboBox = new()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = isEnable,
                ToolTipText = toolText
            };
            Dictionary<TOperation, string> dicOper = Extensions.GetCommitEnumDictionary<TOperation>();
            string selectedItem = null;

            comboBox.Items.AddRange(dicOper.Values.Distinct().ToArray());

            if (isEnable)
            {
                TOperation operationKey = (TOperation)typeof(ConditionsParametr).GetProperties()
                    .First(i => i.PropertyType.Equals(typeof(TOperation))).GetValue(searchParametr);
                selectedItem = dicOper[operationKey];
            }

            if (selectedItem is null) comboBox.SelectedIndex = 0;
            else comboBox.SelectedItem = selectedItem;

            return comboBox;
        }
    }
}
