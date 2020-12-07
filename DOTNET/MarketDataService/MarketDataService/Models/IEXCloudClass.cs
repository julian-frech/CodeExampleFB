using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketDataService.ApiService;
using Newtonsoft.Json;

namespace MarketDataService.Models
{

    public class ModelClass
    {
        public IModel Model { get; set; }
    }
    [Table("IEXCloudClass", Schema = "import")]
    public class IEXCloudClass {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniqueTableId { get; set; }
        public string method { get; set; }
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [Key, Column(Order = 1)]
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("minute")]
        public string minute { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("Id")]
        [Key, Column(Order = 1)]
        public string iexId { get; set; }
        [JsonProperty("region")]
        public string region { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("isEnabled")]
        public string isEnabled { get; set; }
        [JsonProperty("figi")]
        public string figi { get; set; }
        [JsonProperty("cik")]
        public string cik { get; set; }
        [JsonProperty("companyName")]
        public string companyName { get; set; }
        [JsonProperty("exchange")]
        public string exchange { get; set; }
        [JsonProperty("industry")]
        public string industry { get; set; }
        [JsonProperty("website")]
        public string website { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("CEO")]
        public string CEO { get; set; }
        [JsonProperty("securityName")]
        public string securityName { get; set; }
        [JsonProperty("issueType")]
        public string issueType { get; set; }
        [JsonProperty("sector")]
        public string sector { get; set; }
        [JsonProperty("primarySicCode")]
        public int? primarySicCode { get; set; }
        [JsonProperty("employees")]
        public int? employees { get; set; }
        [JsonProperty("tags")]
        [NotMapped]
        public List<string> tags { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("address2")]
        [NotMapped]
        public string address2 { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }

        public double? open { get; set; }
        public double? high { get; set; }
        public double? low { get; set; }
        public double? close { get; set; }
        public int? volume { get; set; }
        public double? uOpen { get; set; }
        public double? uHigh { get; set; }
        public double? uLow { get; set; }
        public double? uClose { get; set; }
        public int? uVolume { get; set; }
        public double? change { get; set; }
        public double? changePercent { get; set; }
        public string? label { get; set; }
        public double? changeOverTime { get; set; }

        [JsonProperty("exDate")]
        public string exDate { get; set; }
        [JsonProperty("paymentDate")]
        public string paymentDate { get; set; }
        [JsonProperty("recordDate")]
        public string recordDate { get; set; }
        [JsonProperty("declaredDate")]
        public string declaredDate { get; set; }
        [JsonProperty("amount")]
        public string amount { get; set; }
        [JsonProperty("flag")]
        public string flag { get; set; }
        [JsonProperty("frequency")]
        public string frequency { get; set; }

        [JsonProperty("datetime")]
        public long? datetime { get; set; }
        [JsonProperty("headline")]
        public string headline { get; set; }
        [JsonProperty("source")]
        public string source { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("summary")]
        public string summary { get; set; }
        [JsonProperty("related")]
        public string related { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("lang")]
        public string lang { get; set; }
        [JsonProperty("hasPaywall")]
        [NotMapped]
        public bool hasPaywall { get; set; }

        
        [JsonProperty("primaryExchange")]
        public string primaryExchange { get; set; }
        [JsonProperty("calculationPrice")]
        public string calculationPrice { get; set; }
        [JsonProperty("openTime")]
        public long openTime { get; set; }
        [JsonProperty("openSource")]
        public string openSource { get; set; }
        [JsonProperty("closeTime")]
        public long closeTime { get; set; }
        [JsonProperty("closeSource")]
        public string closeSource { get; set; }
        [JsonProperty("highTime")]
        public long highTime { get; set; }
        [JsonProperty("highSource")]
        public string highSource { get; set; }
        [JsonProperty("lowTime")]
        public long lowTime { get; set; }
        [JsonProperty("lowSource")]
        public string lowSource { get; set; }
        [JsonProperty("latestPrice")]
        public double latestPrice { get; set; }
        [JsonProperty("latestSource")]
        public string latestSource { get; set; }
        [JsonProperty("latestTime")]
        public string latestTime { get; set; }
        [JsonProperty("latestUpdate")]
        public long latestUpdate { get; set; }
        [JsonProperty("latestVolume")]
        public int latestVolume { get; set; }
        [JsonProperty("iexRealtimePrice")]
        public string iexRealtimePrice { get; set; }
        [JsonProperty("iexRealtimeSize")]
        public string iexRealtimeSize { get; set; }
        [JsonProperty("iexLastUpdated")]
        public string iexLastUpdated { get; set; }
        [JsonProperty("delayedPrice")]
        public double delayedPrice { get; set; }
        [JsonProperty("delayedPriceTime")]
        public long delayedPriceTime { get; set; }
        [JsonProperty("oddLotDelayedPrice")]
        public double oddLotDelayedPrice { get; set; }
        [JsonProperty("oddLotDelayedPriceTime")]
        public long oddLotDelayedPriceTime { get; set; }
        [JsonProperty("extendedPrice")]
        public double extendedPrice { get; set; }
        [JsonProperty("extendedChange")]
        public double extendedChange { get; set; }
        [JsonProperty("extendedChangePercent")]
        public double extendedChangePercent { get; set; }
        [JsonProperty("extendedPriceTime")]
        public long extendedPriceTime { get; set; }
        [JsonProperty("previousClose")]
        public double previousClose { get; set; }
        [JsonProperty("previousVolume")]
        public int previousVolume { get; set; }
        [JsonProperty("iexMarketPercent")]
        public string iexMarketPercent { get; set; }
        [JsonProperty("iexVolume")]
        public string iexVolume { get; set; }
        [JsonProperty("avgTotalVolume")]
        public int avgTotalVolume { get; set; }
        [JsonProperty("iexBidPrice")]
        public string iexBidPrice { get; set; }
        [JsonProperty("iexBidSize")]
        public string iexBidSize { get; set; }
        [JsonProperty("iexAskPrice")]
        public string iexAskPrice { get; set; }
        [JsonProperty("iexAskSize")]
        public string iexAskSize { get; set; }
        [JsonProperty("iexOpen")]
        public string iexOpen { get; set; }
        [JsonProperty("iexOpenTime")]
        public string iexOpenTime { get; set; }
        [JsonProperty("iexClose")]
        public double iexClose { get; set; }
        [JsonProperty("iexCloseTime")]
        public long iexCloseTime { get; set; }
        [JsonProperty("marketCap")]
        public long marketCap { get; set; }
        [JsonProperty("peRatio")]
        public double peRatio { get; set; }
        [JsonProperty("week52High")]
        public double week52High { get; set; }
        [JsonProperty("week52Low")]
        public int week52Low { get; set; }
        [JsonProperty("ytdChange")]
        public double ytdChange { get; set; }
        [JsonProperty("lastTradeTime")]
        public long lastTradeTime { get; set; }
        [JsonProperty("isUSMarketOpen")]
        public string isUSMarketOpen { get; set; }



        public string sourceAPI { get; set; }

        public double adjClose { get; set; }
        public double unadjustedVolume { get; set; }
        public double vwap { get; set; }


    }

    public class IEXCloudBaseClass
    {
        public string Method { get; set; }
        public string Symbol { get; set; }
        public string Span { get; set; }
        public string Chart { get; set; }
        public string Interval { get; set; }
        public string Date { get; set; }
        public string Last { get; set; }
        public string Field { get; set; }
        public string sourceAPI { get; set; }

    }

    [Table("IEXCloudCompany", Schema = "import")]
    public class IEXCloudClassCompany : IModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [Key, Column(Order = 1)]
        [JsonProperty("companyName")]
        public string companyName { get; set; }
        [JsonProperty("exchange")]
        public string exchange { get; set; }
        [JsonProperty("industry")]
        public string industry { get; set; }
        [JsonProperty("website")]
        public string website { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("CEO")]
        public string CEO { get; set; }
        [JsonProperty("securityName")]
        public string securityName { get; set; }
        [JsonProperty("issueType")]
        public string issueType { get; set; }
        [JsonProperty("sector")]
        public string sector { get; set; }
        [JsonProperty("primarySicCode")]
        public int primarySicCode { get; set; }
        [JsonProperty("employees")]
        public string employees { get; set; }
        [JsonProperty("tags")]
        [NotMapped]
        public List<string> tags { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("address2")]
        [NotMapped]
        public string address2 { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }


        
    }

    [Table("IEXCloudSymbolList", Schema = "import")]
    public class IEXCloudClassSymbolList
    {
        [Key, Column(Order = 0)]
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("iexId")]
        [Key, Column(Order = 1)]
        public string iexId { get; set; }
        [JsonProperty("region")]
        public string region { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("isEnabled")]
        public string isEnabled { get; set; }
        [JsonProperty("figi")]
        public string figi { get; set; }
        [JsonProperty("cik")]
        public string cik { get; set; }

    }

    public class ArrayIEXCloudClassSymbolList : IModel
    {
        public IList<IEXCloudClassSymbolList> IEXCloudClassSymbolList { get; set; }

    }

    [Table("IEXCloudHistoric", Schema = "import")]
    public class IEXCloudClassHistoric
    {
        [Key, Column(Order = 0)]
        public string symbol { get; set; }
        [Key, Column(Order = 1)]
        public string date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public int volume { get; set; }
        public double uOpen { get; set; }
        public double uHigh { get; set; }
        public double uLow { get; set; }
        public double uClose { get; set; }
        public int uVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public string label { get; set; }
        public double changeOverTime { get; set; }

    }

    public class IEXCloudClassHistoricList : IModel
    {
        public string range { get; set; }
        public List<IEXCloudClassHistoric> data { get; set; }

    }

}
