using JsonDB;
using JsonDB.Structure;
using System.Diagnostics;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Database database = new Database();
            database.Create("People")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Name")
                .AddColumn(ColumnType.Bool, "Alive");

            Console.WriteLine(database.Save());

            database.Insert(1, "John", true).Into("People").Execute();
            database.Insert(2, "Alice", false).Into("People").Execute();
            database.Insert(3, "Bob", true).Into("People").Execute();

            Console.WriteLine("SELECT * FROM People");
            Console.WriteLine("=========================================");
            database.Select("*").From("People")
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Name: {row.GetData("Name")} | Alive: {row.GetData("Alive")}")
                );

            Console.WriteLine("\nSELECT * FROM People LIMIT 1");
            Console.WriteLine("=========================================");
            database.Select("*").From("People")
                .Limit(1)
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Name: {row.GetData("Name")} | Alive: {row.GetData("Alive")}")
                );

            Console.WriteLine("\nSELECT * FROM People WHERE ID = 3");
            Console.WriteLine("=========================================");
            database.Select("*")
                .From("People")
                .Where(row => (int)row.GetData("ID") == 3)
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Name: {row.GetData("Name")} | Alive: {row.GetData("Alive")}")
                );

            Console.WriteLine("\nSELECT ID, Alive FROM People WHERE Alive = true");
            Console.WriteLine("=========================================");
            database.Select("ID", "Alive").From("People")
                .Where(row => (bool)row.GetData("Alive") == true)
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Alive: {row.GetData("Alive")}")
                );

            Console.WriteLine("\nDELETE FROM People WHERE Alive = true");
            Console.WriteLine("=========================================");
            database.Delete()
                .From("People")
                .Where(row => (bool)row.GetData("Alive") == true)
                .Execute();
            database.Select("*").From("People")
               .Execute()
               .ForEach(row =>
                   Console.WriteLine($"ID: {row.GetData("ID")} | Name: {row.GetData("Name")} | Alive: {row.GetData("Alive")}")
               );
        }
    }
}