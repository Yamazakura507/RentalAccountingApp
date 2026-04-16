using Npgsql;
using System.Text.Json.Serialization;

namespace RentalAccountingApp.Classes.Model
{
    /// <summary>
    /// Объект с информацией о соединении
    /// </summary>
    public class ConnectionElement : IDisposable
    {
        /// <summary>
        /// Метка активного соединения
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Метка запрещающая удаление элемента
        /// </summary>
        public bool IsNotDelete { get; set; }

        /// <summary>
        /// Метка указывающая, отсутствие соединения
        /// </summary>
        public bool IsDiconnect { get; set; }

        /// <summary>
        /// Визуальная метка подключения
        /// </summary>
        public string Name { get => NameGeneration(); }

        /// <summary>
        /// Строка соединения
        /// </summary>
        public NpgsqlConnectionStringBuilder ConnectionBuilder { get; set; }

        /// <summary>
        /// Конструктор объекта с информацией о соединении
        /// </summary>
        /// <param name="isActive">Метка активного соединения</param>
        /// <param name="connectionBuilder">Строка сединения</param>
        /// <param name="isNotDelete">Метка запрещающая удаление</param>
        public ConnectionElement(bool isActive, NpgsqlConnectionStringBuilder connectionBuilder, bool isNotDelete = false)
        { 
            this.IsActive = isActive;
            this.ConnectionBuilder = connectionBuilder;
            this.IsNotDelete = isNotDelete;
        }

        /// <summary>
        /// Конструктор объекта с информацией о соединении
        /// </summary>
        /// <param name="isActive">Метка активного соединения</param>
        /// <param name="host">Хост сединения</param>
        /// <param name="port">Порт соединения</param>
        /// <param name="username">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="database">Имя базы данных</param>
        /// <param name="isNotDelete">Метка запрещающая удаление</param>
        public ConnectionElement(bool isActive, string host, int port, string username, string password, string database, bool isNotDelete = false)
        {
            this.IsActive = isActive;
            this.ConnectionBuilder = new NpgsqlConnectionStringBuilder() 
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = database
            };
        }

        [JsonConstructor]
        public ConnectionElement() { }

        /// <summary>
        /// Метод генерирации визуальной метки соединения
        /// </summary>
        /// <returns>Визуальная метка</returns>
        private string NameGeneration()
        {
            return $"{this.ConnectionBuilder.Username} {this.ConnectionBuilder.Host}:{this.ConnectionBuilder.Port}";
        }

        /// <summary>
        /// Деструктор/Очистка памяти от объекта
        /// </summary>
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
