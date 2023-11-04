using JsonDB;
using JsonDB.Structure;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {
        public string TestDatabase = "[{\"Name\":\"People\",\"Columns\":[{\"Type\":1,\"Name\":\"ID\",\"Nullable\":false},{\"Type\":2,\"Name\":\"Name\",\"Nullable\":true},{\"Type\":0,\"Name\":\"Alive\",\"Nullable\":true}],\"Rows\":[]}]";

        [TestMethod]
        public void CreateTable()
        {
            Database db = new Database();
            db.Create("Cars")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Brand")
                .AddColumn(ColumnType.String, "Name");

            Assert.IsTrue(db.Tables.Count == 1);
        }

        [TestMethod]
        public void OpenDatabase()
        {
            Database db = new Database();
            db.Open(TestDatabase);

            Assert.IsTrue(db.Tables.Count == 1, "Database should have one table");
            Assert.IsTrue(db.Tables[0].Columns[0].Name == "ID", "The tables first column is named ID");
        }

        [TestMethod]
        public void SaveDatabase()
        {
            Database db = new Database();
            db.Create("Cars")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Brand")
                .AddColumn(ColumnType.String, "Name");

            string expectedString = "[{\"Name\":\"Cars\",\"Columns\":[{\"Type\":1,\"Name\":\"ID\",\"Nullable\":false},{\"Type\":2,\"Name\":\"Brand\",\"Nullable\":true},{\"Type\":2,\"Name\":\"Name\",\"Nullable\":true}],\"Rows\":[]}]";
            string savedDatabase = db.Save();
            Assert.IsTrue(savedDatabase.Equals(expectedString));
        }
    }
}