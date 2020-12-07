using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace BoersenDatenService2.ApiDataClasses
{

    public class Historical
    {
        public string date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double adjClose { get; set; }
        public double volume { get; set; }
        public double unadjustedVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public double vwap { get; set; }
        public string label { get; set; }
        public double changeOverTime { get; set; }
    }

    public class FMPStockPrices
    {
        public string Symbol { get; set; }
        public IList<Historical> Historical { get; set; }
    }

}
