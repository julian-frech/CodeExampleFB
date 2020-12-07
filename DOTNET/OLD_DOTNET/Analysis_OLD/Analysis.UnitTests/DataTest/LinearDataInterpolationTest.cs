using Analysis.Core.Stocks;
using Analysis.Data.DataInterpolation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.UnitTests.DataTest
{
    /// <summary>
    /// Test zur linearen Interpolation
    /// </summary>
    class LinearDataInterpolationTest
    {
        IDataInterpolation interpolation;

        [SetUp]
        public void Setup()
        {
            interpolation = new LinearDataInterpolation();
        }

        [Test]
        public void TestWrongData()
        {
            DateTime firstTime = new DateTime(2020, 1, 1);
            DateTime lastTime = new DateTime(2020, 1, 31);
            StockTradingValue first = new StockTradingValue("test", firstTime, true, 0, 0, 0, 0, 0);
            StockTradingValue last = new StockTradingValue("test", lastTime, false, 31, 31, 31, 31, 62);
            Assert.Throws<ArgumentException>(() => interpolation.Interpolate(first, last));
        }

        [Test]
        public void TestWrongOrdner()
        {
            DateTime firstTime = new DateTime(2020, 1, 1);
            DateTime lastTime = new DateTime(2020, 1, 31);
            StockTradingValue first = new StockTradingValue("test", firstTime, true, 0, 0, 0, 0, 0);
            StockTradingValue last = new StockTradingValue("test", lastTime, true, 31, 31, 31, 31, 62);
            Assert.Throws<ArgumentException>(() => interpolation.Interpolate(last, first));
        }

        [Test]
        public void TestInterpolation()
        {
            DateTime firstTime = new DateTime(2020, 1, 1);
            DateTime lastTime = new DateTime(2020, 1, 31);
            StockTradingValue first = new StockTradingValue("test", firstTime ,true, 1,1,1,1,0);
            StockTradingValue last = new StockTradingValue("test", lastTime, true, 32, 32, 32, 32, 62);
            List<StockTradingValue> result = interpolation.Interpolate(first, last);
            Assert.AreEqual(29, result.Count);
            for(int i = 0; i< result.Count; i++)
            {
                StockTradingValue value = result[i];
                Assert.IsTrue(value.Timestamp.Equals(firstTime.AddDays(i+1)));
                Assert.AreEqual(first.Close + (i + 1),value.Close);
                Assert.AreEqual(first.Volume + (2 * i + 2),value.Volume);
            }

            firstTime = new DateTime(2020, 1, 1);
            lastTime = new DateTime(2020, 1, 1);
            first = new StockTradingValue("test", firstTime, true, 1, 1, 1, 1, 0);
            last = new StockTradingValue("test", lastTime, true, 32, 32, 32, 32, 62);
            result = interpolation.Interpolate(first, last);
            Assert.IsEmpty(result);
        }
    }
}
