using System;
using System.Data;
using System.Threading.Tasks;
using ReportWriter.Service;
using ReportWriter.Service.Extensions;

namespace ReportWriter
{
    /// <summary>
    /// Interface of the ReportWriter class containing method to create reports.
    /// </summary>
    public interface IReportFile
    {
        /// <summary>
        /// Access point for writing datatable input as txt delimiter formatted files.
        /// </summary>
        /// <param name="reportAsdataTable">Report data as datatable</param>
        /// <param name="fileName">Name of the report</param>
        /// <param name="targetLocationExtension">Target folder of the report file</param>
        /// <param name="delimiter">String based delimiter i.e. ";" or ","</param>
        /// <returns></returns>
        Task ReportAsFileAsync(DataTable reportAsdataTable, string fileName, string targetLocationExtension, string delimiter, string headerRow);
    }

    /// <summary>
    /// Class storing methodology to create reports.
    /// </summary>
    public class ReportFile : IReportFile
    {
        private readonly IFileWriter _fileWriter;

        public ReportFile(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        /// <summary>
        /// Access point for writing datatable input as txt delimiter formatted files.
        /// </summary>
        /// <param name="reportAsdataTable">Report data as datatable</param>
        /// <param name="fileName">Name of the report</param>
        /// <param name="targetLocation">Target folder of the report file</param>
        /// <param name="delimiter">String based delimiter i.e. ";" or ","</param>
        /// <returns></returns>
        public async Task ReportAsFileAsync(DataTable reportAsdataTable, string fileName, string targetLocationExtension, string delimiter, string headerRow)
        {
            await _fileWriter.StringToFile(targetLocationExtension + fileName,
                                    reportAsdataTable.ToCsv(delimiter, headerRow)
                                    );
        }
    }
}
