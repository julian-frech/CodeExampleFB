using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceBro.Data
{

    public interface ISymbolFactsDataListService
    {
        Task<List<SymbolFacts>> Get();
        Task<SymbolFacts> Get(string str);
        Task<SymbolFacts> Add(SymbolFacts news);
        Task<SymbolFacts> Update(SymbolFacts news);
        Task<SymbolFacts> Delete(int id);
    }

    public class SymbolFactsDataListService : ISymbolFactsDataListService
    {
        private readonly SqlDbContext _context;

        public SymbolFactsDataListService(SqlDbContext context)
        {
            this._context = context;
        }


        public async Task<List<SymbolFacts>> Get()
        {
            //return await _context.SymbolFactsDataList.ToListAsync();
            return await _context.SymbolFactsDataList.OrderBy(x => x.CompanyName).ThenByDescending(x => x.Symbol).ToListAsync();
        }

        public async Task<SymbolFacts> Get(string searchvariable)
        {
            return  await _context.SymbolFactsDataList.FindAsync(searchvariable);

        }

        public async Task<SymbolFacts> Add(SymbolFacts SymbolFacts)
        {
            _context.SymbolFactsDataList.Add(SymbolFacts);
            await _context.SaveChangesAsync();
            return SymbolFacts;
        }

        public async Task<SymbolFacts> Update(SymbolFacts SymbolFacts)
        {
            _context.Entry(SymbolFacts).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return SymbolFacts;
        }

        public async Task<SymbolFacts> Delete(int id)
        {
            var SymbolFacts = await _context.SymbolFactsDataList.FindAsync(id);
            _context.SymbolFactsDataList.Remove(SymbolFacts);
            await _context.SaveChangesAsync();
            return SymbolFacts;
        }

    }
}
