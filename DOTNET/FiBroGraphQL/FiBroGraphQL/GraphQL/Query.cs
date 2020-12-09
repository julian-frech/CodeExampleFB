using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FiBroGraphQL.CoreServices;
using FiBroGraphQL.Data;
using FiBroGraphQL.Models;
using GraphQL;
using HotChocolate.Types;

namespace FiBroGraphQL.GraphQL
{
    public class Query
    {
        private readonly IUserDepotService _userDepotService;

        public Query( IUserDepotService userDepotService)
        {
            this._userDepotService = userDepotService;
        }

        [UseFiltering]
        public IQueryable<UserDepotsView> UserDepotsViews => _userDepotService.GetAll();

    }
}
