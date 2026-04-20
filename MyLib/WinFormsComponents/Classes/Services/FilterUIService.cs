using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.Reflection;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Сервис визуализации фильтров
    /// </summary>
    public class FilterUIService : IFilterUIService
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
        /// Хранилище текущего параметра
        /// </summary>
        private ConditionsParametr baseParametr;

        /// <summary>
        /// Конструктор сервиса визуализации фильтров
        /// </summary>
        /// <param name="filterOffColor">Цвет отключенного фильтра</param>
        /// <param name="filterOnColor">Цвет включенного фильтра</param>
        public FilterUIService(Color filterOffColor, Color filterOnColor)
        {
            this.filterOffColor = filterOffColor;
            this.filterOnColor = filterOnColor;
        }

        /// <summary>
        /// Формирование поискового фильтра для параметра
        /// </summary>
        /// <param name="columnText">Текст свойства фильтрации</param>
        /// <param name="columnName">Наименование параметра фильтрации</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="onFilterChanged">Обработчик включения фильтра</param>
        public ToolStripMenuItem CreateSearchFilter(string columnText, string columnName, ConditionsParametr searchParametr, FilterChangedHandler onFilterChanged = null)
        {
            baseParametr = searchParametr;
            ToolStripMenuItem menuItem = new(columnText, Properties.Resources.searh);

            ToolStripMenuItem checkItem = CreateCheckItem(searchParametr != null);
            ToolStripComboBox comboBoxSearchType = ComboBoxFilterLoad<ConditionalOperators>(checkItem.Checked, searchParametr, "Условие поиска");
            ToolStripComboBox comboBoxLogicType = ComboBoxFilterLoad<LogicOperators>(checkItem.Checked, searchParametr, "Логическое соединение условий поиска");

            menuItem.DropDown.Closing += (s, e) =>
            {
                if (e.CloseReason.Equals(ToolStripDropDownCloseReason.ItemClicked)) e.Cancel = true;
                else
                {
                    onFilterChanged?.Invoke(checkItem.Checked ? CreateConditionSearhParametr(menuItem, columnName) : null, baseParametr, UpdateInfoBaseParametr);
                }
            };
            checkItem.CheckedChanged += (s, e) =>
            {
                comboBoxSearchType.Enabled = comboBoxLogicType.Enabled = checkItem.Checked;
                UpdateCheckItemAppearance(checkItem);
            };
            comboBoxSearchType.SelectedIndexChanged += (s, e) =>
            {
                menuItem.DropDownItems.CutToolStripCollection(2, 1);

                ConditionalOperators operators = GetSelectedOperator(comboBoxSearchType, searchParametr);
                List<ToolStripItem> settingItems = CreateAdvancedFilterItems(checkItem.Checked, searchParametr, operators);
                comboBoxSearchType.EnabledChanged += (s, e) => settingItems.ForEach(i => i.Visible = comboBoxSearchType.Enabled);

                for (int i = 0; i < settingItems.Count; i++)
                {
                    menuItem.DropDownItems.Insert(i+2, settingItems[i]);
                }
            };

            menuItem.DropDownItems.Add(checkItem);
            menuItem.DropDownItems.Add(comboBoxSearchType);
            menuItem.DropDownItems.Add(comboBoxLogicType);

            comboBoxSearchType.GetType()
                .GetMethod("OnSelectedIndexChanged", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(comboBoxSearchType, new object[] { EventArgs.Empty });
            searchParametr = null;

            return menuItem;
        }

        /// <summary>
        /// Обновление текущего действия параметра
        /// </summary>
        /// <param name="baseParametr">Текущий параметр</param>
        private void UpdateInfoBaseParametr(ConditionsParametr baseParametr)
        { 
            this.baseParametr = baseParametr;
        }

        /// <summary>
        /// Получение(сборка) поискового параметра по актуальному фильтру
        /// </summary>
        /// <param name="menuItem">Элемент меню к которому относится поисковой фильтр</param>
        /// <param name="columnName">Наименование параметра поиска</param>
        /// <returns></returns>
        private ConditionsParametr CreateConditionSearhParametr(ToolStripMenuItem menuItem, string columnName)
        {
            string valOper = ((ToolStripComboBox)menuItem.DropDownItems[1]).SelectedItem.ToString();
            string valLogic = ((ToolStripComboBox)menuItem.DropDownItems[menuItem.DropDownItems.Count-1]).SelectedItem.ToString();
            string levenshteinLen = null;

            ConditionalOperators operators = Extensions.GetCommitEnumDictionary<ConditionalOperators>().First(i => i.Value.Equals(valOper)).Key;
            LogicOperators logic = Extensions.GetCommitEnumDictionary<LogicOperators>().First(i => i.Value.Equals(valLogic)).Key;

            switch (operators)
            {
                case ConditionalOperators.MoreOrEqual:
                case ConditionalOperators.More:
                    operators = ((ToolStripMenuItem)menuItem.DropDownItems[2]).Checked ? ConditionalOperators.MoreOrEqual : ConditionalOperators.More;
                    break;
                case ConditionalOperators.LessOrEqual:
                case ConditionalOperators.Less:
                    operators = ((ToolStripMenuItem)menuItem.DropDownItems[2]).Checked ? ConditionalOperators.LessOrEqual : ConditionalOperators.Less;
                    break;
                case ConditionalOperators.Like:
                case ConditionalOperators.ILike:
                case ConditionalOperators.ExactLike:
                case ConditionalOperators.ExactILike:
                    bool isCaseSensitive = ((ToolStripMenuItem)menuItem.DropDownItems[2]).Checked;
                    bool isExac = ((ToolStripMenuItem)menuItem.DropDownItems[3]).Checked;

                    if (isCaseSensitive && isExac) operators = ConditionalOperators.ExactLike;
                    else if (isCaseSensitive) operators = ConditionalOperators.Like;
                    else if (isExac) operators = ConditionalOperators.ExactILike;
                    else operators = ConditionalOperators.ILike;
                    break;

                case ConditionalOperators.Levenshtein:
                    levenshteinLen = ((ToolStripTextBox)menuItem.DropDownItems[3]).Text;
                    break;
            }

            return levenshteinLen is null
                ? new ConditionsParametr(columnName, operators, logic, null) 
                { 
                    IsSerhing = true 
                }
                : new LevenshteinSupplement()
                {
                    ColumnName = columnName,
                    LogicOperator = logic,
                    MaxDistance = String.IsNullOrEmpty(levenshteinLen) ? null : Convert.ToInt32(levenshteinLen)
                };
        }

        /// <summary>
        /// Формирование чекита о включении фильтра
        /// </summary>
        /// <param name="isChecked">Указатель о проверке имеющихся текущих сведений о фильтре</param>
        private ToolStripMenuItem CreateCheckItem(bool isChecked)
        {
            ToolStripMenuItem checkItem = new ()
            {
                CheckOnClick = true,
                Checked = isChecked,
                CheckState = isChecked ? CheckState.Checked : CheckState.Unchecked
            };

            UpdateCheckItemAppearance(checkItem);
            return checkItem;
        }

        /// <summary>
        /// Обновление чекита о включения фильтра
        /// </summary>
        /// <param name="checkItem">Чекит включения фильтра</param>
        private void UpdateCheckItemAppearance(ToolStripMenuItem checkItem)
        {
            if (checkItem.Checked)
            {
                checkItem.Text = "Выключить";
                checkItem.ToolTipText = "Исключить из параметров поиска";
                checkItem.BackColor = filterOnColor;
            }
            else
            {
                checkItem.Text = "Включить";
                checkItem.ToolTipText = "Включить в параметры поиска";
                checkItem.BackColor = filterOffColor;
            }
        }

        /// <summary>
        /// Формирование выпадающего списка фильтрации
        /// </summary>
        /// <typeparam name="TOperation">Тип <see cref="Enum"/> пеерчисления</typeparam>
        /// <param name="isEnable">Информация о видимости</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <param name="toolText">Подсказка</param>
        private ToolStripComboBox ComboBoxFilterLoad<TOperation>(bool isEnable, ConditionsParametr searchParametr, string toolText = "") where TOperation : Enum
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

        /// <summary>
        /// Получение выбранного оператора
        /// </summary>
        /// <param name="comboBox">Выпаадющий список</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <returns>Выбранный оператор</returns>
        private ConditionalOperators GetSelectedOperator(ToolStripComboBox comboBox, ConditionsParametr searchParametr)
        {
            Dictionary<ConditionalOperators, string> operators = Extensions.GetCommitEnumDictionary<ConditionalOperators>();

            return searchParametr?.Operator ?? 
                operators.First(kvp => kvp.Value.Equals(comboBox.SelectedItem)).Key;
        }

        /// <summary>
        /// Формирование расширений фильтра
        /// </summary>
        /// <param name="isChecked">Указатель о проверке имеющихся текущих сведений о фильтре</param>
        /// <param name="operators">Условная операция фильтра</param>
        /// <param name="searchParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <returns>Список расширений фильтра</returns>
        private List<ToolStripItem> CreateAdvancedFilterItems(bool isChecked, ConditionsParametr searchParametr, ConditionalOperators operators)
        {
            List<ToolStripItem> items = new ();

            switch (operators)
            {
                case ConditionalOperators.MoreOrEqual:
                case ConditionalOperators.LessOrEqual:
                case ConditionalOperators.More:
                case ConditionalOperators.Less:
                    items.Add(CreateEqualityCheckItem(isChecked, operators));
                    break;

                case ConditionalOperators.Like:
                case ConditionalOperators.ILike:
                case ConditionalOperators.ExactLike:
                case ConditionalOperators.ExactILike:
                    items.AddRange(CreateLikeFilterItems(isChecked, operators));
                    break;

                case ConditionalOperators.Levenshtein:
                    items.AddRange(CreateLevenshteinFilterItems(isChecked, searchParametr));
                    break;
            }

            return items;
        }

        /// <summary>
        /// Формирование расширенного фильтра равенства для условий "Больше", "Меньше"
        /// </summary>
        /// <param name="isChecked">Указатель о проверке имеющихся текущих сведений о фильтре</param>
        /// <param name="operators">Условная операция фильтра</param>
        /// <returns>Дополнительный фильтр равенства</returns>
        private ToolStripMenuItem CreateEqualityCheckItem(bool isChecked, ConditionalOperators operators)
        {
            bool shouldCheck = isChecked &&
                (operators == ConditionalOperators.MoreOrEqual || operators == ConditionalOperators.LessOrEqual);

            ToolStripMenuItem checkItem = CreateOptionItem(
                "Учитывать равенство",
                "Если эта галочка отмечена, будет дополнительно применено условие равенства",
                shouldCheck);

            checkItem.CheckedChanged += (sender, e) =>
                checkItem.BackColor = checkItem.Checked ? filterOnColor : filterOffColor;

            return checkItem;
        }

        /// <summary>
        /// Формирование расширенных фильтров для условия содержания 
        /// </summary>
        /// <param name="isChecked">Указатель о проверке имеющихся текущих сведений о фильтре</param>
        /// <param name="operators">Условная операция фильтра</param>
        /// <returns>Дополнительный фильтр для содержания</returns>
        private ToolStripMenuItem[] CreateLikeFilterItems(bool isChecked, ConditionalOperators operators)
        {
            bool isCaseSensitive = isChecked &&
                (operators == ConditionalOperators.Like || operators == ConditionalOperators.ExactLike);
            bool isExact = isChecked &&
                (operators == ConditionalOperators.ExactILike || operators == ConditionalOperators.ExactLike);

            ToolStripMenuItem caseSensitiveItem = CreateOptionItem(
                "Регистрозависимый",
                "Если эта галочка отмечена, будет дополнительно применено условие регистрозависимости",
                isCaseSensitive);

            ToolStripMenuItem exactItem = CreateOptionItem(
                "Точное совпадение",
                "Если эта галочка отмечена, будет дополнительно применено условие точного поиска(без исключения пробелов)",
                isExact);

            return new[] { caseSensitiveItem, exactItem };
        }

        /// <summary>
        /// Создание чекита для меню
        /// </summary>
        /// <param name="text">Подпись</param>
        /// <param name="tooltip">Подсказка</param>
        /// <param name="isChecked">Состояние</param>
        /// <returns>Чекит для меню</returns>
        private ToolStripMenuItem CreateOptionItem(string text, string tooltip, bool isChecked)
        {
            ToolStripMenuItem item = new (text)
            {
                CheckOnClick = true,
                ToolTipText = tooltip,
                Checked = isChecked,
                CheckState = CheckState.Unchecked,
                BackColor = filterOffColor
            };

            if (isChecked)
            {
                item.CheckState = CheckState.Checked;
                item.BackColor = filterOnColor;
            }

            item.CheckedChanged += (sender, e) =>
                item.BackColor = item.Checked ? filterOnColor : filterOffColor;

            return item;
        }

        /// <summary>
        /// Формирование расширенных фильтра длины для поиска левенштейна
        /// </summary>
        /// <param name="isCheckid">Указатель о проверке имеющихся текущих сведений о фильтре</param>
        /// <param name="searhParametr">Имеющиеся сведенья о фильтрации по выбраному параметру</param>
        /// <returns>Дополнительный фильтр длины для поиска левенштейна</returns>
        private ToolStripItem[] CreateLevenshteinFilterItems(bool isChecked, ConditionsParametr searchParametr)
        {
            ToolStripLabel label = new ("Длинна");
            ToolStripTextBox textBox = new ()
            {
                Text = isChecked && searchParametr is LevenshteinSupplement ls
                    ? ls.MaxDistance.ToString()
                    : string.Empty,
                ToolTipText = "Введите ограничительную длинну для поиска левенштейна",
                BorderStyle = BorderStyle.FixedSingle
            };

            textBox.KeyPress += (sender, e) => e.NumRestrictionTextBox();

            return new ToolStripItem[] { label, textBox };
        }
    }
}
