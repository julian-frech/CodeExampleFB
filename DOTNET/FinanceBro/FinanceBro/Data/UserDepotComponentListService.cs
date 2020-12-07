using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceBro.Data
{

    public interface IUserDepotComponentListService
    {
        Task<List<UserDepotComponents>> Get();
        Task<List<UserDepotComponents>> Get(string UserHK);
        Task<int> Create(int DepotId, string UserHK, string Symbol, int Quantity, decimal MarketValue, DateTime ValidFrom, DateTime ValidTo);
        Task<List<UserDepotsView>> GetView(string Username);
        Task<List<UserDepotsView>> GetAnalytics(string Username);
        Task<List<UserDepotF>> GetDepotDetails(List<int> _DepotIds);
    }

    public class UserDepotComponentListService : IUserDepotComponentListService
    {
        private readonly SqlDbContext _context;
        private readonly IUserDataListService _userDataListService;

        public UserDepotComponentListService(SqlDbContext context, IUserDataListService userDataListService)
        {
            this._context = context;
            this._userDataListService = userDataListService;
        }

        public async Task<List<UserDepotComponents>> Get()
        {
            return await _context.userDepotComponents.ToListAsync();
        }

        public async Task<List<UserDepotComponents>> Get(string UserHK)
        {
            return await _context.userDepotComponents.Where(x => x.UserHK == UserHK).ToListAsync();
        }

        public async Task<int> Create(int _depotId, string _userName, string _symbol, int _quantity, decimal _marketValue, DateTime _validFrom, DateTime _validTo)
        {
            try {

                var UserDetails = await _userDataListService.Get(_userName);

                var NewComponent = new UserDepotComponents( _depotId, UserDetails.Id,  _symbol,  _quantity,  _marketValue, _validFrom, _validTo);

                _context.userDepotComponents.Add(NewComponent);

                await _context.SaveChangesAsync();

                var feedback = 1;

                return feedback;
            }
            catch(Exception e)
            {
                
                return 2;
            }

        }

        public async Task<List<UserDepotsView>> GetView(string _userName)
        {

            return await _context.userDepotsViews.Where(x => x.UserName == _userName && x.DepotValidTo > DateTime.Now).ToListAsync();
        }

        public async Task<List<UserDepotsView>> GetAnalytics(string _userName)
        {

            var raw = await _context.userDepotsViews.Where(x => x.UserName == _userName).ToListAsync();


            return await _context.userDepotsViews.Where(x => x.UserName == _userName).ToListAsync();


        }

        public async Task<List<UserDepotF>> GetDepotDetails(List<int> _DepotIds)
        {
            
            var test = await _context.userDepotFs.ToListAsync();

            return test; // Where(s => _DepotIds.Contains(s.DepotId)).ToListAsync();
        }
    }
}
