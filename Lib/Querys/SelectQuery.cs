using JsonDB.Exceptions;
using JsonDB.Structure;

namespace JsonDB.Querys
{
    public class SelectQuery
    {
        private List<string> SelectedColumns { get; set; }
        private string TableName { get; set; }
        private List<Table> Tables { get; set; }
        private int LimitAmount { get; set; }

        public SelectQuery(string[] rows, List<Table> tables)
        {
            SelectedColumns = new List<string>(rows);
            Tables = tables;
        }

        public SelectQuery From(string tableName)
        {
            TableName = tableName;
            return this;
        }

        private Func<Row, bool> WhereCondition { get; set; }

        public SelectQuery Where(Func<Row, bool> condition)
        {
            WhereCondition = condition;
            return this;
        }

        public SelectQuery Limit(int limit)
        {
            LimitAmount = limit;
            return this;
        }

        public List<Row> Execute()
        {
            Table? selectedTable = Tables.Find(t => t.Name == TableName);
            if (selectedTable == null)
                throw new JsonDbException($"Table '{TableName}' not found.");

            Table queryTable = new Table(selectedTable.Name);

            if (SelectedColumns.Contains("*"))
            {
                queryTable = selectedTable;
                SelectedColumns.Clear();
                SelectedColumns.AddRange(selectedTable.Columns.Select(c => c.Name));
            }
            else
            {
                queryTable.Columns.AddRange(selectedTable.Columns
                .Where(c => SelectedColumns.Contains(c.Name))
                .ToList());
            }

            List<Row> selectedRows = new List<Row>();

            foreach (var row in selectedTable.Rows)
            {
                Row insertRow = new Row(queryTable);
                for (int i = 0; i < SelectedColumns.Count; i++)
                {
                    if (i < selectedTable.Columns.Count)
                    {
                        int columnIndex = selectedTable.Columns.FindIndex(c => c.Name == SelectedColumns[i]);
                        if (columnIndex != -1)
                        {
                            insertRow.Data.Add(row.Data[columnIndex]);
                        }
                        else
                        {
                            insertRow.Data.Add(null);
                        }
                    }
                    else
                    {
                        insertRow.Data.Add(null);
                    }
                }

                selectedRows.Add(insertRow);
            }

            if (WhereCondition != null)
                selectedRows = selectedRows.Where(WhereCondition).ToList();
            if (LimitAmount > 0)
                selectedRows = selectedRows.Take(LimitAmount).ToList();

            return selectedRows;
        }
    }
}