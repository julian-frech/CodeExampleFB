using System;
using System.Data;

namespace UTReportService.UTData
{
    public class RandomDataTable
    {
        
        private readonly Random _random = new Random();

        public DataTable CreateDataTable(int NumberOfColumns, int j)
        {
            DataTable table = new DataTable("TestTable");
            DataColumn idColumn = new DataColumn("id", typeof(int));
            DataColumn numberColumn = new DataColumn("number", typeof(double));
            DataColumn dateColumn = new DataColumn("date", typeof(DateTime));
            DataColumn stringColumn = new DataColumn("stringName", typeof(string));

            table.Columns.Add(idColumn);
            table.Columns.Add(numberColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(stringColumn);

            for (int i = 1; i <= NumberOfColumns; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["id"] = i;
                newRow["number"] = j;
                newRow["date"] = DateTime.Parse("01/01/2021");
                newRow["stringName"] = "Placeholder for string value";
                table.Rows.Add(newRow);
            }


            return table;
        }

        
    }
}
