using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using FinanceBroGraphQL.Models;

namespace FinanceBroGraphQL.GraphQL
{

    //Azure sql database
    public class UserDepotType : ObjectType<UserDepotsView>
    {
        protected override void Configure(IObjectTypeDescriptor<UserDepotsView> descriptor)
        {
            descriptor.Field(b => b.DepotId).Type<IdType>();
            descriptor.Field(b => b.UserName).Type<StringType>();
            descriptor.Field(b => b.DepotName).Type<StringType>();
            descriptor.Field(b => b.DepotValidFrom).Type<DateType>();
        }
    }

    public class DepotDType : ObjectType<UserDepotMainObject>
    {
        protected override void Configure(IObjectTypeDescriptor<UserDepotMainObject> descriptor)
        {
            descriptor.Field(b => b.DepotId).Type<IdType>();
            descriptor.Field(b => b.UserHK).Type<StringType>();
            descriptor.Field(b => b.ValidFrom).Type<DateType>();
            descriptor.Field(b => b.ValidTo).Type<DateType>();
        }
    }

}
