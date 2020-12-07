
using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analysis.Calculators.MovingAverage
{
    public class CalculatorExponentialMovingAverage : ICalculatorMovingAverage
    {
        //Anzahl der Datenpunkte, die für einen Mittelwert betrachtet werden
        private readonly int range;

        //Glättungsfaktor 
        private readonly decimal factor;

        //Die Werte, auf denen ein Mittelwert bestimmt werden soll
        private readonly Dependency dependency;

        public CalculatorExponentialMovingAverage(int range, decimal factor, Dependency dependency)
        {
            if (range < 2)
            {
                throw new ArgumentException("Calculating an average requires at least two data points.");
            }
            if(factor >= 1 || factor <= 0)
            {
                throw new ArgumentException("Smoothing factor hast to be between 0 and 1.");
            }
            this.range = range;
            this.dependency = dependency;
            this.factor = factor;
        }

        public List<ValueTimeAware<decimal>> Calculate(Stock stock)
        {
            List<ValueTimeAware<decimal>> result = new List<ValueTimeAware<decimal>>();

            //Erhalte alle relevanten Daten mit ihrer Zeitabhängigkeit
            List<ValueTimeAware<decimal>> tradingValues = dependency.GetRelevantData(stock);
            if (tradingValues.Count >= range)
            {
                for (int i = 0; i + range - 1 < tradingValues.Count; i++)
                {
                    decimal average = 0.0m;
                    for (int j = 0; j < range; j++)
                    {
                        average = factor * (tradingValues[i+j].Value - average) + average;
                    }
                    DateTime timestamp = tradingValues[i + range - 1].Timestamp;
                    result.Add(new ValueTimeAware<decimal>(average, timestamp));
                }
            }
            return result;
        }

        public List<Dependency> GetDependencies()
        {
            return new List<Dependency>() { dependency };
        }
    }
}
