using DataBaseProvaider;
using Npgsql;
using TestCMD;

DBProvider.NpgsqlProvider = new(new NpgsqlConnectionStringBuilder()
{
    Host = "pg4.sweb.ru",
    Port = 5433,
    Username = "daniilkupt",
    Password = "Ironmaiden01",
    Database = "daniilkupt"
});

DBProvider.NpgsqlProvider.HandlerErrror.ErrorReporter = new Progress<string>(message => Console.WriteLine(message));

Console.WriteLine((await DBProvider.GetModel<Inventory>(1)).Name);
Console.WriteLine((await DBProvider.GetModel<Inventory>(2)).Name);

Console.WriteLine();

Console.WriteLine(String.Join("\n", (await DBProvider.GetCollectionModel<Inventory>()).Select(i => i.Name)));

Console.WriteLine();
