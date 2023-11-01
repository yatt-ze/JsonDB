using JsonDB;
using JsonDB.Structure;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void TestCreateTable()
        {
            Database db = new Database();
            db.Create("People")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Name")
                .AddColumn(ColumnType.Bool, "Alive");

            Assert.IsTrue(db.Tables.Count == 1);
        }

        [TestMethod]
        public void TestInsert()
        {
            Database db = new Database();
            db.Create("People")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Name")
                .AddColumn(ColumnType.Bool, "Alive");

            db.Insert(1, "John", true).Into("People").Execute();
            Assert.IsTrue(db.Tables[0].Rows.Count == 1);
            db.Insert(2, "Alice", false).Into("People").Execute();
            Assert.IsTrue(db.Tables[0].Rows.Count == 2);
            db.Insert(3, "Bob", true).Into("People").Execute();
            Assert.IsTrue(db.Tables[0].Rows.Count == 3);
        }

        [TestMethod]
        public void TestSelect()
        {
            Database db = new Database();
            db.Create("People")
                .AddColumn(ColumnType.Int, "ID", false)
                .AddColumn(ColumnType.String, "Name")
                .AddColumn(ColumnType.Bool, "Alive");

            db.Insert(1, "John", true).Into("People").Execute();
            db.Insert(2, "Alice", false).Into("People").Execute();
            db.Insert(3, "Bob", true).Into("People").Execute();

            List<Row> ret = db.Select("ID", "Name", "Alive").From("People").Execute();

            Assert.IsTrue((int)ret[0].GetData("ID") == 1);
            Assert.IsTrue((string)ret[0].GetData("Name") == "John");
            Assert.IsTrue((bool)ret[0].GetData("Alive") == true);

            Assert.IsTrue((int)ret[1].GetData("ID") == 2);
            Assert.IsTrue((string)ret[1].GetData("Name") == "Alice");
            Assert.IsTrue((bool)ret[1].GetData("Alive") == false);

            Assert.IsTrue((int)ret[2].GetData("ID") == 3);
            Assert.IsTrue((string)ret[2].GetData("Name") == "Bob");
            Assert.IsTrue((bool)ret[2].GetData("Alive") == true);
        }
    }
}