using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                IEnumerable<string> columns = drow.ItemArray.Select(column => column.ToString());
                result.Append('\n'+string.Join(delimiter, columns));
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
            result.AppendLine(headerRow);

            foreach (DataRow row in datatable.Rows)
            {
                IEnumerable<string> columns = row.ItemArray.Select(column => column.ToString());
                result.AppendLine(string.Join(delimiter, columns));
            }

            return result.ToString();

        }

    }
}
