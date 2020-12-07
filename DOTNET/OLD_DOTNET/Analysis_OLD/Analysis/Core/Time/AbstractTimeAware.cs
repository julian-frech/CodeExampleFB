using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Time
{
    /// <summary>
    /// Subklassen haben einen eindeutigen Zeitstempel
    /// </summary>
    public class AbstractTimeAware
    {
        public DateTime Timestamp { get; set; }

        public AbstractTimeAware(DateTime timestamp)
        {
            this.Timestamp = timestamp;
        }
    }
}
