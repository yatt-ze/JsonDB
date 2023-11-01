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

            database.Create("Places")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Name")
                .AddColumn(ColumnType.String, "Address");

            database.Insert(1, "John", true).Into("People").Execute();
            database.Insert(2, "Alice", false).Into("People").Execute();
            database.Insert(3, "Bob", true).Into("People").Execute();
            database.Insert(1, "Doctors", "123 Doctor Street").Into("Places").Execute();
            database.Insert(2, "School", "456 School Street").Into("Places").Execute();

            database.Test();

            Console.WriteLine("SELECT ID, Alive FROM People WHERE ALIVE = true");
            Console.WriteLine("=========================================");
            database.Select("ID", "Alive").From("People")
                .Where(row => (bool)row.GetData("Alive") == true)
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Alive: {row.GetData("Alive")}")
                );

            Console.WriteLine("\nSELECT ID, Address FROM Places");
            Console.WriteLine("=========================================");
            database.Select("ID", "Address")
                .From("Places")
                .Execute()
                .ForEach(row =>
                    Console.WriteLine($"ID: {row.GetData("ID")} | Address: {row.GetData("Address")}")
                );
        }
    }
}