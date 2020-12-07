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
    /// Test für den Kalkulator des exponential gleitenden Durchsschnitts
    /// </summary>
    public class CalculatorExponentialMovingAverageTest
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
        public void TestCalculateMit5Datenpunkten()
        {
            decimal a = 0.5m;
            calculator = new CalculatorExponentialMovingAverage(5, a, Calculators.Dependency.DayLow);
            List<ValueTimeAware<decimal>> result = calculator.Calculate(testStock);
            Assert.IsTrue(result.Count == 96);
            for(int i = 0; i < result.Count; i++)
            {
                decimal calc = a*(a*(a*(a*(a*(i + 1) + (i + 2)) + (i + 3)) + (i + 4))+ (i + 5));
                Assert.AreEqual(calc, result[i].Value);
            }
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