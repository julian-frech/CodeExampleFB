using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalysisSignals.Model
{
    [Table("F_AnalysisData", Schema = "dbo")]
    public class AnalysisData
    {

        [Key] public int AnalysisId;
        [Key] public string Symbol;
        [Key] public DateTime Market_Timestamp;
        public decimal AnalysisValue;
        [Key] public string Status;

        public AnalysisData(string symbol, DateTime market_Timestamp, int analysisId, decimal analysisValue, string status)
        {
            this.Symbol = symbol;
            this.Market_Timestamp = market_Timestamp;
            this.AnalysisId = analysisId;
            this.AnalysisValue = analysisValue;
            this.Status = status;
        }

    }


    public class AnalysisDataTmp
    {

        public int AnalysisId;
        public string Symbol;
        public DateTime Market_Timestamp;
        public DateTime ValidFrom;
        public DateTime ValidTo;

        public decimal Close;
        public decimal Up;
        public decimal Down;
        public decimal AvgUp;
        public decimal AvgDown;
        public decimal AnalysisValue;
       

        public AnalysisDataTmp(string symbol, DateTime market_Timestamp, decimal close)
        {
            this.Symbol = symbol;
            this.Market_Timestamp = market_Timestamp;
            this.Close = close;
        }

    }




}
