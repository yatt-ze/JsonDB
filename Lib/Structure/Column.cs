namespace JsonDB.Structure
{
    public class Column
    {
        public ColumnType Type { get; set; }
        public string Name { get; set; }
        private bool Nullable { get; set; }

        public Column(ColumnType type, string name, bool nullable = true)
        {
            Type = type;
            Name = name;
            Nullable = nullable;
        }

        public bool IsNullable() => Nullable;

        public bool MatchType(Type? type)
        {
            if (type == typeof(bool) && Type == ColumnType.Bool) return true;
            if (type == typeof(int) && Type == ColumnType.Int) return true;
            if (type == typeof(string) && Type == ColumnType.String) return true;

            return false;
        }
    }
}