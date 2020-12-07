using System;
using System.Collections.Generic;

namespace BoersenDatenService2.ApiDataClasses
{
    public class AlphaVantageData
    {

        public string Symbol { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }

        public decimal High { get; set; }
        public decimal Low { get; set; }

        public decimal Close { get; set; }
        public decimal adjusted_close { get; set; }
        public decimal Volume { get; set; }
        
        public decimal dividend_amount { get; set; }
        public decimal split_coefficient { get; set; }
    }

}
