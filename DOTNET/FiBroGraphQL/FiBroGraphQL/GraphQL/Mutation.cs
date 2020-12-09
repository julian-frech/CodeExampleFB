using System;
using FiBroGraphQL.CoreServices;
using FiBroGraphQL.Models;

namespace FiBroGraphQL.GraphQL
{
    public class Mutation
    {
        private readonly IUserDepotService _userDepotService;

        public Mutation(IUserDepotService userDepotService)
        {
            this._userDepotService = userDepotService;

        }

        //public UserDepotMainObject CreateDepot(CreateDepotInput inputNewDepot)
        //{
        //    return _userDepotService.Create(inputNewDepot);
        //}

        public UserDepotMainObject DeleteDepot(DeleteDepotInput inputDepotId)
        {
            return _userDepotService.Delete(inputDepotId);
        }
    }
}
