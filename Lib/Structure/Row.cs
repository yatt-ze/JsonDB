using JsonDB.Exceptions;

namespace JsonDB.Structure
{
    public class Row
    {
        private readonly Table _parentTable;
        public List<object> Data { get; set; }

        public Row(Table parentTable)
        {
            _parentTable = parentTable;
            Data = new List<object>();
        }

        public object GetData(string columnName)
        {
            for (int i = 0; i < _parentTable.Columns.Count; i++)
            {
                Column column = _parentTable.Columns[i];
                if (column.Name == columnName)
                    return Data[i];
            }
            throw new JsonDbException($"{columnName} not found");
        }
    }
}