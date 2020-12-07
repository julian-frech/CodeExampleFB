using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Data.DataProvider
{
    /// <summary>
    /// Interface für DataProvider. Bietet Methoden zum Verwalten von Aktienverläufen und Berechungsergebnissen.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Antwortet den Aktienverlauf zum gegebenen Symbol und updatet diesen 
        /// entsprechend der angeforterten Daten bis zum gewünschten Zeitpunkt. 
        /// </summary>
        public Stock GetUpdatedStock(string symbol, TimeInterval targetData, DateTime targetUpdateTime);
    }
}
