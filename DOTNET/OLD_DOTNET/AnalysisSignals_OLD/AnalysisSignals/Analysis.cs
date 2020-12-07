using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalysisSignals.Services;

namespace AnalysisSignals
{
    public class Analysis
    {
        readonly IAnalysisMethod analysisMethod;
        readonly IConnectionBuilder connectionBuilder;
        readonly IDataFetcher dataFetcher;

        public Analysis( IConnectionBuilder connectionBuilder, IDataFetcher dataFetcher, IAnalysisMethod analysisMethod)
        {
            this.connectionBuilder = connectionBuilder;
            this.dataFetcher = dataFetcher;
            this.analysisMethod = analysisMethod;
        }

        public async void PassParameters(Dictionary<string, string> dictionary)
        {
            try
            {
                //string[] PassingArguments = this.property.PassArguments(args);

                //var test = this.calculator.Calculate();

                var AnalysisValue = await analysisMethod.DoMethod(dictionary);

                string tmp = "";

                string AnalysisId = dictionary.TryGetValue("analysisid", out tmp) ? tmp : "";

                string Symbol = dictionary.TryGetValue("symbol", out tmp) ? tmp : "";

                string Market_Timestamp = dictionary.TryGetValue("market_timestamp", out tmp) ? tmp : "";

                string TargetTable = dictionary.TryGetValue("market_timestamp", out tmp) ? tmp : "";

                //string SqlString = this.BuildSqlCommand(Symbol, AnalysisId, Market_Timestamp, AnalysisValue);

                //Task.WhenAll(AnalysisValue);

                await this.dataFetcher.BulkMerge("dbo.F_AnalysisData", AnalysisValue);

                //this.databaseService.ImportToTargetSchema(storedProcedure, Int32.Parse(PassingArguments[4]));

            }
            catch (Exception e)
            {
                Console.WriteLine("Skipped one FlowTask: {0}.", e.Message);
            }
        }

        public string BuildSqlCommand(string Symbol, string AnalysisId, string Market_Timestamp, decimal AnalysisValue)
        {

            string SqlCommand = " INSERT INTO dbo.F_AnalysisData " +
                                " (AnalysisId, Symbol, AnalysisValue, Market_Timestamp) " +
                                " VALUES(" + AnalysisId+",\'"+ Symbol + "\',"+ AnalysisValue + ",\'" + Market_Timestamp + "\')";

            return SqlCommand;
        }

    }
}
