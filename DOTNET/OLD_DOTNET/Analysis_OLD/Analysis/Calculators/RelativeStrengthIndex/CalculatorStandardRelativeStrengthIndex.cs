using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Calculators.RelativeStrengthIndex
{
    /// <summary>
    /// Standard Implementierung eines Kalkulators zur Berechnung des relativen Stärkeindex
    /// </summary>
    public class CalculatorStandardRelativeStrengthIndex : ICalculatorRelativeStrengthIndex
    {
        //Anzahl der Datenpunkte
        private readonly int range;

        /// <summary>
        /// Erzeugt einen Kalkulator für den klassischen RSI. Die "range" bestimmt, die Anzahl beobachteter Closed-Werte. 
        /// Die Anzahl täglichen Kursentwicklungen sind damit range-1. 
        /// </summary>
        /// <param name="range"></param>
        public CalculatorStandardRelativeStrengthIndex(int range)
        {
            if (range < 2)
            {
                throw new Exception("Calculating an RSI requires at least two data points.");
            }
            this.range = range;
        }


        public List<ValueTimeAware<decimal>> Calculate(Stock stock)
        {
            List<ValueTimeAware<decimal>> result = new List<ValueTimeAware<decimal>>();

            //Erhalte alle tagesrelevanten Daten
            List<ValueTimeAware<decimal>> closed = Dependency.DayClose.GetRelevantData(stock);

            if (closed.Count >= range)
            {
                for (int i = 0; i + range - 1 < closed.Count; i++)
                {
                    decimal averageUp = 0.0m;
                    decimal averageDown = 0.0m;
                    for (int j = 1; j < range; j++)
                    {
                        decimal trend = closed[i + j].Value - closed[i + j - 1].Value;
                        if(trend < 0)
                        {
                            averageDown -= trend;
                        }
                        else
                        {
                            averageUp += trend;
                        }
                    }
                    //Dadurch, dass nur die Close-Werte eingehen, 
                    //erhalten wir einen Trend weniger, als die Anzahl an beobachteten Datenpunkten.
                    averageUp /= (range-1);
                    averageDown /= (range-1);

                    decimal rsi;
                    if (averageDown == 0m && averageUp > 0m)
                    {
                        rsi = 100m;
                    }
                    else if(averageDown == 0m && averageUp == 0m)
                    {
                        throw new Exception("RSI can not be calculated based on constant trends.");
                    }
                    else
                    {
                        rsi = 100m - (100m / (1 + (averageUp / averageDown)));
                    }

                    DateTime timestamp = closed[i + range - 1].Timestamp;
                    result.Add(new ValueTimeAware<decimal>(rsi, timestamp));
                }
            }
            return result;
        }

        public List<Dependency> GetDependencies()
        {
            return new List<Dependency>() { Dependency.DayClose};
        }
    }
}
