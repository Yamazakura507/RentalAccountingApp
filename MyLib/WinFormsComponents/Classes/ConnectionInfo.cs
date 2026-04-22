using DataBaseProvaider;
using Newtonsoft.Json;
using Npgsql;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Classes
{
    public static class ConnectionInfo
    {
        /// <summary>
        /// Список соединений
        /// </summary>
        public static ConnectionElement[] Conections => JsonConvert.DeserializeObject<ConnectionElement[]>(File.ReadAllText("connection_list.json"));
        /// <summary>
        /// Активное соединение
        /// </summary>
        public static ConnectionElement ActiveConnection => Conections.FirstOrDefault(i => i.IsActive);
        /// <summary>
        /// Соединение по умолчанию
        /// </summary>
        public static NpgsqlConnectionStringBuilder DefaultConnection => new()
        { 
            Host = Properties.Settings.Default.Host, 
            Port = Properties.Settings.Default.Port,
            Username = Properties.Settings.Default.Login,
            Password = Properties.Settings.Default.Password,
            Database = Properties.Settings.Default.DataBase
        };

        /// <summary>
        /// Добавление подключения
        /// </summary>
        public static void ConnectDB()
        {
            DBProvider.NpgsqlProvider = new(ConnectionInfo.ActiveConnection?.ConnectionBuilder ?? ConnectionInfo.DefaultConnection);
            DBProvider.NpgsqlProvider.HandlerErrror.ErrorReporter = new Progress<string>(async message => InfoViewer.ErrrorMessege(message));
        }

        /// <summary>
        /// Сохранение списка соединений
        /// </summary>
        /// <param name="connections">Список соединений</param>
        public static void Save(this ConnectionElement[] connections)
        {
            string updatedJson = JsonConvert.SerializeObject(connections);
            File.WriteAllText("connection_list.json", updatedJson);

            ConnectDB();
        }

        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <param name="connectionBuilder">Строка соединения</param>
        /// <returns>Результат проверкаи</returns>
        public static bool IsCheckConection(this NpgsqlConnectionStringBuilder connectionBuilder)
        {
            using (NpgsqlConnection connection = new(connectionBuilder.ToString()))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
