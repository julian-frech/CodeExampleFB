using NUnit.Framework;
using Analysis.Calculators.MovingAverage;
using System.Collections.Generic;
using Analysis.UnitTests;
using Analysis.Core.Stocks;
using System;
using Analysis.Core.Time;

namespace Analysis.UnitTests.CalculatorTest
{
    /// <summary>
    /// Test für den Standard-Kalkulator des gleitenden Durchsschnitts
    /// </summary>
    public class CalculatorStandardMovingAverageTest
    {
        ICalculatorMovingAverage calculator;
        Stock testStock;

       [SetUp]
        public void Setup()
        {
            testStock = new Stock("testStock");
            for(int i = 1; i <= 100; i++)
            {
                DateTime start = new DateTime(2020,1,1);
                StockTradingValue value = new StockTradingValue("testStock", start.AddDays(i),true,0,0,i,0,0);
                testStock.Update(value);
            }
        }

        [Test]
        public void TestCalculateMit90Datenpunkten()
        {
            calculator = new CalculatorStandardMovingAverage(90, Calculators.Dependency.DayLow);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(testStock);
            Assert.IsTrue(result.Count == 11);
            for(int i = 0; i < 11; i++)
            {
                Assert.AreEqual(45.5m + i, result[i].Value);
            }
        }

        [Test]
        public void TestCalculateMit100Datenpunkten()
        {
            calculator = new CalculatorStandardMovingAverage(100, Calculators.Dependency.DayLow);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(testStock);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(50.5m, result[0].Value);
        }

        [Test]
        public void TestCalculateMit200Datenpunkten()
        {
            calculator = new CalculatorStandardMovingAverage(200, Calculators.Dependency.DayLow);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(testStock);
            Assert.IsTrue(result.Count == 0);
        }
    }
}