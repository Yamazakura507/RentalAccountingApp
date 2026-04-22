using DataBaseProvaider.Classes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using Npgsql;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace DataBaseProvaider
{
    /// <summary>
    /// Клас преобразователь    
    /// </summary>
    public static class Converter
    {
        private static readonly ConcurrentDictionary<Type, PropertyMap[]> propertyMapsCache = new();
        private static readonly ConditionalOperators[] likeOperation = [ConditionalOperators.Like, ConditionalOperators.ILike];
        private static readonly ConditionalOperators[] exactLikeOperation = [ConditionalOperators.ExactLike, ConditionalOperators.ExactILike];

        /// <summary>
        /// Преобразует строку данных в объект модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="row">Строка данных</param>
        /// <returns>Объект модели</returns>
        /// <exception cref="ArgumentNullException">Попытка преобразования пустой строки</exception>
        public static TModel RowToObject<TModel>(this DataRow row) where TModel : new()
        {
            if (row is null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            TModel model = new();
            PropertyMap[] propertyMaps = GetPropertyMaps<TModel>();

            foreach (PropertyMap map in propertyMaps)
            {
                if (!row.Table.Columns.Contains(map.ColumnName)) continue;

                object value = row[map.ColumnName];

                if (value == DBNull.Value) value = null;

                try
                {
                    object convertedValue = ConvertValue(value, map.PropertyType);
                    map.Property.SetValue(model, convertedValue, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return model;
        }

        /// <summary>
        /// Получает или создает карту свойств для указанного типа модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <returns>Массив объектов <see cref="PropertyMap"/>, содержащий информацию о свойствах модели и соответствующих столбцах</returns>
        /// <remarks>
        /// Метод использует кэширование для повышения производительности при повторных вызовах.
        /// Для каждого типа модели информация о свойствах вычисляется только один раз.
        /// </remarks>
        private static PropertyMap[] GetPropertyMaps<TModel>()
        {
            Type modelType = typeof(TModel);

            return propertyMapsCache.GetOrAdd(modelType, type =>
            {
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyMap[] maps = new PropertyMap[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    ColumnAttribute columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                    string columnName = columnAttr?.Name ?? property.Name;

                    maps[i] = new PropertyMap(property, columnName);
                }

                return maps;
            });
        }

        /// <summary>Преобразует значение из <see cref="DataRow"/> в целевой тип свойства</summary>
        /// <param name="value">Исходное значение из столбца DataRow</param>
        /// <param name="targetType">Целевой тип, в который необходимо преобразовать значение</param>
        /// <returns>Преобразованное значение или значение по умолчанию для целевого типа</returns>
        /// <exception cref="InvalidCastException">Может быть выброшено, если преобразование типа невозможно</exception>
        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null)
            {
                return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) is null
                    ? Activator.CreateInstance(targetType)
                    : null;
            }

            Type actualTargetType = targetType;

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                actualTargetType = Nullable.GetUnderlyingType(targetType);
            }

            return Convert.ChangeType(value, actualTargetType);
        }

        /// <summary>
        /// Преобразование в строку элемента <see cref="Enum"/>
        /// </summary>
        /// <param name="enumElement">Элемент <see cref="Enum"/></param>
        /// <returns>Элемент <see cref="Enum"/> в формате строки</returns>
        public static string EnumToString(this Enum enumElement)
        {
            return Enum.GetName(enumElement.GetType(), enumElement);
        }

        /// <summary>
        /// Перевод строки в элемент <see cref="Enum"/>
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/> тип</typeparam>
        /// <param name="enumElementName">строка для перевода в <see cref="Enum"/></param>
        /// <returns><see cref="Enum"/> элемент</returns>
        public static T StringToEnum<T>(this string enumElementName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), enumElementName);
        }

        /// <summary>
        /// Получение описания элемнта <see cref="Enum"/>
        /// </summary>
        /// <param name="enumElement">Элемент <see cref="Enum"/></param>
        /// <returns>Описание <see cref="Enum"/> элемента</returns>
        public static string GetDescription(this Enum enumElement)
        {
            FieldInfo field = enumElement.GetType().GetField(enumElement.ToString());
            DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? enumElement.ToString();
        }

        /// <summary>
        /// Проверка строки на like операцию
        /// </summary>
        /// <param name="operators">Условный оператор</param>
        /// <param name="value">Значение</param>
        /// <returns>Поступившее или отформатированое под like операцию значение</returns>
        /// <remarks>
        /// <para>Если операция имеет like формат,тогда будет применено форматирование строки:</para>
        /// <para>-Если имеется указание, точного совпадения вернется строка вида "%[Значение]%"</para>
        /// <para>-Если не имеется указания, точного совпадения вернется строка вида "%[Значение в котором пробелы заменены на символ %]%"</para>
        /// <para>Если операция не имеет like формат или значение не имеет тип String,тогда будет возвращено исходное значение</para>
        /// </remarks>
        internal static object CheckLikeOperation(this ConditionalOperators operators, object value)
        {
            if (!(value is string)) return value;

            string valueStr = value.ToString();
            bool isExacLike = exactLikeOperation.Contains(operators);
            bool isLike = likeOperation.Contains(operators);

            if (isExacLike || isLike)
            {
                valueStr = String.Format("%{0}%", valueStr);

                if (isLike) valueStr = valueStr.Replace(' ', '%');
            }

            return valueStr;
        }

        /// <summary>
        /// Вызов метода возращающего коллекцию с неявным типом через рефлексию
        /// </summary>
        /// <typeparam name="T">Тип возврощаемой коллекции</typeparam>
        /// <param name="type">Тип коллекции которую возвращет вызываемый метод</param>
        /// <param name="parametrs">Параметры вызываемого метода</param>
        /// <param name="nameMethod">Наименование метода</param>
        /// <returns>
        /// <para>Коллекцию типа</para>
        /// <para>-Рекомендуется в качестве T указывать <see cref="object"/> или dynamic.</para>
        /// </returns>
        async public static Task<BindingList<T>> GetCollectionByType<T>(this Type type, object[] parametrs, string nameMethod)
        {
            Task task = await type.InvokeMethodByType(parametrs, nameMethod);

            IEnumerable<T> collection = ((dynamic)task).Result;

            return new BindingList<T>(collection.ToList());
        }

        /// <summary>
        /// Вызов метода возращающего процес с неявным типом через рефлексию
        /// </summary>
        /// <param name="type">Тип обрабатываемой модели</param>
        /// <param name="parametrs">Параметры вызываемого метода</param>
        /// <param name="nameMethod">Наименование метода</param>
        /// <returns>Процес</returns>
        async public static Task<Task> InvokeMethodByType(this Type type, object[] parametrs, string nameMethod)
        {
            MethodInfo method = typeof(DBProvider).GetMethod(nameMethod);
            MethodInfo genericMethod = method.MakeGenericMethod(type);

            Task task = genericMethod.Invoke(null, parametrs) as Task;
            await task.ConfigureAwait(false);

            return task;
        }

        /// <summary>
        /// Вызов метода возращающего значение запрашиваемого типа
        /// </summary>
        /// <typeparam name="T">Запрашиваемый тип</typeparam>
        /// <param name="type">Тип модели</param>
        /// <param name="parametrs">Параметры вызываемого метода</param>
        /// <param name="nameMethod">Наименование метода</param>
        /// <returns>Значение запрашиваемого типа</returns>
        async public static Task<T> GetResultByType<T>(this Type type, object[] parametrs, string nameMethod)
        {
            Task task = await type.InvokeMethodByType(parametrs, nameMethod);

            T result = (T)((dynamic)task).Result;

            return result;
        }

        /// <summary>
        /// Приводит набор параметров для фильрации к строчному виду запроса
        /// </summary>
        /// <param name="collection">Набор параметров</param>
        /// <returns>Кортеж из строки части запроса для фильтрации и набор параметров подставляемых в запрос</returns>
        internal static (string, NpgsqlParameter[]) ToStringConditions(this CollectionParametrs collection)
        {
            IEnumerable<ConditionsParametr> conditions = collection.Conditions.Where(c => (c.Value != null && !String.IsNullOrEmpty(c.Value.ToString())) || !c.IsSerhing);
            string conditionsStr = String.Empty;
            List<NpgsqlParameter> conditionsParametr = null;
            int cntCondition = conditions.Count();

            if (cntCondition != 0)
            {
                conditionsStr += " WHERE ";
                conditionsParametr = new();

                for (int i = 0; i < cntCondition; i++)
                {
                    ConditionsParametr condition = conditions.ElementAt(i);

                    condition.LogicOperator = i + 1 == cntCondition 
                                                ? LogicOperators.None 
                                                : condition.LogicOperator == LogicOperators.None 
                                                    ? LogicOperators.And 
                                                    : condition.LogicOperator;

                    if (condition.Operator.Equals(ConditionalOperators.Levenshtein))
                    {
                        LevenshteinSupplement levenshteinCondition = (LevenshteinSupplement)condition;

                        conditionsStr += String.Format("{1}(t.\"{0}\",@{0}{3},@maxDistance{0}{3}) <= @maxDistance{0}{3} {2} ",
                                         condition.ColumnName,
                                         condition.Operator.GetDescription(),
                                         condition.LogicOperator.GetDescription(), 
                                         condition.Id);

                        conditionsParametr.Add(new(String.Format("@{0}{1}", condition.ColumnName, condition.Id), condition.Value));
                        conditionsParametr.Add(new(String.Format("@maxDistance{0}{1}", condition.ColumnName, condition.Id),
                                                                       levenshteinCondition.MaxDistance ?? 3));

                        levenshteinCondition.LevenshteinSetOrders(collection);
                    }
                    else
                    {
                        conditionsStr += String.Format("t.\"{0}\" {1} @{0}{3} {2} ",
                                         condition.ColumnName,
                                         condition.Operator.GetDescription(),
                                         condition.LogicOperator.GetDescription(), 
                                         condition.Id);

                        conditionsParametr.Add(new(String.Format("@{0}{1}", condition.ColumnName, condition.Id),
                                                                       condition.Operator.CheckLikeOperation(condition.Value ?? DBNull.Value)));
                    }
                }

                conditionsStr = conditionsStr.TrimEnd();
            }

            return (conditionsStr, conditionsParametr?.ToArray());
        }

        /// <summary>
        /// Приводит набор параметров для сортировки к строчному виду запроса
        /// </summary>
        /// <param name="collection">Набор параметров</param>
        /// <returns>Строка части запроса для сортировки</returns>
        internal static string ToStringOrders(this CollectionParametrs collection)
        {
            string ordersStr = String.Empty;

            if (collection.Orders.Count() != 0)
            {
                ordersStr = String.Format(
                                        " ORDER BY {0}",
                                        String.Join(", ", collection.Orders.Select(o => String.Format("t.\"{0}\" {1}",
                                                                                        o.ColumnName,
                                                                                        o.OrderType.EnumToString().ToUpper()))));

                collection.Orders = collection.Orders.Where(o => !o.IsTemp);
            }

            return ordersStr;
        }

        /// <summary>
        /// Приводит ограничение вывода с конца к строчному виду запроса
        /// </summary>
        /// <param name="collection">Набор параметров</param>
        /// <returns>Строка части запроса для ограничения вывода</returns>
        internal static string ToStringLimit(this CollectionParametrs collection)
        {
            string limitStr = String.Empty;

            if (collection.Limit > 0)
            {
                limitStr = String.Format(" LIMIT {0}", collection.Limit);
            }

            return limitStr;
        }

        /// <summary>
        /// Приводит ограничение вывода с начала к строчному виду запроса
        /// </summary>
        /// <param name="collection">Набор параметров</param>
        /// <returns>Строка части запроса для ограничения вывода</returns>
        internal static string ToStringOffset(this CollectionParametrs collection)
        {
            string offsetStr = String.Empty;

            if (collection.Offset > 0)
            {
                offsetStr = String.Format(" OFFSET {0}", collection.Offset);
            }

            return offsetStr;
        }

        /// <summary>
        /// Добавление сортировки по ливенштейну
        /// </summary>
        /// <param name="levenshteinCondition">Условие ливенштейна</param>
        /// <param name="parametrs">Набор параметров</param>
        internal static void LevenshteinSetOrders(this LevenshteinSupplement levenshteinCondition, CollectionParametrs parametrs)
        {
            if (!levenshteinCondition.OrderPriority.Equals(OrderLevenshteineSupplement.None))
            {
                OrderParametr newOrder = new(String.Format("{1}(t.\"{0}\",@{0},@maxDistance{0})",
                                                levenshteinCondition.ColumnName,
                                                levenshteinCondition.Operator.GetDescription()),
                                            levenshteinCondition.OrderType, true);

                parametrs.Orders = levenshteinCondition.OrderPriority switch
                {
                    OrderLevenshteineSupplement.Priority => parametrs.Orders.Prepend(newOrder),
                    OrderLevenshteineSupplement.EndList => parametrs.Orders.Append(newOrder),
                    OrderLevenshteineSupplement.Position => parametrs.Orders.InsertAt(newOrder, levenshteinCondition.OrderPriorityPosition),
                };
            }
        }

        /// <summary>
        /// Находит словарь [Key:[Свойство модели], Value:[Описание свойства]]
        /// </summary>
        /// <param name="typeModel">Тип модели</param>
        /// <returns>Словарь [Key:[Свойство модели], Value:[Описание свойства]</returns>
        public static Dictionary<string, string> GetDescriptionModel(this Type typeModel)
        {
            return typeModel.GetProperties().Select
                (
                    p => new
                    {
                        Property = p,
                        Attribute = p.GetCustomAttribute<DescriptionAttribute>()
                    }
                ).Where(p => p.Attribute != null)
                .ToDictionary
                (
                    p => p.Property.Name, 
                    p => p.Attribute.Description
                );
        }
    }
}
