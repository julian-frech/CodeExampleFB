using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceBro.Data
{
    public interface ILatestDatesListService
    {
        Task<List<LatestDate>> Get();
        Task<LatestDate> Get(string symbol);


    }
    public class LatestDatesListService : ILatestDatesListService
    {
        private readonly SqlDbContext _context;

        public LatestDatesListService(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<List<LatestDate>> Get()
        {
            return await _context.latestDates.ToListAsync();
        }

        public async Task<LatestDate> Get(string _symbol)
        {
            return await _context.latestDates.Where(x => x.symbol == _symbol).FirstOrDefaultAsync();
        }


    }
}
