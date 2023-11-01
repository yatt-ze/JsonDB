﻿using JsonDB.Exceptions;
using JsonDB.Structure;

namespace JsonDB.Querys
{
    public class SelectQuery
    {
        private List<string> SelectedColumns { get; set; }
        private string TableName { get; set; }
        private List<Table> Tables { get; set; }

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

        public List<Row> Execute()
        {
            Table? selectedTable = Tables.Find(t => t.Name == TableName);
            if (selectedTable == null)
                throw new JsonDbException($"Table '{TableName}' not found.");

            Table queryTable = new Table(selectedTable.Name);
            foreach (var c in selectedTable.Columns.Where(c => SelectedColumns.Contains(c.Name)))
            {
                queryTable.Columns.Add(c);
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
            {
                selectedRows = selectedRows.Where(WhereCondition).ToList();
            }

            return selectedRows;
        }
    }
}