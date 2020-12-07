using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorService.Calculator;
using CalculatorService.Models;
using CsvHelper;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace CalculatorServiceUT
{
    [TestClass]
    public class AverageExponentialUT
    {
        AverageExponential AE = new AverageExponential();

        List<CalculationData> CalcData = new List<CalculationData>();

        ApiInputClass inputClass = new ApiInputClass("averageexp", "googl", 5, 1);

        List<Analysis> AnalysisDataCalculation = new List<Analysis>();

        List<Analysis> AnalysisDataSolution = new List<Analysis>();

        [TestMethod]
        public async Task CalculateUTAsync()
        {

            using (var readerSolution = new StreamReader("./AverageExponentialSolution.csv"))
            using (var csvSolution = new CsvReader(readerSolution, CultureInfo.InvariantCulture))
            {
                csvSolution.Configuration.MissingFieldFound = null;
                csvSolution.Configuration.HasHeaderRecord = true;

                AnalysisDataSolution = csvSolution.GetRecords<Analysis>().ToList();

                AnalysisDataSolution.ForEach(x => x.ITS = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataSolution.ForEach(x => x.UTS = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataSolution.ForEach(x => x.ValidFrom = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataSolution.ForEach(x => x.ValidTo = DateTime.Parse("9999-12-31 00:00:00.000"));

            }

            using (var reader = new StreamReader("./CalculationDataSample.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CalcDataMap>();
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.HasHeaderRecord = true;

                CalcData = csv.GetRecords<CalculationData>().ToList();

                AnalysisDataCalculation = await AE.Calculate(CalcData, inputClass);
                AnalysisDataCalculation.ForEach(x => x.ITS = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataCalculation.ForEach(x => x.UTS = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataCalculation.ForEach(x => x.ValidFrom = DateTime.Parse("9999-12-31 00:00:00.000"));
                AnalysisDataCalculation.ForEach(x => x.ValidTo = DateTime.Parse("9999-12-31 00:00:00.000"));
            }

            /// 14 digits
            /// Math.Round(AnalysisDataCalculation.AnalysisValue, 14)

            AnalysisDataCalculation.ForEach(x => x.AnalysisValue = Math.Round(x.AnalysisValue,14));

             Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(AnalysisDataCalculation),
                Newtonsoft.Json.JsonConvert.SerializeObject(AnalysisDataSolution));

        }

    }




    public sealed class CalcDataMap : ClassMap<CalculationData>
    {
        public CalcDataMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.AnalysisId).Ignore();
            Map(m => m.AnalysisValue).Ignore();
        }
    }

}
