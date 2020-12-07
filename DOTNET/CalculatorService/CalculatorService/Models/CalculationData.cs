using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorService.Models
{

	[Table("VCalculatorInputData", Schema = "Analysis")]
	public class CalculationData
	{
		public string method { get; set; }
		public string symbol { get; set; }
		public DateTime MarketTimestamp { get; set; }
		public decimal? open { get; set; }
		public decimal? high { get; set; }
		public decimal? low { get; set; }
		public decimal? close { get; set; }
		public int? volume { get; set; }
		public decimal? uOpen { get; set; }
		public decimal? uHigh { get; set; }
		public decimal? uLow { get; set; }
		public decimal? uClose { get; set; }
		public int? uVolume { get; set; }
		public string exDate { get; set; }

		/// <summary>
		/// Here are values used for calculation
		/// </summary>
		[NotMapped]
		public int AnalysisId { get; set; }
		[NotMapped]
		public decimal AnalysisValue { get; set; }

		public CalculationData() { }

		public CalculationData(string method_, string symbol_, DateTime marketTimestamp_, decimal? open_, decimal? high_, decimal? low_, decimal? close_, int? volume_, decimal? uOpen_, decimal? uHigh_, decimal? uLow_, decimal? uClose_, int? uVolume_, string exDate_)
		{
			this.method = method_;
			this.symbol = symbol_;
			this.MarketTimestamp = marketTimestamp_;
			this.open = open_;
			this.high = high_;
			this.low = low_;
			this.close = close_;
			this.volume = volume_;
			this.uOpen = uOpen_;
			this.uHigh = uHigh_;
			this.uLow = uLow_;
			this.uClose = uClose_;
			this.uVolume = uVolume_;
			this.exDate = exDate_;
		}
	}





}
