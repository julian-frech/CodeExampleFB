using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ReportWriter.Service.Extensions
{
    public static class DataTableExtension
    {
        /// <summary>
        /// Parallel method to be used on big datatables. Creates string output with provided
        /// delimiter. Due to parallel mechanism applied to rows the order of the datatable is not kept.
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="delimiter"></param>
        public static string ToCsvParallel(this DataTable datatable, string delimiter, int maxThreads, string headerRow)
        {
            var result = new StringBuilder();
            result.Append(headerRow);
            // Rows in parallel --> No ordering anymore
            Parallel.ForEach(datatable.AsEnumerable(), new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, drow =>
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    result.Append(drow[i].ToString());
                    result.Append(i == /*datatable.Columns.Count - 1*/ 0 ? "\n" : delimiter);
                }
            });

            return result.ToString();
        }

        /// <summary>s
        /// Creates string output with provided delimiter.
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="delimiter"></param>
        public static string ToCsv(this DataTable datatable, string delimiter, string headerRow)
        {
            var result = new StringBuilder();
            result.Append(headerRow + "\n");
            foreach (DataRow row in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == datatable.Columns.Count - 1 ? "\n" : delimiter);
                }
            }
            return result.ToString();

        }

    }
}
