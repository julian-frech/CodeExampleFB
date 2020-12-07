using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceBro.Data
{
    public interface IMarketDataListService
    {
        Task<List<MarketData>> Get();
        Task<List<MarketData>> Get(string symbol);
        Task<List<MarketData>> Get(List<string> symbols);
        Task<MarketData> Add(MarketData MarketData);
        Task<MarketData> Update(MarketData MarketData);
        Task<MarketData> Delete(int id);
        

    }
    public class MarketDataListService : IMarketDataListService
    {
        private readonly SqlDbContext _context;

        public MarketDataListService(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<List<MarketData>> Get()
        {
            return await _context.MarketDataList.OrderBy(x => x.symbol).ThenByDescending(x => x.MarketTimestamp).ToListAsync();
        }

        public async Task<List<MarketData>> Get(string symbol)
        {
            return await _context.MarketDataList.Where(s => s.symbol == symbol).OrderBy(x => x.symbol).ThenBy(x => x.MarketTimestamp).ToListAsync();
        }

        public async Task<List<MarketData>> Get(List<string> symbols)
        {
            return await _context.MarketDataList.Where(s => symbols.Contains(s.symbol)).OrderBy(x => x.symbol).ThenBy(x => x.MarketTimestamp).ToListAsync();
        }

        public async Task<List<MarketData>> Get(List<string> symbols, int dateUntil)
        {
            return await _context.MarketDataList.Where(s => symbols.Contains(s.symbol.ToLower()) && s.MarketTimestamp > DateTime.Today.AddDays(-dateUntil)).OrderBy(x => x.symbol).ThenBy(x => x.MarketTimestamp).ToListAsync();
        }

        public async Task<MarketData> Add(MarketData MarketData)
        {
            _context.MarketDataList.Add(MarketData);
            await _context.SaveChangesAsync();
            return MarketData;
        }

        public async Task<MarketData> Update(MarketData MarketData)
        {
            _context.Entry(MarketData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return MarketData;
        }

        public async Task<MarketData> Delete(int id)
        {
            var MarketData = await _context.MarketDataList.FindAsync(id);
            _context.MarketDataList.Remove(MarketData);
            await _context.SaveChangesAsync();
            return MarketData;
        }

    }
}
