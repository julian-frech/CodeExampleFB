using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorService.Models;

namespace CalculatorService.Calculator
{
    public class RSI: ICalculator
    {

        public async Task<List<Analysis>> Calculate(IEnumerable<CalculationData> calculationData, ApiInputClass inputClass)
        {
            //string tmp;

            Console.WriteLine(calculationData.Select(x => x.symbol).FirstOrDefault());

            //int Interval = keyValuePairs.TryGetValue("interval", out tmp) ? Int32.Parse(tmp) : 0;

            //decimal Weighting = keyValuePairs.TryGetValue("weighting", out tmp) ? Decimal.Parse(tmp) : 0;


            List<Analysis> analysisData = new List<Analysis>();


            return analysisData;
        }
    }
}
