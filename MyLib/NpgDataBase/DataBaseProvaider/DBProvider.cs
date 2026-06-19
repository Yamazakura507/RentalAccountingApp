    using DataBaseProvaider.Objects;
using Npgsql;
using PostgresSQL;
using System.ComponentModel;
using System.Data;

namespace DataBaseProvaider
{
    public static class DBProvider
    {
        private static NpgsqlProvider npgSqlProviderClone;
        public static NpgsqlProvider NpgsqlProvider { get; set; }

        /// <summary>
        /// Добавление строки
        /// </summary>
        /// <typeparam name="TModel">Тип добовляемой модели</typeparam>
        /// <param name="parametrs">Набор добавляемых праметров модели (Key: [Имя колонки], Value: [Значение])</param>
        /// <param name="returningColumns">Возвращаемые колонки(по умолчанию null - вся строка, для возврата null укажите пустой массив)</param>
        /// <returns>Запрашевыемый результат по новой строке</returns>
        /// <exception cref="Exception">Исключение при отсутствии подключения</exception>
        async public static Task<DataRow> Insert<TModel>(Dictionary<string, object> parametrs, string[] returningColumns = null)
        {
            ConectionCheck();

            DataRow returningValue = null;
            string tableName = typeof(TModel).Name;
            string returningString = returningColumns != null && returningColumns.Length == 0
                                        ? String.Empty
                                        : String.Format("RETURNING  {0}",
                                        returningColumns is null ? "\"Id\"" : String.Join(", ", returningColumns.Select(x => $"\"{x}\"")));
            string command = String.Format(
                "INSERT INTO \"{0}\" ({1}) VALUES ({2}) {3};",
                tableName,
                String.Join(", ", parametrs.Select(x => $"\"{x.Key}\"")),
                String.Join(", ", parametrs.Select(x => $"@{x.Key}")),
                returningString);

            NpgsqlParameter[] npgSqlParameters = parametrs.Select(x => new NpgsqlParameter($"@{x.Key}", x.Value ?? DBNull.Value)).ToArray();

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                returningValue = await msProvider.GetRowAsync(command, npgSqlParameters);
            }

            npgSqlProviderClone = null;

            return returningValue;
        }

        /// <summary>
        /// Обновление строки
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="parametrs">Набор изменяемых праметров модели (Key: [Имя колонки], Value: [Значение])</param>
        /// <param name="conditions">Набор условных параметров модели (Key: [Имя колонки], Value: [Значение])</param>
        /// <param name="returningColumns">Возвращаемые колонки(по умолчанию null - вся строка, для возврата null укажите пустой массив)</param>
        /// <returns>Запрашевыемый результат по новой строке</returns>
        /// <exception cref="Exception">Исключение при отсутствии подключения</exception>
        async public static Task<DataRow> Update<TModel>(Dictionary<string, object> parametrs, IEnumerable<ConditionsParametr> conditions, string[] returningColumns = null)
        {
            ConectionCheck();

            DataRow returningValue = null;
            string tableName = typeof(TModel).Name;
            (string conditionsStr, NpgsqlParameter[] npgSqlParametersReturning) = new CollectionParametrs() { Conditions = conditions }.ToStringConditions();
            string returningString = returningColumns != null && returningColumns.Length == 0
                                        ? String.Empty
                                        : String.Format("RETURNING {0}",
                                        returningColumns is null ? "\"Id\"" : String.Join(", ", returningColumns.Select(x => $"\"{x}\"")));
            string command = String.Format(
                "UPDATE \"{0}\" t SET {1}{2} {3};",
                tableName,
                String.Join(", ", parametrs.Select(x => $"\"{x.Key}\" = @{x.Key}")),
                conditionsStr,
                returningString);

            List<NpgsqlParameter> npgSqlParameters = parametrs.Select(x => new NpgsqlParameter($"@{x.Key}", x.Value ?? DBNull.Value)).ToList();
            npgSqlParameters.AddRange(npgSqlParametersReturning);

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                returningValue = await msProvider.GetRowAsync(command, npgSqlParameters.ToArray());
            }


            npgSqlProviderClone = null;

            return returningValue;
        }

        /// <summary>
        /// Удаление строки
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="conditions">Набор условных параметров модели (Key: [Имя колонки], Value: [Значение])</param>
        /// <returns>Процесс...</returns>
        /// <exception cref="Exception">Исключение при отсутствии подключения</exception>
        async public static Task Delete<TModel>(IEnumerable<ConditionsParametr> conditions)
        {
            ConectionCheck();

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format("DELETE FROM \"{0}\" t{1};", tableName, conditionsCommand.quary);

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                await msProvider.ExecuteQueryAsync(command, conditionsCommand.parametrs);
            }

            npgSqlProviderClone = null;
        }

        /// <summary>
        /// Coment транзакции - прервать транзакцию с сохранением выполненой работы
        /// </summary>
        /// <returns>Результат прерывания</returns>
        async public static Task<bool> Comit() => await npgSqlProviderClone.TransactionCommitAsync();

        /// <summary>
        /// Rollback транзакции - прервать транзакцию и откатить изменения
        /// </summary>
        /// <returns>Результат прерывания</returns>
        async public static Task<bool> Rollback() => await npgSqlProviderClone.TransactionRollbackAsync();

        /// <summary>
        /// Получение объекта модели по Id 
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="id">Идентификатотр объекта модели/строки</param>
        /// <returns>Объект модели по запрашевоемому идентификатору</returns>
        /// <exception cref="Exception">Возможны исключения преобразования строки в объект</exception>
        async public static Task<TModel> GetModel<TModel>(int id) where TModel : new()
        {
            ConectionCheck();

            string command = String.Format("SELECT * FROM \"{0}\" t WHERE t.\"Id\" = @Id", typeof(TModel).Name);
            DataRow row = null;

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                row = await msProvider.GetRowAsync(command, new[] { new NpgsqlParameter("@Id", id) });
            }

            npgSqlProviderClone = null;

            try
            {
                return row.RowToObject<TModel>();
            }
            catch (Exception ex)
            {
                NpgsqlProvider.HandlerErrror.ErrorReport(ex);

                return default(TModel);
            }
        }

        /// <summary>
        /// Получение списка объектов модели по вызову функции
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="functionParametrs">Набор значений параметров функции</param>
        /// <param name="functionName">Наименование функции, по умолчанию будет сформировано из типа</param>
        /// <param name="parametrs">Набор различных паарметров фильтрации сортировки запрпоса</param>
        /// <returns>Объект модели</returns>
        async public static Task<TModel> GetCallFunctionModel<TModel>(object[] functionParametrs = null, string functionName = null, CollectionParametrs parametrs = null) where TModel : new() =>
            (await GetCallFunctionCollectionModel<TModel>(functionParametrs, functionName, parametrs))[0];

        /// <summary>
        /// Получение списка объектов модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="parametrs">Набор различных паарметров фильтрации сортировки запрпоса</param>
        /// <returns>Динамическую коллекцию типа модели</returns>
        async public static Task<BindingList<TModel>> GetCollectionModel<TModel>(CollectionParametrs parametrs = null) where TModel : new()
        {
            ConectionCheck();

            parametrs = parametrs ?? new CollectionParametrs();

            (string quary, NpgsqlParameter[] parametrs) conditions = parametrs.ToStringConditions();
            BindingList<TModel> collection = new();

            string command = String.Format(
                                "SELECT * FROM \"{0}\" t{1}{2}{3}{4}",
                                typeof(TModel).Name,
                                conditions.quary,
                                parametrs.ToStringOrders(),
                                parametrs.ToStringLimit(),
                                parametrs.ToStringOffset());

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;

                DataTable dataTable = await msProvider.GetTableAsync(command, conditions.parametrs, true);

                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        collection.Add(dataTable.Rows[i].RowToObject<TModel>());
                    }
                }
            }

            npgSqlProviderClone = null;

            return collection;
        }

        /// <summary>
        /// Получение списка объектов модели по вызову функции
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="functionParametrs">Набор значений параметров функции</param>
        /// <param name="functionName">Наименование функции, по умолчанию будет сформировано из типа</param>
        /// <param name="parametrs">Набор различных паарметров фильтрации сортировки запрпоса</param>
        /// <returns>Динамическую коллекцию типа модели</returns>
        async public static Task<BindingList<TModel>> GetCallFunctionCollectionModel<TModel>(object[] functionParametrs = null, string functionName = null, CollectionParametrs parametrs = null) where TModel : new()
        {
            ConectionCheck();

            parametrs = parametrs ?? new CollectionParametrs();

            (string quary, NpgsqlParameter[] parametrs) conditions = parametrs.ToStringConditions();
            (string quary, NpgsqlParameter[] parametrs) funcParametrs = functionParametrs.ToStringParametrs();
            BindingList<TModel> collection = new();

            string command = String.Format(
                                "SELECT * FROM \"{0}\"({1}) t{2}{3}{4}{5}",
                                functionName ?? typeof(TModel).Name.ToSnakeCase(),
                                funcParametrs.quary,
                                conditions.quary,
                                parametrs.ToStringOrders(),
                                parametrs.ToStringLimit(),
                                parametrs.ToStringOffset());

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;

                NpgsqlParameter[] conditionsAndParametrs = conditions.parametrs ?? funcParametrs.parametrs;

                if (funcParametrs.parametrs is not null && conditions.parametrs is not null)
                {
                    conditionsAndParametrs = conditions.parametrs.Union(funcParametrs.parametrs).ToArray();
                }

                DataTable dataTable = await msProvider.GetTableAsync(command, conditionsAndParametrs, true);

                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        collection.Add(dataTable.Rows[i].RowToObject<TModel>());
                    }
                }
            }

            npgSqlProviderClone = null;

            return collection;
        }

        /// <summary>
        /// Получение колонки из таблицы модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <typeparam name="TValue">Тип возврата</typeparam>
        /// <param name="columnName">Имя колонки таблицы</param>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <returns>Список значений колонки</returns>
        async public static Task<IEnumerable<TValue>> GetColumnModel<TValue,TModel>(string columnName, IEnumerable<ConditionsParametr> conditions = null) where TModel : new()
        {
            ConectionCheck();

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format("SELECT t.\"{2}\" FROM \"{0}\" t{1};", tableName, conditionsCommand.quary, columnName);

            IEnumerable<TValue> columnModel = null;

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                columnModel = await msProvider.GetColumnAsync<TValue>(command, conditionsCommand.parametrs);
            }

            npgSqlProviderClone = null;

            return columnModel;
        }

        /// <summary>
        /// Получение количества строк в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <returns>Количество строк</returns>
        async public static Task<int> Count<TModel>(IEnumerable<ConditionsParametr> conditions = null)
        {
            ConectionCheck();

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format("SELECT COUNT(*) FROM \"{0}\" t{1};", tableName, conditionsCommand.quary);

            int count = 0;

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                count = await msProvider.GetValueAsync<int>(command, conditionsCommand.parametrs);
            }

            npgSqlProviderClone = null;

            return count;
        }

        /// <summary>
        /// Получение максимального значения по колонке в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <param name="columnName">Наименование колонки поиска</param>
        /// <returns>Максимальное значение колонки</returns>
        async public static Task<object> Max<TModel>(string columnName, IEnumerable<ConditionsParametr> conditions = null) => await AgregateFunc<TModel>(columnName, nameof(Max), conditions);

        /// <summary>
        /// Получение минимального значения по колонке в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <param name="columnName">Наименование колонки поиска</param>
        /// <returns>Минимальное значение колонки</returns>
        async public static Task<object> Min<TModel>(string columnName, IEnumerable<ConditionsParametr> conditions = null) => await AgregateFunc<TModel>(columnName, nameof(Min), conditions);

        /// <summary>
        /// Получение суммы значений по колонке в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <param name="columnName">Наименование колонки поиска</param>
        /// <returns>Минимальное значение колонки</returns>
        async public static Task<object> Sum<TModel>(string columnName, IEnumerable<ConditionsParametr> conditions = null) => await AgregateFunc<TModel>(columnName, nameof(Sum), conditions);

        /// <summary>
        /// Получение значения агрегатной функции по колонке в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <param name="columnName">Наименование колонки поиска</param>
        /// <param name="nameFunc">Наименование агрегатной функции</param>
        /// <returns>Результат функции</returns>
        async private static Task<object> AgregateFunc<TModel>(string columnName, string nameFunc, IEnumerable<ConditionsParametr> conditions = null)
        {
            ConectionCheck();

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format("SELECT {3}(t.\"{2}\") FROM \"{0}\" t{1};", tableName, conditionsCommand.quary, columnName, nameFunc.ToUpper());

            object agregateVal = null;

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                agregateVal = await msProvider.GetValueAsync<object>(command, conditionsCommand.parametrs);
            }

            npgSqlProviderClone = null;

            return agregateVal == DBNull.Value ? null : agregateVal;
        }

        /// <summary>
        /// Получение значений набора агрегатных функций по колонке в таблице модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели таблицы</typeparam>
        /// <param name="conditions">Параметры фильтрации(если пусто то выведиться полное количество строк)</param>
        /// <param name="columnName">Наименование колонки поиска</param>
        /// <param name="namesFunc">Наименования агрегатных функций</param>
        /// <returns>Результат функции</returns>
        async public static Task<Dictionary<string, object>> ArgegateFuncStack<TModel>(string columnName, string[] namesFunc, IEnumerable<ConditionsParametr> conditions = null)
        {
            ConectionCheck();

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format(
                "SELECT {2} FROM \"{0}\" t{1};",
                tableName,
                conditionsCommand.quary,
                String.Join(", ", namesFunc.Select(i => String.Format(
                                                            "{1}(t.\"{0}\") \"{2}\"",
                                                            columnName,
                                                            i.ToUpper(), i))));

            Dictionary<string, object> agregateVals = new ();

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                DataRow row = await msProvider.GetRowAsync(command, conditionsCommand.parametrs);

                foreach (DataColumn column in row.Table.Columns)
                {
                    object val = row[column.ColumnName];
                    agregateVals.Add(column.ColumnName, val == DBNull.Value ? null : val);
                }
            }

            npgSqlProviderClone = null;

            return agregateVals;
        }

        /// <summary>
        /// Проверка объекта подключения
        /// </summary>
        /// <exception cref="Exception">Сообщение исключение, если отсутствует объект подключения</exception>
        private static void ConectionCheck()
        {
            if (NpgsqlProvider is null)
            {
                throw new Exception("Отсутствует объект подключения");
            }
        }
    }
}
