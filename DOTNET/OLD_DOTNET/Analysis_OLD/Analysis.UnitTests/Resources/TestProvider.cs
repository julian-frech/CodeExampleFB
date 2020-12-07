using Analysis.Core.Stocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Analysis.UnitTests
{
    /// <summary>
    /// Provider für Testdaten. Liest einmalig alle vorhandenen Daten ein und bietet anschlißend eine Map mit Aktienverläufen
    /// </summary>
    public class TestProvider
    {
        private static TestProvider instance;
        public Dictionary<string, Stock> Testdata { get; }

        private TestProvider() 
        {
            Testdata = readTestData();
        }

        /// <summary>
        /// Erhalte die einzige Instanz dieser Klasse
        /// </summary>
        public static TestProvider getInstance()
        {
            if(instance == null)
            {
                instance = new TestProvider();
            }
            return instance;
        }

        /// <summary>
        /// Läd die Testdaten im Verzeichnis
        /// </summary>
        private Dictionary<string, Stock> readTestData()
        {
            Dictionary<string, Stock> data = new Dictionary<string, Stock>();

            string[] dailyData = Properties.Resources.DailyTestData.Split("\n");

            string symbol = "DailyTestData";
            Stock stock = new Stock(symbol);
            bool tradingDay = true;

            for (int i = 1; i < dailyData.Length; i++) {

                var values = dailyData[i].Trim().Split(',');
                if (!values[0].Equals(""))
                {

                    DateTime timestamp = Convert.ToDateTime(values[0]);
                    decimal open = Convert.ToDecimal(values[1]);
                    decimal high = Convert.ToDecimal(values[2]);
                    decimal low = Convert.ToDecimal(values[3]);
                    decimal close = Convert.ToDecimal(values[4]);
                    long volume = Convert.ToInt32(values[5]);

                    StockTradingValue stockTradingValue =
                       new StockTradingValue(symbol, timestamp, tradingDay, open, high, low, close, volume);
                    stock.Update(stockTradingValue);
                }
            }
            data.Add(symbol, stock);
            return data;
        }
    }
}
