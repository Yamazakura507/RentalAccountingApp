using Npgsql;
using System.Data;

namespace PostgresSQL
{
    /// <summary>
    /// Класс для базового взаимодействия с БД
    /// </summary>
    public class NpgsqlProvider : IDisposable
    {
        private NpgsqlConnectionStringBuilder npgSqlConnectionStringBuilder;
        private NpgsqlConnection npgSqlConnection;
        private NpgsqlTransaction npgSqlTransaction;

        /// <summary>
        /// Объект(параметры) соединения/Билд строки подключения
        /// </summary>
        public NpgsqlConnectionStringBuilder NpgsqlConnectionStringBuilder 
        { 
            get => npgSqlConnectionStringBuilder;
            set
            {
                npgSqlConnectionStringBuilder = value;
                npgSqlConnection = new (value.ConnectionString);
            }
        }

        /// <summary>
        /// Состояние подключения
        /// </summary>
        public NpgsqlConnection Conection { get => npgSqlConnection; }

        /// <summary>
        /// Обработчику ошибок, для работы назначте делегат прогресс объекту
        /// </summary>
        public HandlerErrror HandlerErrror { get; set; }

        /// <summary>
        /// конструктор через строку подключения
        /// </summary>
        /// <param name="connectionString"></param>
        public NpgsqlProvider(string connectionString) : this(new NpgsqlConnectionStringBuilder(connectionString)) { }

        /// <summary>
        /// Конструктор через готовый билд строки подключения
        /// </summary>
        /// <param name="connectionStringBuilder">Билд строки подключения</param>
        public NpgsqlProvider(NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            NpgsqlConnectionStringBuilder = connectionStringBuilder;
            HandlerErrror = new ();
        }

        /// <summary>
        /// Конструктор через готовый билд строки подключения
        /// </summary>
        /// <param name="connectionStringBuilder">Билд строки подключения</param>
        /// <param name="handlerErrror">Обработчик ошибок</param>
        public NpgsqlProvider(NpgsqlConnectionStringBuilder connectionStringBuilder, HandlerErrror handlerErrror)
        {
            NpgsqlConnectionStringBuilder = connectionStringBuilder;
            HandlerErrror = handlerErrror;
        }

        /// <summary>
        /// Открытие подключения
        /// </summary>
        private void Connect()
        {
            if (!npgSqlConnection.State.Equals(ConnectionState.Open))
            {
                npgSqlConnection.Open();
            }
        }

        /// <summary>
        /// Закрытие подключения
        /// </summary>
        private void Disconnect()
        {
            if (npgSqlConnection != null && npgSqlConnection.State.Equals(ConnectionState.Open))
            {
                npgSqlConnection.Close();
            }
        }

        /// <summary>
        /// Открытие подключения
        /// </summary>
        async private Task ConnectAsync()
        {
            if (!npgSqlConnection.State.Equals(ConnectionState.Open))
            {
                await npgSqlConnection.OpenAsync();
            }
        }

        /// <summary>
        /// Закрытие подключения
        /// </summary>
        async private Task DisconnectAsync()
        {
            if (npgSqlConnection != null && npgSqlConnection.State.Equals(ConnectionState.Open))
            {
                await npgSqlConnection.CloseAsync();
            }
        }

        /// <summary>
        /// Получение текущей транзакции
        /// </summary>
        private void BeginTransaction()
        {
            Connect();

            npgSqlTransaction = npgSqlConnection.BeginTransaction();

            return;
        }

        /// <summary>
        /// Получение текущей транзакции
        /// </summary>
        async private Task BeginTransactionAsync()
        {
            await ConnectAsync();

            npgSqlTransaction = await npgSqlConnection.BeginTransactionAsync();

            return;
        }

        /// <summary>
        /// Coment транзакции - прервать транзакцию с сохранением выполненой работы
        /// </summary>
        /// <returns>Результат прерывания</returns>
        async public Task<bool> TransactionCommitAsync()
        {
            if (npgSqlTransaction is null)
            {
                await this.BeginTransactionAsync();
            }

            try
            {
                await npgSqlTransaction.CommitAsync();
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);

                return false;
            }

            npgSqlTransaction = null;

            return true;
        }

        /// <summary>
        /// Coment транзакции - прервать транзакцию с сохранением выполненой работы
        /// </summary>
        /// <returns>Результат прерывания</returns>
        public bool TransactionCommit()
        {
            if (npgSqlTransaction is null)
            {
                this.BeginTransaction();
            }

            try
            {
                npgSqlTransaction.Commit();
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);

                return false;
            }

            npgSqlTransaction = null;

            return true;
        }

        /// <summary>
        /// Rollback транзакции - прервать транзакцию и откатить изменения
        /// </summary>
        /// <returns>Результат прерывания</returns>
        async public Task<bool> TransactionRollbackAsync()
        {
            if (npgSqlTransaction is null)
            {
                await this.BeginTransactionAsync();
            }

            try
            {
                await npgSqlTransaction.RollbackAsync();
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);

                return false;
            }

            npgSqlTransaction = null;

            return true;
        }

        /// <summary>
        /// Rollback транзакции - прервать транзакцию и откатить изменения
        /// </summary>
        /// <returns>Результат прерывания</returns>
        public bool TransactionRollback()
        {
            if (npgSqlTransaction is null)
            {
                this.BeginTransaction();
            }

            try
            {
                npgSqlTransaction.Rollback();
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);

                return false;
            }

            npgSqlTransaction = null;

            return true;
        }

        /// <summary>
        /// Выполнение скрипта для возврата одного значения, при ошибке вернет null и сообщение в обработчик
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип</typeparam>
        /// <param name="query">Запрос/Скрипт</param>
        /// <param name="parametrs">Параметры(необязательно)</param>
        /// <returns>Запрашиваемый результат</returns>
        async public Task<T> GetValueAsync<T>(string query, NpgsqlParameter[] parametrs = null)
        {
            await ConnectAsync();

            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.Connection = npgSqlConnection;
            cmd.CommandText = query;

            if (parametrs != null)
            {
                cmd.Parameters.AddRange(parametrs);
            }

            T value = default(T);

            try
            {
                value = (T)Convert.ChangeType(await cmd.ExecuteScalarAsync(), typeof(T));
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);
            }

            return value;
        }

        /// <summary>
        /// Выполнение скрипта для возврата одного значения, при ошибке вернет null и сообщение в обработчик
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип</typeparam>
        /// <param name="query">Запрос/Скрипт</param>
        /// <param name="parametrs">Параметры(необязательно)</param>
        /// <returns>Запрашиваемый результат</returns>
        public T GetValue<T>(string query, NpgsqlParameter[] parametrs = null)
        {
            Connect();

            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.Connection = npgSqlConnection;
            cmd.CommandText = query;

            if (parametrs != null)
            {
                cmd.Parameters.AddRange(parametrs);
            }

            T value = default(T);

            try
            {
                value = (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);
            }

            return value;
        }

        /// <summary>
        /// Чтение таблицы по запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <param name="is_void_empty">Если true таблица имеющая 0 строк будет заменена на null</param>
        /// <returns>Таблица</returns>
        async public Task<DataTable> GetTableAsync(string query, NpgsqlParameter[] parametrs = null, bool is_void_empty = false) => 
            await Task.Run(() => GetTable(query, parametrs, is_void_empty));

        /// <summary>
        /// Чтение таблицы по запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <param name="is_void_empty">Если true таблица имеющая 0 строк будет заменена на null</param>
        /// <returns>Таблица</returns>
        public DataTable GetTable(string query, NpgsqlParameter[] parametrs = null, bool is_void_empty = false)
        {
            Connect();

            DataTable dt = new ();
            NpgsqlCommand cmd = new (query, npgSqlConnection);

            if (parametrs != null)
            {
                cmd.Parameters.AddRange(parametrs);
            }

            NpgsqlDataAdapter da = new (cmd);

            try
            {
                da.Fill(dt);
            }
            catch (NpgsqlException ex)
            {
                dt = null;

                HandlerErrror.ErrorReport(ex);
            }
            finally 
            {
                da.Dispose(); 
            }

            if (is_void_empty && dt != null && dt.Rows.Count == 0)
            {
                dt = null;
            }

            return dt;
        }

        /// <summary>
        /// Чтение строки
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <returns>Строка</returns>
        public DataRow GetRow(string query, NpgsqlParameter[] parametrs = null)
        {
            DataTable dt = GetTable(query, parametrs, true);

            return  dt != null ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Чтение строки
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <returns>Строка</returns>
        async public Task<DataRow> GetRowAsync(string query, NpgsqlParameter[] parametrs = null)
        {
            DataTable dt = await GetTableAsync(query, parametrs, true);

            return dt != null ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Чтение колонки
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип колонки</typeparam>
        /// <param name="query"></param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <returns></returns>
        public IEnumerable<T> GetColumn<T>(string query, NpgsqlParameter[] parametrs = null)
        {
            DataTable dt = GetTable(query, parametrs, true);

            return dt is null ? new () : dt.AsEnumerable().Select(i => i.Field<T>(0)).ToList();
        }

        /// <summary>
        /// Чтение колонки
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип колонки</typeparam>
        /// <param name="query"></param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <returns></returns>
        async public Task<IEnumerable<T>> GetColumnAsync<T>(string query, NpgsqlParameter[] parametrs = null)
        {
            DataTable dt = await GetTableAsync(query, parametrs, true);

            return dt is null ? new() : dt.AsEnumerable().Select(i => i.Field<T>(0)).ToList();
        }

        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <param name="cntWork">Количество повторений выполния по умолчания 1 раз</param>
        public void ExecuteQuery(string query, NpgsqlParameter[] parametrs = null, int cntWork = 1)
        {
            try
            {
                Connect();

                NpgsqlCommand cmd = new (query, npgSqlConnection);

                cmd.Parameters.AddRange(parametrs);

                for (int i = 0; i < cntWork; i++)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);
            }
        }

        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="parametrs">Параметры если есть</param>
        /// <param name="cntWork">Количество повторений выполния по умолчания 1 раз</param>
        async public Task ExecuteQueryAsync(string query, NpgsqlParameter[] parametrs = null, int cntWork = 1)
        {
            try
            {
                await ConnectAsync();

                NpgsqlCommand cmd = new(query, npgSqlConnection);

                cmd.Parameters.AddRange(parametrs);

                for (int i = 0; i < cntWork; i++)
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                HandlerErrror.ErrorReport(ex);
            }
        }

        /// <summary>
        /// Сборщик мусора/Деструктор
        /// </summary>
        async public void DisposeAsync()
        {
            await DisconnectAsync();
            npgSqlTransaction?.DisposeAsync();
            npgSqlConnection?.DisposeAsync();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Сборщик мусора/Деструктор
        /// </summary>
        public void Dispose()
        {
            Disconnect();
            npgSqlTransaction?.Dispose();
            npgSqlConnection?.Dispose();
            HandlerErrror?.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Создать клон объект <see cref="NpgsqlProvider"/>
        /// </summary>
        /// <returns>Объект <see cref="NpgsqlProvider"/></returns>
        public NpgsqlProvider Clone() => new NpgsqlProvider(this.NpgsqlConnectionStringBuilder, this.HandlerErrror);
    }
}
