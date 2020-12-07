using System;
using System.Linq;
using FinanceBroGraphQL.GraphQL;
using FinanceBroGraphQL.Models;

namespace FinanceBroGraphQL.CoreServices
{
    public interface IUserDepotService
    {
        IQueryable<UserDepotsView> GetAll();
        IQueryable<UserDepotsView> GetSpecific(int DepotId);
        UserDepotMainObject Delete(DeleteDepotInput InputDepotId);
        //UserDepotMainObject Create(CreateDepotInput InputNewDepot);
    }
}
