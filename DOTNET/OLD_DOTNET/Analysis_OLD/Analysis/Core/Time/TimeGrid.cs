using Analysis.Core.Stocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Time
{
    /// <summary>
    /// Zeitgitter auf Basis eines Zeitintervalls und der Vorgabe von zwei unterschiedlichen Zeitpunkten des Gitters.
    /// </summary>
    public class TimeGrid
    {
        public List<DateTime> Times { get; set; }

        /// <summary>
        /// Erzeugt ein Zeitgitter aus einem Start- und einem Endpunkt, sowie einem Zeitschritt.
        /// </summary>
        public TimeGrid(DateTime firstTime, DateTime lastTime, TimeInterval interval)
        {
            if(lastTime.CompareTo(firstTime) < 0)
            {
                throw new ArgumentException("Second DateTime has to be earlier than the first one.");
            }
            Times = new List<DateTime>();
            TimeSpan intervalLength = interval.GetTimeSpan();
            double jumps = (lastTime - firstTime) / intervalLength;
            int maxJumps = (int)jumps;
            if(maxJumps != jumps)
            {
                throw new ArgumentException("Dates do not fit in the timegrid with given jumping length.");
            }
            for (int i = 0; i <= maxJumps; i++)
            {
                DateTime nextTime = firstTime + i*intervalLength;
                Times.Add(nextTime);
            }
        }
    }
}
