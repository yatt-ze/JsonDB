using System.Collections.Generic;
using System.Text.Json;
using JsonDB.Querys;
using JsonDB.Structure;

namespace JsonDB
{
    public class Database
    {
        public List<Table> Tables { get; set; }

        public Database()
        {
            Tables = new List<Table>();
        }

        public bool Open(string JsonData)
        {
            if (!string.IsNullOrEmpty(JsonData))
            {
                try
                {
                    List<Table>? tempTables = JsonSerializer.Deserialize<List<Table>>(JsonData);
                    if (tempTables != null)
                    {
                        Tables.Clear();
                        Tables = tempTables;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public string Save() => JsonSerializer.Serialize(Tables);

        public Table Create(string tableName)
        {
            Table table = new Table(tableName);
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

        public DeleteQuery Delete()
        {
            return new DeleteQuery(Tables);
        }
    }
}