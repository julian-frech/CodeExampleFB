using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorService.Models
{
	[Table("F_ANALYSIS", Schema = "ANALYSIS")]
	public class Analysis
	{
		public int CalculationId { get; set; }
		public decimal Parameter { get; set; }
		public string SymbolId { get; set; }
		public DateTime MarketTimestamp { get; set; }
		public decimal AnalysisValue { get; set; }
		public DateTime? ValidFrom { get; set; }
		public DateTime? ValidTo { get; set; }
		public DateTime? ITS { get; set; }
		public DateTime? UTS { get; set; }

		public Analysis() { }

        public Analysis(int CalculationId_, decimal Parameter_, string SymbolId_, DateTime MarketTimestamp_, decimal AnalysisValue_, DateTime? ValidFrom_, DateTime? ValidTo_, DateTime? ITS_, DateTime? UTS_)
        {
            this.CalculationId = CalculationId_;
            this.Parameter = Parameter_;
            this.SymbolId = SymbolId_;
            this.MarketTimestamp = MarketTimestamp_;
            this.AnalysisValue = AnalysisValue_;
            this.ValidFrom = ValidFrom_;
            this.ValidTo = ValidTo_;
            this.ITS = ITS_;
            this.UTS = UTS_;
        }

        public Analysis(int CalculationId_, decimal Parameter_, string SymbolId_, DateTime MarketTimestamp_, decimal AnalysisValue_)
        {
            this.CalculationId = CalculationId_;
            this.Parameter = Parameter_;
            this.SymbolId = SymbolId_;
            this.MarketTimestamp = MarketTimestamp_;
            this.AnalysisValue = AnalysisValue_;
            this.ValidFrom = DateTime.Now;
            this.ValidTo = DateTime.Parse("9999-12-31 00:00:00.000");
            this.ITS = DateTime.Now;
            this.UTS = DateTime.Now;
        }

    }




}
