using System;
using System.Threading.Tasks;
using BoersenDatenService2.Services;

namespace BoersenDatenService1.Interfaces
{
    public class BoersenDatenService
    {
        readonly IApi api;
        readonly IProperties property;
        readonly IDataBaseService databaseService;
        readonly IErrorService errorService;

        public BoersenDatenService(IApi api, IProperties property, IDataBaseService dataBaseServiceSQLServer, IErrorService errorService)
        {
            this.api = api;
            this.property = property;
            this.databaseService = dataBaseServiceSQLServer;
            this.errorService = errorService;
        }

        public async Task<int> PassParameters(string[] args, string storedProcedure)
        {
            try
            {
                string[] PassingArguments = this.property.PassArguments(args);

                var test = await this.api.PassArgumentsToApiBulk(PassingArguments);

                await this.databaseService.ImportToTargetSchema(storedProcedure, Int32.Parse(PassingArguments[4]));

                return 3;

            }
            catch(Exception e)
            {
                Console.WriteLine("Skipped one FlowTask: {0}. For Parameters: {1}", e, args.ToString());

                return 999;
            }
        }
    }
}
