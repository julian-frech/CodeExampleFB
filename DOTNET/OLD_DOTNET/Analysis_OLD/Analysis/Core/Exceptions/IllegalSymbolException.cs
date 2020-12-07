using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Core.Exceptions
{
    public class IllegalSymbolException : Exception
    {
        /// <summary>
        /// Erzeugt eine Instanz der IllegalSymbolException. Meldet welches Symbol erwartet und welche stattdessen übergeben wurde.
        /// </summary>
        public IllegalSymbolException(string symbolExpected, string symbolActual)
            : base(String.Format("Received wrong symbol {0} when expected was {1}", symbolActual, symbolExpected))
        {
        }
    }
}
