using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using MarketDataService.Data;
using MarketDataService.Models;
using Microsoft.AspNetCore.Mvc;


namespace MarketDataService.ApiService
{


    [Route("api/Trigger")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
  
        private readonly SqlDatabaseContext _context;
        
        public TriggerController(SqlDatabaseContext context)
        {
            _context = context;
        }

        ApiServiceHelper apiServiceHelper = new ApiServiceHelper();


        // Post: api/Trigger/
        [HttpPost]
        public async Task<ActionResult<string>> GetSymbol(IEXCloudBaseClass iEXCloudBaseClass)
        {

            ModelClass modelClass = RoutParameterToGenerateModel(iEXCloudBaseClass);

           
            var FeedbackTuple = await apiServiceHelper.PassArgumentsToApiBulk(iEXCloudBaseClass, modelClass);

            ICollection<IEXCloudClass> classzeug = (ICollection<IEXCloudClass>)FeedbackTuple.Item2;

            classzeug.Select(c => { c.method = iEXCloudBaseClass.Method; return c; })
                                     .Where(y => y.method is null)
                                     .ToList();

            classzeug.Select(c => { c.symbol = iEXCloudBaseClass.Symbol; return c; })
                                     .Where(y => y.symbol is null)
                                     .ToList();

            classzeug.Select(c => { c.sourceAPI = iEXCloudBaseClass.sourceAPI; return c; })
                                     .Where(y => y.sourceAPI is null)
                                     .ToList();

            await _context.AddRangeAsync(classzeug);

            await _context.BulkInsertOrUpdateAsync(classzeug.ToList());

            return FeedbackTuple.Item1;

        }


        private static ModelClass RoutParameterToGenerateModel(IEXCloudBaseClass iEXCloudBaseClass)
        {
            return GenerateModel.GetModelClass(iEXCloudBaseClass);
        }


    }

    public class GenerateModel
    {
        public static ModelClass GetModelClass(IEXCloudBaseClass iEXCloudBaseClass)
        {
            var InterfaceClass = default(IModel);

            switch (iEXCloudBaseClass.Method)
            {
                case "company":
                    InterfaceClass = new IEXCloudClassCompany();
                    break;
                case "symbols":
                    InterfaceClass = new ArrayIEXCloudClassSymbolList();
                    break;
                case "chart":
                    InterfaceClass = new IEXCloudClassHistoricList();
                    break;
            }


            return new ModelClass()
            {
                Model = InterfaceClass
            };
        }
    }
}
