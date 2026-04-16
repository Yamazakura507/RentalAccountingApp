using DataBaseProvaider.Objects;
using Npgsql;
using PostgresSQL;
using System.Collections.ObjectModel;
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
            if (NpgsqlProvider is null)
            {
                throw new Exception("Отсутствует объект подключения");
            }

            DataRow returningValue = null;
            string tableName = typeof(TModel).Name;
            string returningString = returningColumns != null && returningColumns.Length == 0
                                        ? String.Empty
                                        : String.Format("RETURNING  {0}",
                                        returningColumns is null ? "\"Id\"" : String.Join(", ", returningColumns.Select(x => $"\"{x}\"")));
            string command = String.Format(
                @"INSERT INTO {0} ({1}) VALUES ({2}); {3};",
                tableName,
                String.Join(", ", parametrs.Select(x => $"{x.Key}")),
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
            if (NpgsqlProvider is null)
            {
                throw new Exception("Отсутствует объект подключения");
            }

            DataRow returningValue = null;
            string tableName = typeof(TModel).Name;
            (string conditionsStr, NpgsqlParameter[] npgSqlParameters) = new CollectionParametrs() { Conditions = conditions }.ToStringConditions();
            string returningString = returningColumns != null && returningColumns.Length == 0
                                        ? String.Empty
                                        : String.Format("SELECT {0} FROM \"{1}\" t{2}",
                                        returningColumns is null ? "*" : String.Join(", ", returningColumns.Select(x => $"t.\"{x}\"")), 
                                        tableName,
                                        conditionsStr);
            string command = String.Format(
                "UPDATE \"{0}\" SET {1}{2} {3};",
                tableName,
                String.Join(", ", parametrs.Select(x => $"\"{x.Key}\" = @{x.Key}")),
                conditionsStr,
                returningString);

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                returningValue = await msProvider.GetRowAsync(command, npgSqlParameters);
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
            if (NpgsqlProvider is null)
            {
                throw new Exception("Отсутствует объект подключения");
            }

            CollectionParametrs parametrs = new() { Conditions = conditions };
            (string quary, NpgsqlParameter[] parametrs) conditionsCommand = parametrs.ToStringConditions();

            string tableName = typeof(TModel).Name;
            string command = String.Format("DELETE FROM \"{0}\"{1};",tableName,conditionsCommand.quary);;

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
        async public static Task<bool>Comit() => await npgSqlProviderClone.TransactionCommitAsync();

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
        async public static Task<TModel> GetModel<TModel>(int id) where TModel : new ()
        {
            if (NpgsqlProvider is null)
            {
                throw new Exception("Отсутствует объект подключения");
            }

            string command = String.Format("SELECT * FROM \"{0}\" t WHERE t.\"Id\" = @Id", typeof(TModel).Name);
            DataRow row = null;

            using (NpgsqlProvider msProvider = NpgsqlProvider.Clone())
            {
                npgSqlProviderClone = msProvider;
                row = await msProvider.GetRowAsync(command, new[] { new NpgsqlParameter("@Id", id) } );
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
        /// Получение списка объектов модели
        /// </summary>
        /// <typeparam name="TModel">Тип модели</typeparam>
        /// <param name="conditions">Набор условных параметров списка объектов модели (Key: [Имя колонки], Value: ([Условна операция], [Логическая операция], [Значение]))</param>
        /// <param name="orders">Набор сортировочных параметров списка объектов модели (Key: [Имя колонки], Value: [Тип сортировки])</param>
        /// <param name="limit">Ограничение вывода с конца/лимит</param>
        /// <param name="offset">Ограничение вывода с начала/пропуск</param>
        /// <returns>Динамическую коллекцию типа модели</returns>
        async public static Task<ObservableCollection<TModel>> GetCollectionModel<TModel>(CollectionParametrs parametrs = null) where TModel : new()
        {
            parametrs = parametrs ?? new CollectionParametrs();

            (string quary, NpgsqlParameter[] parametrs) conditions = parametrs.ToStringConditions();
            ObservableCollection<TModel> collection = new ();

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
    }
}
