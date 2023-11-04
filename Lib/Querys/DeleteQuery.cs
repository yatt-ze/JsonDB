using JsonDB.Structure;
using JsonDB.Exceptions;

namespace JsonDB
{
    public class DeleteQuery
    {
        private List<Table> Tables { get; set; }
        private string SelectedTable { get; set; }
        private Func<Row, bool> WhereCondition { get; set; }

        public DeleteQuery(List<Table> tables)
        {
            Tables = tables;
        }

        public DeleteQuery From(string tableName)
        {
            SelectedTable = tableName;
            return this;
        }

        public DeleteQuery Where(Func<Row, bool> condition)
        {
            WhereCondition = condition;
            return this;
        }

        public void Execute()
        {
            Table? selectedTable = Tables.Find(t => t.Name == SelectedTable);
            if (selectedTable == null)
                throw new JsonDbException($"Table '{SelectedTable}' not found");

            if (WhereCondition == null)
            {
                selectedTable.Rows.Clear();
            }
            else
            {
                selectedTable.Rows.Where(WhereCondition)
                    .ToList()
                    .ForEach(row =>
                        selectedTable.Rows.Remove(row)
                    );
            }
        }
    }
}