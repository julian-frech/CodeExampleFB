using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AnalysisSignals.Model;
using AnalysisSignals.Services;
using MathNet.Numerics;

namespace AnalysisSignals.AnalysisDiffMethods
{
    public class AverageSimple : IAnalysisMethod
    {

        readonly IDataFetcher dataFetcher;

        public AverageSimple(IDataFetcher dataFetcher)
        {
            this.dataFetcher = dataFetcher;
        }

        public async Task<List<AnalysisData>> DoMethod(Dictionary<string, string> dictionary)
        {
            ///Get all necessary parts of the Dictionary
            ///
            string tmp = "";

            string Symbol = dictionary.TryGetValue("symbol", out tmp) ? tmp : "";

            string Market_Timestamp = dictionary.TryGetValue("market_timestamp", out tmp) ? tmp : "";

            int Interval = dictionary.TryGetValue("interval", out tmp) ? Int32.Parse(tmp) : 0;

            string Weight = dictionary.TryGetValue("weight", out tmp) ? tmp : "";

            string SqlSelect = this.SqlSelectStringBuilder(Market_Timestamp, Interval, Symbol);

            int AnalysisId = dictionary.TryGetValue("analysisid", out tmp) ? Int32.Parse(tmp) : 0;

            ///////////////////////
            ///

            // Get Market Data from DB
            DataTable dataTable = await dataFetcher.ReadFromDatabase(SqlSelect);

            // Parse Data to Object
            List<AnalysisDataTmp> analysisDatas = this.ConvertToObjList(dataTable, Symbol);

            //Order Data accordingly
            analysisDatas.OrderBy(x => x.Market_Timestamp);

            if (analysisDatas.Count > Interval)
            {
                List<AnalysisData> AnalysisData = new List<AnalysisData>();

                for (int i = Interval; i < analysisDatas.Count; i++)
                {

                    analysisDatas[i].AnalysisValue = analysisDatas.GetRange(i - Interval, Interval).ToList().Select(x => x.Close).Sum() / Interval;

                    AnalysisData Tmp = new AnalysisData(analysisDatas[i].Symbol, analysisDatas[i].Market_Timestamp, AnalysisId, analysisDatas[i].AnalysisValue, "V");

                    AnalysisData.Add(Tmp);
                }

                return AnalysisData;
            }
            else
            {
                return null;
            }
            
        }

        private string SqlSelectStringBuilder(string Market_Timestamp, int Interval, string Symbol)
        {

            string SqlSelect = "Select [Close],[Market_Timestamp] FROM production.F_MARKET_DATA " +
                " WHERE Symbol = \'" + Symbol + "\' " +
                " AND EOD = 1";

            Console.WriteLine(SqlSelect);

            return SqlSelect;
        }

        private List<AnalysisDataTmp> ConvertToObjList(DataTable dataTable, string Symbol)
        {
            List<AnalysisDataTmp> AnalysisData = new List<AnalysisDataTmp>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                AnalysisDataTmp tmp = new AnalysisDataTmp(Symbol, Convert.ToDateTime(dataTable.Rows[i]["Market_Timestamp"]), Convert.ToDecimal(dataTable.Rows[i]["Close"]));

                AnalysisData.Add(tmp);
            }

            return AnalysisData;
        }

    }
}
