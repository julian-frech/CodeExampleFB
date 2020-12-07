using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Stocks
{
    /// <summary>
    /// Allgemeine Klasse für das Momentum eines Aktienverlaufes
    /// </summary>
    public class StockTradingValue
    {
        //Aktiensymbol
        public string Symbol { get; }

        //Zeitstempel
        public DateTime Timestamp { get; }

        //Kurs zu Beginn der Periode
        public decimal Open { get; }

        //Höchster Kurs innerhalb der Periode
        public decimal High { get; }

        //Niedrigster Kurs innerhalb der Periode
        public decimal Low { get; }

        //Kurs zum Ende der Periode
        public decimal Close { get; }

        //Anzahl der Transaktionen während der Periode
        public long Volume { get; }

        //Flag für Handelstage
        public bool IsTradingDay { get; }

        /// <summary>
        /// Erzeugt einen Verlaufswert mit den übergebenen Größen. IsTradingDay spezifiziert, ob es sich um einen ganzen Handelstag handelt.
        /// </summary>
        public StockTradingValue(String symbol, DateTime timestamp, bool isTradingDay, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            this.Symbol = symbol;
            this.Timestamp = timestamp;
            this.IsTradingDay = isTradingDay;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
        }

        public override bool Equals(Object obj)
        {
            if (obj is StockTradingValue other)
            {
                return
                this.Symbol.Equals(other.Symbol) &&
                this.Timestamp.Equals(other.Timestamp) &&
                this.IsTradingDay == other.IsTradingDay &&
                this.Open == other.Open &&
                this.High == other.High &&
                this.Low == other.Low &&
                this.Close == other.Close &&
                this.Volume == other.Volume;
            }
            return false;                
        }
    }
}
