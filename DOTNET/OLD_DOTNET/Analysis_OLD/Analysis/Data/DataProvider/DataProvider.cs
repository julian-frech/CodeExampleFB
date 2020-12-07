using Analysis.Core.Stocks;
using Analysis.Data.DataFetcher;
using System.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using Analysis.Calculators;
using Analysis.Core.Time;
using Analysis.Data.DataInterpolation;

namespace Analysis.Data.DataProvider
{
    /// <summary>
    /// DataProvider für Aktienverläufe und Berechnungsergebnisse. 
    /// In dieser Implementierung werden alle Daten für einen festgelegten Zeitraum im Cache gehalten.
    /// </summary>
    public class DataProvider : IDataProvider
    {
        //Fetcher für fehlende Datensätze
        private readonly IDataFetcher fetcher;

        //Interpolation für Datenlücken
        private readonly IDataInterpolation interpolation;

        //Cache für Aktienverläufe
        private readonly ObjectCache stockCache;

        public DataProvider(IDataFetcher fetcher, IDataInterpolation interpolation)
        {
            this.fetcher = fetcher;
            this.stockCache = MemoryCache.Default;
            this.interpolation = interpolation;
        }

        protected Stock CreateOrGetStock(string symbol)
        {
            Stock cachedStock = (Stock)stockCache[symbol];
            //Falls noch nicht vorhanden, lege einen Aktienverlauf zum Symbol an und cache diesen.
            if (cachedStock == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    SlidingExpiration = TimeSpan.FromMinutes(6)
                };
                cachedStock = new Stock(symbol);
                stockCache.Set(symbol, cachedStock, policy);
            }
            return cachedStock;
        }

        public Stock GetUpdatedStock(string symbol, TimeInterval targetData, DateTime targetUpdateTime)
        {
            Stock cachedStock = CreateOrGetStock(symbol);
            //Bestimmt den letzten Update-Zeitpunkt
            switch (targetData)
            {
                case TimeInterval.DAILY:
                    if (cachedStock.TradingDays.Count == 0)
                    {
                        DateTime beginOfYear = new DateTime(targetUpdateTime.Year,1,1);
                        List<StockTradingValue> fetchedData = fetcher.LoadData(symbol, beginOfYear, targetUpdateTime, targetData);
                        UpdateStock(cachedStock, fetchedData);
                    }
                    else
                    {
                        DateTime latestUpdateTime = cachedStock.TradingDays.Last.Value.Timestamp;
                        if (latestUpdateTime.CompareTo(targetUpdateTime) < 0)
                        {
                            List<StockTradingValue> fetchedData = fetcher.LoadData(symbol, latestUpdateTime, targetUpdateTime, targetData);
                            UpdateStock(cachedStock, fetchedData);
                        }
                    }
                    break;
                case TimeInterval.MINUTELY:
                    if (cachedStock.TradingValues.Count == 0)
                    {
                        DateTime beginOfDay = new DateTime(targetUpdateTime.Year, targetUpdateTime.Month, targetUpdateTime.Day,0,0,0);
                        List<StockTradingValue> fetchedData = fetcher.LoadData(symbol, beginOfDay, targetUpdateTime, targetData);
                        UpdateStock(cachedStock, fetchedData);
                    }
                    DateTime latestUpdate = cachedStock.TradingValues.Last.Value.Timestamp;
                    if (latestUpdate.Date.CompareTo(targetUpdateTime.Date) < 0)
                    {
                        List<StockTradingValue> fetchedData = fetcher.LoadData(symbol, latestUpdate, targetUpdateTime, targetData);
                        UpdateStock(cachedStock, fetchedData);
                    }
                    break;
                default:
                    throw new Exception(String.Format("Illegal time interval for stock data {0}.", targetData));
            }
            return cachedStock;
        }

        /// <summary>
        /// Update für einen Aktienverlauf
        /// </summary>
        protected void UpdateStock(Stock stock, List<StockTradingValue> fetchedData)
        {
            //Interpoliere falls notwendig
            List<StockTradingValue> interpolatedData = new List<StockTradingValue>();
            for (int i = 0; i < fetchedData.Count; i++)
            {
                StockTradingValue currentValue = fetchedData[i];
                interpolatedData.Add(currentValue);
                if (i < fetchedData.Count - 1)
                {
                    StockTradingValue nextValue = fetchedData[i + 1];
                    List<StockTradingValue> missingData = interpolation.Interpolate(currentValue, nextValue);
                    interpolatedData.AddRange(missingData);
                }
            }
            //Überträgt die Daten an den Aktienverlauf
            stock.Update(interpolatedData);
        }

    }
}
