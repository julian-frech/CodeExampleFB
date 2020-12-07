using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketDataService.ApiService;
using MarketDataService.Data;
using MarketDataService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkSymbol=397860

namespace MarketDataService.Controllers
{
    [Route("api/Symbols")]
    [ApiController]
    public class SymbolsController : ControllerBase
    {
        private readonly SqlDatabaseContext _context;
        private readonly ApiServiceHelper _apiServiceHelper;

        public SymbolsController(SqlDatabaseContext context, ApiServiceHelper apiServiceHelper)
        {
            _context = context;
            _apiServiceHelper = apiServiceHelper;
        }

        //GET: api/Symbols
       [HttpGet]
       public async Task<ActionResult<IEnumerable<SymbolClass>>> GetSymbols()
       {
            return await _context.Symbols.ToListAsync();
       }

        // GET: api/Symbols/ARWR
        [HttpGet("{_symbol}")]
        public async Task<ActionResult<SymbolClass>> GetSymbol(string _symbol)
        {
            var symbols = await _context.Symbols.FindAsync(_symbol);

            if (symbols == null)
            {
                return NotFound();
            }

            return symbols;
        }


    }
}
