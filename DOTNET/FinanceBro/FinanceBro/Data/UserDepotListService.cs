using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinanceBro.Data
{

    public interface IUserDepotListService
    {
        Task<List<UserDepotD>> Get();
        Task<List<UserDepotD>> Get(string UserHK);
        Task<int> CreateD(string UserName, string DepotName);
        Task<int> CreateF(int DepotId, string UserHK, string DepotName);
        Task<int> DeactivateReactivateDepot(int _DepotId, DateTime _ValidTo);

    }

    public class UserDepotListService: IUserDepotListService
    {
        private readonly SqlDbContext _context;
        private readonly IUserDataListService _userDataListService;
        private readonly ILogger _logger;

        public UserDepotListService(SqlDbContext context, IUserDataListService userDataListService, ILogger<UserDepotListService> logger)
        {
            this._context = context;
            this._userDataListService = userDataListService;
            this._logger = logger;
        }

        public async Task<List<UserDepotD>> Get()
        {
            return await _context.userDepotDs.ToListAsync();
        }

        public async Task<List<UserDepotD>> Get(string UserHK)
        {
            return await _context.userDepotDs.Where(x => x.UserHK == UserHK).ToListAsync();
        }

        public async Task<int> CreateD(string UserName, string DepotName)
        {
            try {

                var UserDetails = await _userDataListService.Get(UserName);

                var NewUserDepotD = new UserDepotD(UserDetails.Id);

                _context.userDepotDs.Add(NewUserDepotD);

                await _context.SaveChangesAsync();

                var AllUserDepots = await this.Get(UserDetails.Id);

                var NewDepotId = AllUserDepots.Select(x => x.DepotId).Max();

                var feedback = await CreateF(NewDepotId, UserDetails.Id, DepotName);

                _logger.LogInformation(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Successfully created new DepotD object in database!"));

                return feedback;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return 2;
            }

        }

        public async Task<int> CreateF(int DepotId, string UserHK, string DepotName)
        {
            try
            {
                var NewUserDepotF = new UserDepotF(DepotId, UserHK, DepotName);

                _context.userDepotFs.Add(NewUserDepotF);

                await _context.SaveChangesAsync();

                _logger.LogInformation(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Successfully created new DepotF object in database!"));

                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Could not create database object for DepotF!"));
                return 2;
            }

        }

        public async Task<int> DeactivateReactivateDepot(int _DepotId, DateTime _ValidTo)
        {
            
            try {
                UserDepotD DepotToDeactivateD = await _context.userDepotDs.Where(s => s.DepotId == _DepotId).FirstOrDefaultAsync();
                UserDepotF DepotToDeactivateF = await _context.userDepotFs.Where(s => s.DepotId == _DepotId).FirstOrDefaultAsync();

                DepotToDeactivateD.ValidTo = _ValidTo;
                DepotToDeactivateD.UTS = DateTime.Now;
                DepotToDeactivateF.ValidTo = _ValidTo;
                DepotToDeactivateF.UTS = DateTime.Now;

                _context.Update(DepotToDeactivateF);
                _context.Update(DepotToDeactivateD);

                await _context.SaveChangesAsync();

                _logger.LogInformation(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Successfully changed validTo in database for DepotId = ", _DepotId, ". ValidTo = ", _ValidTo));

                

                return 1;
            }
            catch(Exception e)
            {
                _logger.LogError(string.Join("Not able to deactivate user depot with DepotID: ", _DepotId, e.Message));
                _logger.LogError(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Could not deactivate user depot with DepotId = ", _DepotId));
                return 2;
            }
            
        }

    }
}
