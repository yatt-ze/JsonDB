using JsonDB.Exceptions;
using JsonDB.Structure;

namespace JsonDB
{
    public class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();

        public Table(string name)
        {
            Name = name;
        }

        public Table AddColumn(ColumnType type, string name, bool nullable = true)
        {
            Columns.Add(new Column(type, name, nullable));
            return this;
        }
    }
}