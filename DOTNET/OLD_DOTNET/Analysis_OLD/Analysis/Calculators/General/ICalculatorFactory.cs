using Analysis.Calculators.MovingAverage;
using Analysis.Calculators.RelativeStrengthIndex;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Calculators.General
{
    /// <summary>
    /// Factory für jegliche Art von Kalkulatoren, die auf Aktienverläufen operieren
    /// </summary>
    public interface ICalculatorFactory
    {
        /// <summary>
        /// Erzeugt einen Kalkulator zur Berechnung des gleitenden Durchschnitts
        /// </summary>
        public ICalculatorMovingAverage Create_CalculatorMovingAverage(Dependency relevantData);

        /// <summary>
        /// Erzeugt einen Kalkulator zur Berechnung des relativen Stärkeindex
        /// </summary>
        public ICalculatorRelativeStrengthIndex Create_CalculatorRelativeStrengthIndex(Dependency relevantData);
    }
}
