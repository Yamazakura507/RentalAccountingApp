using Newtonsoft.Json;
using Npgsql;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Classes
{
    public static class ConnectionInfo
    {
        public static ConnectionElement[] Conections => JsonConvert.DeserializeObject<ConnectionElement[]>(File.ReadAllText("connection_list.json"));

        public static ConnectionElement ActiveConnection => Conections.FirstOrDefault(i => i.IsActive);

        public static NpgsqlConnectionStringBuilder DefaultConnection => new()
        { 
            Host = Properties.Settings.Default.Host, 
            Port = Properties.Settings.Default.Port,
            Username = Properties.Settings.Default.Login,
            Password = Properties.Settings.Default.Password,
            Database = Properties.Settings.Default.DataBase
        };
    }
}
