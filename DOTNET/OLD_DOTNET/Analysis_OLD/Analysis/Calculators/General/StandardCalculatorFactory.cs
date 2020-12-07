using System;
using System.Collections.Generic;
using System.Text;
using Analysis.Calculators.General;
using Analysis.Calculators.MovingAverage;
using Analysis.Calculators.RelativeStrengthIndex;
using Analysis.Core;

namespace Analysis.Calculators
{
    /// <summary>
    /// Factory für Kalkulatoren des Analyse-Projekts
    /// </summary>
    public class StandardCalculatorFactory : ICalculatorFactory
    {
        public ICalculatorMovingAverage Create_CalculatorMovingAverage(Dependency relevantData)
        {
            return new CalculatorStandardMovingAverage(7, relevantData);
        }

        public ICalculatorRelativeStrengthIndex Create_CalculatorRelativeStrengthIndex(Dependency relevantData)
        {
            return new CalculatorStandardRelativeStrengthIndex(14);
        }
    }
}
