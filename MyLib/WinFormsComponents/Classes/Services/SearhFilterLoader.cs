using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.Reflection;
using WinFormsComponents.Classes.Interface;

namespace WinFormsComponents.Classes.Services
{
    /// <summary>
    /// Сервис визуализации фильтров
    /// </summary>
    public class SearhFilterLoader : IFilter
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
        public SearhFilterLoader(Color filterOffColor, Color filterOnColor)
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
        public ToolStripMenuItem CreateFilter(string columnText, string columnName, ConditionsParametr searchParametr, FilterChangedHandler onFilterChanged = null)
        {
            baseParametr = searchParametr;
            ToolStripMenuItem menuItem = new(columnText, Properties.Resources.searh);

            Dictionary<bool, (string, string, Color)> checkItemParametrs = new()
            {
                { false, ("Включить", "Включить в параметры поиска", filterOffColor) },
                { true, ("Выключить", "Исключить из параметров поиска", filterOnColor) }
            };

            ToolStripMenuItem checkItem = checkItemParametrs.CreateOptionItem(searchParametr != null);
            ToolStripComboBox comboBoxSearchType = FilterFunction.ComboBoxFilterLoad<ConditionalOperators>(checkItem.Checked, searchParametr, "Условие поиска");
            ToolStripComboBox comboBoxLogicType = FilterFunction.ComboBoxFilterLoad<LogicOperators>(checkItem.Checked, searchParametr, "Логическое соединение условий поиска");

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
                checkItem.CheckedChangedItemMenu(checkItemParametrs);
                comboBoxSearchType.Enabled = comboBoxLogicType.Enabled = checkItem.Checked;
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

            ToolStripMenuItem checkItem = new Dictionary<bool, (string, string, Color)>()
            {
                { false, ("Учитывать равенство", "Если эта галочка снята, будет отменено дополнительное условие равенства", filterOffColor) },
                { true, ("Учитывать равенство", "Если эта галочка отмечена, будет дополнительно применено условие равенства", filterOnColor) }
            }.CreateOptionItem(shouldCheck);

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

            ToolStripMenuItem caseSensitiveItem = new Dictionary<bool, (string, string, Color)>()
            {
                { false, ("Регистрозависимый", "Если эта галочка отмечена, будет дополнительно применено условие регистрозависимости", filterOffColor) },
                { true, ("Регистрозависимый", "Если эта галочка снята, будет отменено дополнительное условие регистрозависимости", filterOnColor) }
            }.CreateOptionItem(isCaseSensitive);

            ToolStripMenuItem exactItem = new Dictionary<bool, (string, string, Color)>()
            {
                { false, ("Точное совпадение", "Если эта галочка отмечена, будет дополнительно применено условие точного поиска(без исключения пробелов)", filterOffColor) },
                { true, ("Точное совпадение", "Если эта галочка снята, будет отменено дополнительное условие точного поиска(игнорирование пробелов)", filterOnColor) }
            }.CreateOptionItem(isExact);

            return new[] { caseSensitiveItem, exactItem };
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
