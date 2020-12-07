using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Data.DataInterpolation
{
    /// <summary>
    /// Interpoliert fehlende Verlaufsdaten.
    /// </summary>
    public interface IDataInterpolation
    {
        /// <summary>
        /// Antwortet eine Liste der interpolierten Verlaufsdaten zwischen den übergebenen Terminen.
        /// </summary>
        public List<StockTradingValue> Interpolate(StockTradingValue first, StockTradingValue last);
    }
}
