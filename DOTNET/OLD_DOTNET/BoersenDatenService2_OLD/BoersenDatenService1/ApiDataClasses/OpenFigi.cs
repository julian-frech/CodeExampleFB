using System.Collections.Generic;
using Newtonsoft.Json;

namespace BoersenDatenService2.ApiDataClasses
{
    public class Data
    {
        [JsonProperty(PropertyName = "figi")]
        public string figi { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "ticker")]
        public string ticker { get; set; }
        [JsonProperty(PropertyName = "exchCode")]
        public string exchCode { get; set; }
        [JsonProperty(PropertyName = "compositeFIGI")]
        public string compositeFIGI { get; set; }
        [JsonProperty(PropertyName = "uniqueID")]
        public string uniqueID { get; set; }
        [JsonProperty(PropertyName = "securityType")]
        public string securityType { get; set; }
        [JsonProperty(PropertyName = "marketSector")]
        public string marketSector { get; set; }
        [JsonProperty(PropertyName = "shareClassFIGI")]
        public string shareClassFIGI { get; set; }
        [JsonProperty(PropertyName = "uniqueIDFutOpt")]
        public string uniqueIDFutOpt { get; set; }
        [JsonProperty(PropertyName = "securityType2")]
        public string securityType2 { get; set; }
        [JsonProperty(PropertyName = "securityDescription")]
        public string securityDescription { get; set; }
    }

    public class ApiFeedbackGenericList
    {
        public IList<Data> data { get; set; }

    }
}
