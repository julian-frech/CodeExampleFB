
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Analysis.Core.Stocks;
using Analysis.Core.Exceptions;

namespace Analysis.UnitTests.CoreTest
{
    /// <summary>
    /// Test zum Erzeugen und Verwalten der Aktienverläufe durch die Klasse Stock.
    /// </summary>
    class StockTest
    {
        readonly string symbol = "testSymbol";
        Stock stock;

        [SetUp]
        public void Setup()
        {
            stock = new Stock(symbol);
        }

        [Test]
        //Teste das Hinzufügen falscher Daten
        public void TestWrongSymbol()
        {
            StockTradingValue wrongValue = new StockTradingValue("wrongSymbol",new DateTime(),true,0,0,0,0,0);
            Assert.Throws<IllegalSymbolException>( () => stock.Update(wrongValue));
        }

        [Test]
        //Teste das Hinzufügen doppelter Werte
        public void TestConcurrentValues()
        {
            StockTradingValue valueOne = new StockTradingValue("testSymbol", new DateTime(), false, 0, 0, 0, 0, 0);
            StockTradingValue valueTwo = new StockTradingValue("testSymbol", new DateTime(), false, 0, 0, 0, 0, 0);
            stock.Update(valueOne);
            stock.Update(valueTwo);
            Assert.IsTrue(stock.TradingValues.Count == 1 && stock.TradingValues.First.Value.Equals(valueOne));

            StockTradingValue valueThree = new StockTradingValue("testSymbol", new DateTime(), true, 0, 0, 0, 0, 0);
            stock.Update(valueThree);
            Assert.IsTrue(stock.TradingDays.Count == 1);

            StockTradingValue valueFour = new StockTradingValue("testSymbol", new DateTime(), false, 1, 0, 0, 0, 0);
            Assert.Throws<ArgumentException>(() => stock.Update(valueFour));
        }

        [Test]
        //Teste das Hinzufügen von korrekten Daten
        public void TestUpdate()
        {
            for(int i = 0; i<10; i++)
            {
                StockTradingValue value = new StockTradingValue("testSymbol", new DateTime().AddDays(i), true, 0, i, 0, i, 0);
                stock.Update(value);
            }
            Assert.IsTrue(stock.TradingDays.Count == 10);

            for (int i = 5; i < 15; i++)
            {
                StockTradingValue value = new StockTradingValue("testSymbol", new DateTime().AddDays(i), true, 0, i, 0, i, 0);
                stock.Update(value);
            }
            Assert.IsTrue(stock.TradingDays.Count == 15);

            for (int i = 10; i < 20; i++)
            {
                StockTradingValue value = new StockTradingValue("testSymbol", new DateTime().AddDays(i), false, i, i, i, i, i);
                stock.Update(value);
            }
            Assert.IsTrue(stock.TradingDays.Count == 15 && stock.TradingValues.Count == 10);
        }
    }
}
