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
    public class RSI: IAnalysisMethod
    {

        readonly IDataFetcher dataFetcher;

        public RSI(IDataFetcher dataFetcher)
        {
            this.dataFetcher = dataFetcher;
        }

        public async Task <List<AnalysisData>> DoMethod(Dictionary<string, string> dictionary)
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

            //Calc Ups and Downs
            for (int i = 1; i < analysisDatas.Count; i++)
            {
                decimal tempUp = analysisDatas[i].Close - analysisDatas[i - 1].Close;

                decimal tempDown = analysisDatas[i - 1].Close - analysisDatas[i].Close;

                // Calc all Ups and Downs
                analysisDatas[i].Up = (tempUp >= 0) ? tempUp : 0;

                analysisDatas[i].Down = (tempDown >= 0) ? tempDown : 0;


                if ( i == Interval)
                {

                    analysisDatas[i].AvgUp = analysisDatas.GetRange(i - Interval, Interval).ToList().Select(x => x.Up).Sum() / Interval;

                    analysisDatas[i].AvgDown = analysisDatas.GetRange(i - Interval, Interval).ToList().Select(x => x.Down).Sum() / Interval;

                   decimal RS = 
                    (analysisDatas.GetRange(i - Interval, Interval).ToList().Select(x => x.Up).Sum() / Interval) / (analysisDatas.GetRange(i - Interval, Interval).ToList().Select(x => x.Down).Sum() / Interval);

                    analysisDatas[i].AnalysisValue = 100 - 100 / (1 + RS);

                }

                if (i > Interval)
                {
                    analysisDatas[i].AvgUp = (analysisDatas[i-1].AvgUp * (Interval - 1) + analysisDatas[i].Up) / Interval;

                    analysisDatas[i].AvgDown = (analysisDatas[i-1].AvgDown * (Interval - 1) + analysisDatas[i].Down) / Interval;

                    //Console.WriteLine("{0} {1} {2} {3} {4}", i, analysisDatas[i].Market_Timestamp, analysisDatas[i].Close, analysisDatas[i].AvgUp, analysisDatas[i].AvgDown);

                    //decimal RS = 0;
                    decimal RS = analysisDatas[i].AvgUp / analysisDatas[i].AvgDown;
                    analysisDatas[i].AnalysisValue = 100 - 100 / (1 + RS);

                }



            }

            List<AnalysisData> AnalysisData = new List<AnalysisData>();

            for (int i = 0; i < analysisDatas.Count; i++)
            {
                AnalysisData Tmp = new AnalysisData(analysisDatas[i].Symbol, analysisDatas[i].Market_Timestamp, AnalysisId, analysisDatas[i].AnalysisValue, "V");

                AnalysisData.Add(Tmp);
            }


            return AnalysisData;
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
