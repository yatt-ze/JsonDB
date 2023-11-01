using JsonDB.Exceptions;
using JsonDB.Structure;

namespace JsonDB.Querys
{
    public class InsertQuery
    {
        private List<Table> Tables { get; set; }
        private string TableName { get; set; }
        private List<object> Data { get; set; }

        public InsertQuery(List<object> data, List<Table> tables)
        {
            Data = data;
            Tables = tables;
        }

        public InsertQuery Into(string tableName)
        {
            TableName = tableName;
            return this;
        }

        public void Execute()
        {
            Table? selectedTable = Tables.Find(t => t.Name == TableName);
            if (selectedTable == null)
                throw new JsonDbException($"Table '{TableName}' not found");
            if (Data.Count != selectedTable.Columns.Count)
                throw new JsonDbException(nameof(Data));

            Row row = new Row(selectedTable);
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i] == null && !selectedTable.Columns[i].IsNullable())
                    throw new JsonDbException($"Data at index {i} was Null, Column {selectedTable.Columns[i].Name} is not Nullable");
                if (Data[i] == null && selectedTable.Columns[i].IsNullable())
                    i++;
                if (!selectedTable.Columns[i].MatchType(Data[i].GetType()))
                    throw new JsonDbException($"Data at index {i} did not match column type {selectedTable.Columns[i].Type}");

                row.Data.Add(Data[i]);
            }
            selectedTable.Rows.Add(row);
        }
    }
}