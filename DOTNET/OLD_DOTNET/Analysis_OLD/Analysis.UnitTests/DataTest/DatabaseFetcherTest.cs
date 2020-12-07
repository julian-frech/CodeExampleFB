using Analysis.Core.Stocks;
using Analysis.Core.Time;
using Analysis.Data.DataFetcher;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.UnitTests.DataTest
{
    /// <summary>
    /// Test für den DatabaseFetcher. Da dieser Test von der tatsächlichen Datenbank abhängt, kann sich eine Änderung 
    /// an der Datengrundlage auch auf das Testergebnis auswirken.
    /// </summary>
    class DatabaseFetcherTest
    {
        IDataFetcher fetcher;

        [SetUp]
        public void Setup()
        {
            fetcher = new DatabaseFetcher();
        }

        [Test]
        public void TestLoadFromTimestamp()
        {
            string symbol = "pypl";
            DateTime from = new DateTime(2020,2,10);
            List<StockTradingValue> ergebnis = fetcher.LoadData(symbol, from, TimeInterval.DAILY);
            Assert.IsNotEmpty(ergebnis);
        }

        [Test]
        public void TestLoadToTimestamp()
        {
            string symbol = "pypl";
            DateTime from = new DateTime(2020, 1, 1);
            DateTime to = new DateTime(2020, 2, 14);
            List<StockTradingValue> ergebnis = fetcher.LoadData(symbol, from, to, TimeInterval.DAILY);
            Assert.IsNotEmpty(ergebnis);
        }
    }
}
