using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorService.Calculator;
using CalculatorService.Data;
using CalculatorService.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ninject;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkSymbol=397860

namespace CalculatorService.Controllers
{
    [Route("api/Analysis")]
    [ApiController]
    public class AnalysisDataController : ControllerBase
    {
        private readonly SqlDatabaseContext _context;

        private readonly ILogger _logger;

        private ICalculator calculator;

        public AnalysisDataController(SqlDatabaseContext context, ILogger<AnalysisDataController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Choose a calculation method to be performed on a specific company.
        /// Mandatory: Method, Symbol, Interval,
        /// Optional: Weighting
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Analysis
        ///     {
        ///        "Method": "average",
        ///        "Symbol": "msft",
        ///        "Interval": 200,
        ///        "Weighting: 1
        ///     }
        /// </remarks>
        /// <param name="inputClass"></param>
        /// <returns code="202">Latest 100 calculations.</returns>
        [HttpPost]
        public async Task<IActionResult> GetData(ApiInputClass inputClass)
        {
            try
            {
                bool StrategyNotExists = this.StrategyDecider(inputClass.Method.ToLower());

                if (StrategyNotExists)
                {
                    _logger.LogError($"Not a valid/implemented strategy: {inputClass.Method}");
                    return NotFound($"Not a valid/implemented strategy: {inputClass.Method}");
                }

                var CalcData = await _context.CalculationDatas.Where(i => i.symbol == inputClass.Symbol && i.MarketTimestamp.Hour == 0 && i.MarketTimestamp.Minute == 0).OrderBy(o => o.MarketTimestamp).Distinct().ToListAsync();

                var DataAvailable = (CalcData.Count() > 0) ? false : true;

                if (DataAvailable)
                {
                    _logger.LogError($"No data available for symbol: {inputClass.Symbol}");
                    return NotFound($"No data available for symbol: {inputClass.Symbol}");
                }

                var calcDataCleaning = await calculator.Calculate(CalcData, inputClass);

                var InputDB = calcDataCleaning.Distinct().ToList();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.BulkInsertOrUpdateAsync(InputDB);
                    transaction.Commit();
                }

                _logger.LogDebug($"Number of records updated/inserted into database: {InputDB.Count().ToString()}");

                return Accepted(InputDB.OrderByDescending(o => o.MarketTimestamp).Take(100));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return BadRequest(e.Message);
            }
        }


        private bool StrategyDecider(string _strategy)
        {
            ///System.reflection is to slow. Discrete initialization is much faster.
            try
            {
                switch (_strategy)
                {
                    case "average":
                        calculator = new AverageSimple();
                        break;
                    case "averageexp":
                        calculator = new AverageExponential();
                        break;
                    case "rsi":
                        calculator = new RSI();
                        break;
                    default:
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }


    }
}
