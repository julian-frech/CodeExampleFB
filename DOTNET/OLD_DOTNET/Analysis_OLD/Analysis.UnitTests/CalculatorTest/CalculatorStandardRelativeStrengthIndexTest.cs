using NUnit.Framework;
using Analysis.Calculators.MovingAverage;
using System.Collections.Generic;
using Analysis.UnitTests;
using Analysis.Core.Stocks;
using System;
using Analysis.Core.Time;
using Analysis.Calculators.RelativeStrengthIndex;

namespace Analysis.UnitTests.CalculatorTest
{
    /// <summary>
    /// Test für den Kalkulator des relativen Stärke Index
    /// </summary>
    public class CalculatorStandardRelativeStrengthIndexTest
    {
        ICalculatorRelativeStrengthIndex calculator;
        Stock increasingStock;
        Stock decreasingStock;
        Stock oscillatingStock;

        [SetUp]
        public void Setup()
        {
            increasingStock = new Stock("increasingStock");
            decreasingStock = new Stock("decreasingStock");
            oscillatingStock = new Stock("oscillatingStock");

            int lastClosed = 0;
            for (int i = 1; i <= 101; i++)
            {
                DateTime start = new DateTime(2020, 1, 1);
                StockTradingValue valueIncreasing = new StockTradingValue("increasingStock", start.AddDays(i), true, 0, 0, 0, i, 0);
                increasingStock.Update(valueIncreasing);

                StockTradingValue valueDecreasing = new StockTradingValue("decreasingStock", start.AddDays(i), true, 0, 0, 0, 101 - i, 0);
                decreasingStock.Update(valueDecreasing);

                StockTradingValue valueOscillating;
                if (i % 2 != 0) {
                    valueOscillating = new StockTradingValue("oscillatingStock", start.AddDays(i), true, 0, 0, 0, lastClosed + 4, 0);
                    lastClosed = lastClosed + 4;
                }
                else
                {
                    valueOscillating = new StockTradingValue("oscillatingStock", start.AddDays(i), true, 0, 0, 0, lastClosed - 1, 0);
                    lastClosed = lastClosed - 1;
                }
                oscillatingStock.Update(valueOscillating);
            }
        }

        [Test]
        public void TestCalculateMit101Datenpunkten()
        {
            calculator = new CalculatorStandardRelativeStrengthIndex(101);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(increasingStock);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(100m, result[0].Value);

            result = calculator.Calculate(decreasingStock);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(0m, result[0].Value);

            result = calculator.Calculate(oscillatingStock);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(80m, result[0].Value);
        }

        [Test]
        public void TestCalculateMit15Datenpunkten()
        {
            calculator = new CalculatorStandardRelativeStrengthIndex(15);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(increasingStock);
            Assert.IsTrue(result.Count == 87);
            for(int i = 0; i< result.Count; i++)
            {
                Assert.AreEqual(100m, result[i].Value);
            }

            result = calculator.Calculate(decreasingStock);
            Assert.IsTrue(result.Count == 87);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(0m, result[i].Value);
            }

            result = calculator.Calculate(oscillatingStock);
            Assert.IsTrue(result.Count == 87);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(80m, result[i].Value);
            }
        }

        [Test]
        public void TestCalculateMit200Datenpunkten()
        {
            calculator = new CalculatorStandardRelativeStrengthIndex(200);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(oscillatingStock);
            Assert.IsTrue(result.Count == 0);
        }
    }
}