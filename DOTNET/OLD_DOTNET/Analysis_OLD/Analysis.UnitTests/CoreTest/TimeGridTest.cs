using Analysis.Core.Time;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.UnitTests.CoreTest
{
    /// <summary>
    /// Unit-Test für ein Zeitgitter
    /// </summary>
    class TimeGridTest
    {
        [Test]
        //Teste das Hinzufügen in falscher Reihenfolge
        public void TestWrongOrder()
        {
            DateTime first = new DateTime(2020,1,2);
            DateTime last = new DateTime(2020, 1, 1);
            TimeInterval interval = TimeInterval.DAILY;
            Assert.Throws<ArgumentException>(() => new TimeGrid(first, last, interval));
        }

        [Test]
        //Teste das Hinzufügen falscher Daten
        public void TestWrongInterval()
        {
            DateTime first = new DateTime(2020, 1, 1, 10, 10, 10);
            DateTime last = new DateTime(2020, 1, 1,10,15,11);
            TimeInterval interval = TimeInterval.MINUTELY;
            Assert.Throws<ArgumentException>(() => new TimeGrid(first, last, interval));

            first = new DateTime(2020, 1, 1, 10, 10, 10);
            last = new DateTime(2020, 1, 1, 10, 11, 9);
            Assert.Throws<ArgumentException>(() => new TimeGrid(first, last, interval));
        }

        [Test]
        //Erfolgreiche Erzeugung
        public void TestCorrectInput()
        {
            DateTime first = new DateTime(2020, 1, 1, 10, 10, 10);
            DateTime last = new DateTime(2020, 1, 1, 10, 15, 10);
            TimeInterval interval = TimeInterval.MINUTELY;
            TimeGrid grid = new TimeGrid(first, last, interval);
            Assert.IsTrue(grid.Times.Count == 6);

            first = new DateTime(2020, 1, 1);
            last = new DateTime(2020, 1, 31);
            interval = TimeInterval.DAILY;
            grid = new TimeGrid(first, last, interval);
            Assert.IsTrue(grid.Times.Count == 31);
            Assert.IsTrue(grid.Times[grid.Times.Count-1].Equals(last));
        }
    }
}
