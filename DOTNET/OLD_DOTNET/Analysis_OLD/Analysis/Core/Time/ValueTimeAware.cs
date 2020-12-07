using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Time
{
    /// <summary>
    /// Klasse zur Bereitstellung eines Wertes mit zugehörigem Zeitstempel
    /// </summary>
    public class ValueTimeAware<T> : AbstractTimeAware
    {
        public T Value { get; }

        /// <summary>
        /// Ein Wert, der zu gegebenem Zeitstempel, gültig ist.
        /// </summary>
        public ValueTimeAware(T value, DateTime timestamp) : base(timestamp)
        {
            this.Value = value;
        }
    }
}
