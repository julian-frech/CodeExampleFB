using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace FinanceBro.Data
{
    public interface ISymbolNewsListService
    {
        Task<List<SymbolNews>> Get();
        Task<List<SymbolNews>> Get(string str);
        Task<SymbolNews> Add(SymbolNews news);
        Task<SymbolNews> Update(SymbolNews news);
        Task<SymbolNews> Delete(int id);
        Task BulkUpdate(List<SymbolNews> symbolNews);
    }

    public class SymbolNewsListService : ISymbolNewsListService
    {
        private readonly SqlDbContext _context;

        public SymbolNewsListService(SqlDbContext context)
        {
            this._context = context;
        }

        public async Task<List<SymbolNews>> Get()
        {
            return await _context.SymbolNewsList.ToListAsync();
        }

        public async Task<List<SymbolNews>> Get(string searchvariable)
        {
            return await _context.SymbolNewsList.Where(x => x.symbol == searchvariable || x.summary.Contains(searchvariable) || x.headline.Contains(searchvariable)).ToListAsync();

        }

        public async Task<SymbolNews> Add(SymbolNews symbolNews)
        {
            _context.SymbolNewsList.Add(symbolNews);
            await _context.SaveChangesAsync();
            return symbolNews;
        }

        public async Task<SymbolNews> Update(SymbolNews symbolNews)
        {
            _context.Entry(symbolNews).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return symbolNews;
        }

        public async Task<SymbolNews> Delete(int id)
        {
            var symbolNews = await _context.SymbolNewsList.FindAsync(id);
            _context.SymbolNewsList.Remove(symbolNews);
            await _context.SaveChangesAsync();
            return symbolNews;
        }

        public async Task BulkUpdate(List<SymbolNews> symbolNews)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.BulkInsertOrUpdateAsync(symbolNews);
                transaction.Commit();
            }
        }


    }
}
