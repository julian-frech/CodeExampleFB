using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Time
{
    /// <summary>
    /// Enumeration von Zeitintervallen
    /// </summary>
    public enum TimeInterval
    {
        MINUTELY, 
        DAILY
    }

    /// <summary>
    /// Methoden für die Enumeration TimeInterval
    /// </summary>
    static class TimeIntervalMethods { 
        
        /// <summary>
        /// Antwortet die Zeitspanne hinter einem Zeitintervall.
        /// </summary>
        public static TimeSpan GetTimeSpan(this TimeInterval interval)
        {
            return interval switch
            {
                TimeInterval.MINUTELY => TimeSpan.FromMinutes(1),
                TimeInterval.DAILY => TimeSpan.FromDays(1),
                _ => throw new ArgumentException("Could not find the TimeSpan of the given TimeInterval."),
            };
        }

        /// <summary>
        /// Key des Intervalls für die Persistenz.
        /// </summary>
        public static int GetKey(this TimeInterval interval)
        {
            return interval switch
            {
                TimeInterval.MINUTELY => 0,
                TimeInterval.DAILY => 1,
                _ => throw new ArgumentException("Could not find the Key of the given TimeInterval."),
            };
        }

    }
}
