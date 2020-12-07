using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BoersenDatenService2.Services
{
    public interface IDataBaseService
    {
        
        abstract SqlConnection connectionToDatabase();

        public Task<int> BulkInsert(DataTable dataTable, string targetTable, int FlowId);

        public Task ImportToTargetSchema(string storedProcedure, int FlowId);

        public Task<int> UpdateFlowControl(int ErrorCode, int FlowID);

    }



}
