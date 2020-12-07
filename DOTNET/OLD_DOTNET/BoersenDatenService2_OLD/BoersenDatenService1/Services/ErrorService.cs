using System;
using System.Data.SqlClient;

namespace BoersenDatenService2.Services
{
    public class ErrorService: IErrorService
    {

        private readonly IDataBaseService dataBaseServiceSQLServer;

        public ErrorService(IDataBaseService dataBaseServiceSQLServer)
        {
            this.dataBaseServiceSQLServer = dataBaseServiceSQLServer;
        }


        public void ErrorHandler(int FlowId, int ErrorCode)
        {
            dataBaseServiceSQLServer.UpdateFlowControl(ErrorCode,FlowId);
        }
    }
}
