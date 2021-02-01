using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using ReportWriter;
using ReportWriter.Service;
using UTReportService.UTData;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ReportWriter.Service;
using Microsoft.Extensions.Options;
using FileWriter.ConfigurationOption;

namespace UTReportService.UTReportWriter
{
    [TestClass]
    public class UTReportService
    {


        [TestMethod]
        public void FileWriterUT()
        {

            var fileWriterConfig = new FileWriterConfig();

            fileWriterConfig.BaseFolder = @"./";

            IOptions<FileWriterConfig> someOptions = Options.Create<FileWriterConfig>(fileWriterConfig);


            var mock = new Mock<ILogger<ReportWriter.Service.FileWriter>>();

            ILogger<ReportWriter.Service.FileWriter> logger = mock.Object;

            IFileWriter fileWriter = new ReportWriter.Service.FileWriter(logger, someOptions) ;

            IReportFile reportService = new ReportFile(fileWriter);

            RandomDataTable dataCreator = new RandomDataTable();

            var testData = dataCreator.CreateDataTable(10,1);

            Console.WriteLine(testData);

            reportService.ReportAsFileAsync(testData, "testReport.csv","" , ";", "Row1;Row2;Row3;Row4") ;

            var Expected = File.ReadAllText(@"./expectedReport.csv");

            var Created = File.ReadAllText("./testReport.csv");

            Assert.AreEqual(Expected, Created);

            System.IO.File.Delete(@"./testReport.csv");
        }
    }
}
