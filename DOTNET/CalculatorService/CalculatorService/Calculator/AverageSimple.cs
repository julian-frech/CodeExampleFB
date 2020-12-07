using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CalculatorService.Models;
using MathNet.Numerics.LinearAlgebra;

namespace CalculatorService.Calculator
{
    public class AverageSimple : ICalculator
    {


        public AverageSimple() { }

        public async Task<List<Analysis>> Calculate(IEnumerable<CalculationData> calculationData, ApiInputClass inputClass)
        {

            calculationData.OrderBy(i => i.MarketTimestamp);

            List<CalculationData> calcData = AVG(calculationData.ToList(), inputClass.Interval, inputClass.Weighting);

            List<Analysis> analysisData = new List<Analysis>();

            foreach(var item in calcData)
            {
                analysisData.Add(new Analysis(1, inputClass.Interval, item.symbol, item.MarketTimestamp, item.AnalysisValue));
            }

            return analysisData;
        }

        public List<CalculationData> AVG(List<CalculationData> calcData, int interval, decimal? weighting)
        {

            // m_GD = 1/interval * sum(x(t-i))_i=0^(n-1)

            int Length = calcData.Select(x => x.close).Count();

            int n = (int)(Length / interval);

            double[] CloseValues = calcData.Select(x => (double)x.close.GetValueOrDefault()).ToArray();

            Vector<double> v = Vector<double>.Build.DenseOfArray(CloseValues);

            Matrix<double> m = Matrix<double>.Build.Dense(1, interval, 1);

            for (int i = 1; i <= Length - interval + 1; i++)
            {
                calcData[interval + i - 2].AnalysisValue = (decimal)m.Multiply(v.SubVector(i - 1, interval)).Divide(interval).First();
            }

            return calcData;
        }


    }
}
