using System;
using System.Collections.Generic;
using System.Linq;
using FinanceBroGraphQL.Data;
using FinanceBroGraphQL.ExceptionHandler;
using FinanceBroGraphQL.GraphQL;
using FinanceBroGraphQL.Models;

namespace FinanceBroGraphQL.CoreServices
{
    public class UserDepotService: IUserDepotService
    {

        private readonly SqlDbContext _sqlDbContext;

        public UserDepotService(SqlDbContext sqlDbContext)
        {
            this._sqlDbContext = sqlDbContext;

        }

        public IQueryable<UserDepotsView> GetAll()
        {
            return _sqlDbContext.userDepotsViews.AsQueryable();
        }

        public IQueryable<UserDepotsView> GetSpecific(int DepotId)
        {
            return _sqlDbContext.userDepotsViews.Where(x => x.DepotId == DepotId).AsQueryable();
        }

        public UserDepotMainObject Delete(DeleteDepotInput InputDepotId)
        {

            var DepotToDelete = _sqlDbContext.userDepotMainObj.FirstOrDefault(w => w.DepotId == InputDepotId.DepotId);

            if(DepotToDelete is null)  throw new DepotNotFoundException() { DepotId = InputDepotId.DepotId };

            _sqlDbContext.userDepotMainObj.Remove(DepotToDelete);

            _sqlDbContext.SaveChanges();

            return DepotToDelete;

        }

        //public UserDepotMainObject Create(CreateDepotInput InputNewDepot)
        //{

        //    var NewUserDepotD = new UserDepotMainObject(InputNewDepot.UserName);

        //    return NewUserDepotD;

        //}



    }
}
