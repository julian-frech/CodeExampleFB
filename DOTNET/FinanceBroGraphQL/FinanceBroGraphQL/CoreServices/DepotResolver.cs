using FinanceBroGraphQL.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System.Linq;

namespace FinanceBroGraphQL.CoreServices
{
    public class DepotResolver
    {
        private readonly IUserDepotService _userDepotService;

        public DepotResolver([Service]IUserDepotService userDepotService)
        {
            this._userDepotService = userDepotService;
        }

        public UserDepotsView GetDepot(UserDepotMainObject userDepot, IResolverContext resolverContext)
        {
            return _userDepotService.GetAll().Where(a => a.DepotId == userDepot.DepotId).FirstOrDefault();
        }
    }
}
