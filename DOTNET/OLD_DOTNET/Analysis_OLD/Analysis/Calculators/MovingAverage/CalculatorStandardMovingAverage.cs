using Analysis.Core.Stocks;
using System;
using System.Collections.Generic;
using System.Text;
using Analysis.Calculators;
using Analysis.Core.Time;

namespace Analysis.Calculators.MovingAverage
{
    public class CalculatorStandardMovingAverage : ICalculatorMovingAverage
    {
        //Anzahl der Datenpunkte, die für einen Mittelwert betrachtet werden
        private readonly int range;

        //Die Werte, auf denen ein Mittelwert bestimmt werden soll
        private readonly Dependency dependency;

        public CalculatorStandardMovingAverage(int range, Dependency dependency)
        {
            if(range < 2)
            {
                throw new Exception("Calculating an average requires at least two data points.");
            }
            this.range = range;
            this.dependency = dependency;
        }

        public List<ValueTimeAware<decimal>> Calculate(Stock stock)
        {
            List<ValueTimeAware<decimal>> result = new List<ValueTimeAware<decimal>>();

            //Erhalte alle relevanten Daten mit ihrer Zeitabhängigkeit
            List<ValueTimeAware<decimal>> tradingValues = dependency.GetRelevantData(stock);
            if(tradingValues.Count >= range)
            {
                //Berechne ersten Mittelwert
                decimal average = 0.0m;
                List<ValueTimeAware<decimal>> relevantValues = tradingValues.GetRange(0, range);
                foreach(ValueTimeAware<decimal> timeAware in relevantValues){
                    average += timeAware.Value;
                }
                average /= range;
                DateTime timestamp = relevantValues[range-1].Timestamp;
                result.Add(new ValueTimeAware<decimal>(average, timestamp));

                //Berechne weitere Werte anhand des jeweils vorangehenden Ergebnisses
                for(int i = 1; i + range-1 < tradingValues.Count; i++)
                {
                    decimal nextAverage = average + (tradingValues[i + range - 1].Value - tradingValues[i-1].Value) /range;
                    timestamp = tradingValues[i + range - 1].Timestamp;
                    result.Add(new ValueTimeAware<decimal>(nextAverage, timestamp));
                    average = nextAverage;
                }
            }
            return result;
        }

        public List<Dependency> GetDependencies()
        {
            return new List<Dependency>() {dependency};
        }
    }
}
