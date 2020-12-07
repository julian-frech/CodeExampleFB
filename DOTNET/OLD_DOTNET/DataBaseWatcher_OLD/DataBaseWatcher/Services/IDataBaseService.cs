using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseWatcher.Services
{
    public interface IDataBaseService
    {
        
        abstract SqlConnection connectionToDatabase();

        public void BulkInsert(DataTable dataTable, string targetTable, int FlowId);

        public void ImportToTargetSchema(string storedProcedure, int FlowId);

        public void UpdateFlowControl(int ErrorCode, int FlowID);

    }



}
