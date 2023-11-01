using System.Text.Json;
using JsonDB.Querys;
using JsonDB.Structure;

namespace JsonDB
{
    public class Database
    {
        public List<Table> Tables { get; set; }

        public Database(string dbPath = "")
        {
            Tables = new List<Table>();
        }

        public Table Create(string Name)
        {
            Table table = new Table(Name);
            Tables.Add(table);
            return table;
        }

        public InsertQuery Insert(params object[] data)
        {
            return new InsertQuery(data.ToList(), Tables);
        }

        public SelectQuery Select(params string[] rows)
        {
            return new SelectQuery(rows, Tables);
        }

        public void Test()
        {
            // Serialize the table to JSON and print it
            string json = JsonSerializer.Serialize(Tables, new JsonSerializerOptions
            {
                WriteIndented = true // for pretty-printing the JSON
            });
            Console.WriteLine(json);
        }
    }
}