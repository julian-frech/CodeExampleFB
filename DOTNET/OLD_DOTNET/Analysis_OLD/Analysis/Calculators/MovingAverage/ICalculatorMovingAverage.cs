using Analysis.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Calculators.MovingAverage
{
    /// <summary>
    /// Implementierungen dieses Kalkulators berechnen Varianten des gleitenden Mittwelwerts auf Aktienverläufen.
    /// </summary>
    public interface ICalculatorMovingAverage : ICalculator<decimal>
    {
    }
}
