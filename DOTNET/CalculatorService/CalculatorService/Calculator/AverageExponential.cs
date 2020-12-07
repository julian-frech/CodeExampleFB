using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CalculatorService.Models;

namespace CalculatorService.Calculator
{
    public class AverageExponential : ICalculator
    {


        public AverageExponential()
        {

        }

        public async Task<List<Analysis>> Calculate(IEnumerable<CalculationData> calculationData, ApiInputClass inputClass)
        {

            calculationData.OrderBy(i => i.MarketTimestamp);

            // m_GD = 1/interval * sum(x(t-i))_i=0^(n-1)

            List<CalculationData> calcData = AVG(calculationData.ToList(), inputClass.Interval, inputClass.Weighting);

            List<Analysis> analysisData = new List<Analysis>();

            foreach (var calcDataSet in calcData)
            {
                analysisData.Add(new Analysis(2, inputClass.Interval, calcDataSet.symbol, calcDataSet.MarketTimestamp, calcDataSet.AnalysisValue));
            }

            return analysisData;
        }



        public List<CalculationData> AVG(List<CalculationData> calcData, int interval, decimal? weighting)
        {
            decimal smoothing = 2;

            decimal smoothingFactor = smoothing / (interval + 1);

            calcData = calcData.OrderBy(x => x.MarketTimestamp).ToList();

            calcData[0].AnalysisValue = (decimal)calcData[0].close;

            int Length = calcData.Select(x => x.close).Count();

            //EMA(0) = C(0);
            //EMA(t) = EMA(t-1) + (SF * (C(t) - EMA(t-1));

            for (int i = 1; i < Length; i++)
            {
                calcData[i].AnalysisValue = calcData[i-1].AnalysisValue + (smoothingFactor * ((decimal)calcData[i].close - calcData[i - 1].AnalysisValue));
            }

            Console.WriteLine(string.Join("\t\n", calcData.Select(x => new { x.MarketTimestamp, x.AnalysisValue, x.close }).OrderBy(x => x.MarketTimestamp).ToList()));

            return calcData;
        }


    }
}
