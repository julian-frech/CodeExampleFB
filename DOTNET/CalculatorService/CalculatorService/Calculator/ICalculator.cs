using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorService.Models;

namespace CalculatorService.Calculator
{
    public interface ICalculator
    {

        public Task<List<Analysis>> Calculate(IEnumerable<CalculationData> calculationData, ApiInputClass inputClass);

    }
}
