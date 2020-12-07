using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Data.DataFetcher
{
    /// <summary>
    /// Interface zum Laden von Aktiendaten.
    /// </summary>
    public interface IDataFetcher
    {
        /// <summary>
        /// Antwortet alle verfügbaren Aktienverlaufsdaten auf minütlicher oder täglicher Basis ab dem angegeben Zeitstempel.
        /// </summary>
        public List<StockTradingValue> LoadData(string symbol, DateTime from, TimeInterval interval);

        /// <summary>
        /// Antwortet alle verfügbaren Aktienverlaufsdaten auf minütlicher oder täglicher Basis innerhalb des angegebenen Zeitraums.
        /// </summary>
        public List<StockTradingValue> LoadData(string symbol, DateTime from, DateTime to, TimeInterval interval);
    }
}
