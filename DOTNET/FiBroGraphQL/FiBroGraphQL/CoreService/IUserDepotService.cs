using System;
using System.Linq;
using FiBroGraphQL.GraphQL;
using FiBroGraphQL.Models;

namespace FiBroGraphQL.CoreServices
{
    public interface IUserDepotService
    {
        IQueryable<UserDepotsView> GetAll();
        IQueryable<UserDepotsView> GetSpecific(int DepotId);
        UserDepotMainObject Delete(DeleteDepotInput InputDepotId);
        //UserDepotMainObject Create(CreateDepotInput InputNewDepot);
    }
}
