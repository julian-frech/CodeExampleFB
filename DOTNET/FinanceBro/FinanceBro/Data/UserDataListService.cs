using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceBro.Data
{

    public interface IUserDataListService
    {
        Task<UserAccount> Get(string UserName);
    }

    public class UserDataListService : IUserDataListService
    {
        private readonly SqlDbContext _context;

        public UserDataListService(SqlDbContext context)
        {
            this._context = context;
        }


        public async Task<UserAccount> Get(string _userName)
        {
            return await _context.userAccounts.Where(x => x.UserName == _userName).FirstOrDefaultAsync();
        }
        
    }


}
