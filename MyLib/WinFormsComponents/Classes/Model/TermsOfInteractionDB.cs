using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using System.ComponentModel;
using System.Reflection;
using static System.Windows.Forms.ListView;

namespace WinFormsComponents.Classes.Model
{
    public class TermsOfInteractionDB
    {
        /// <summary>
        /// Колонки для условия сравнения в удалении
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public (string, ConditionalOperators, LogicOperators)[] DeleteConditions { get; set; } = [ ("Id", ConditionalOperators.Equal, LogicOperators.None) ];

        /// <summary>
        /// Колонки для условия сравнения в обновлении
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public (string, ConditionalOperators, LogicOperators)[] UpdateConditions { get; set; } = [ ("Id", ConditionalOperators.Equal, LogicOperators.None) ];

        /// <summary>
        /// Получение списка параметров фильтрации для операции удаления
        /// </summary>
        /// <param name="items">Список строк на удаление</param>
        /// <param name="modelType">Тип модели</param>
        /// <returns>Список параметров</returns>
        public IEnumerable<ConditionsParametr> GetDeleteParamers(SelectedListViewItemCollection items, Type modelType) => CreateParametrs(DeleteConditions, items, modelType);
        /// <summary>
        /// Получение списка параметров фильтрации для операции обновления
        /// </summary>
        /// <param name="modelType">Тип модели</param>
        /// <returns>Список параметров</returns>
        public IEnumerable<ConditionsParametr> GetUpdateParamers(Type modelType) => CreateParametrs(UpdateConditions, null, modelType);

        /// <summary>
        /// Получение списка параметров фильтрации по входному набору условий
        /// </summary>
        /// <param name="conditions">Входной набор условий(Наименование колонки, Условие сравнения, Логический оператор соединения условий)</param>
        /// <param name="items">Список строк на изменение</param>
        /// <param name="modelType">Тип модели</param>
        /// <returns>Список параметров</returns>
        private IEnumerable<ConditionsParametr> CreateParametrs((string, ConditionalOperators, LogicOperators)[] conditions, SelectedListViewItemCollection items, Type modelType)
        {
            PropertyInfo[] properties = modelType.GetProperties();

            for (int i = 0; i < (items?.Count ?? 1); i++)
            {
                for (int j = 0; j < conditions.Length; j++)
                {
                    (string colName, ConditionalOperators operators, LogicOperators logic) condition = conditions[j];
                    object value = items is null ? null : properties.First(i => i.Name.Equals(condition.colName)).GetValue(items[i].Tag);

                    condition.logic =
                        i + 1 == (items?.Count ?? 1) && j + 1 == conditions.Length
                            ? LogicOperators.None
                            : j + 1 == conditions.Length 
                                ? LogicOperators.Or
                                : condition.logic == LogicOperators.None 
                                    ? LogicOperators.And 
                                    : condition.logic;

                    yield return new ConditionsParametr(condition.colName, condition.operators, condition.logic, value);
                }
            }
        }
    }
}
