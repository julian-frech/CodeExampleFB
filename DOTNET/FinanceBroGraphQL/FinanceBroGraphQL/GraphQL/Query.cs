using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FinanceBroGraphQL.CoreServices;
using FinanceBroGraphQL.Data;
using FinanceBroGraphQL.Models;
using GraphQL;
using HotChocolate.Types;

namespace FinanceBroGraphQL.GraphQL
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
