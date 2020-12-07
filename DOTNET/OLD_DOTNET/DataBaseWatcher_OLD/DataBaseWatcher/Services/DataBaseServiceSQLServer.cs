using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseWatcher.Services
{
    public class DataBaseServiceSQLServer : IDataBaseService
    {
        public SqlConnection connectionToDatabase()
        {
            SqlConnectionStringBuilder ConBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "teq92.database.windows.net",
                UserID = "Developer",
                Password = "WeCreateCoolFeatures2020WithBugs",
                InitialCatalog = "Datenbank01"
            };
            return new SqlConnection(ConBuilder.ConnectionString);
        }

        public void BulkInsert(DataTable dataTable, string targetTable, int FlowId)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {
                sqlConnection.Open();

                Console.WriteLine("\n 1 ... Data Transfer into Import Schema.");

                SqlBulkCopy objbulk = new SqlBulkCopy(sqlConnection);

                objbulk.DestinationTableName = targetTable;

                objbulk.BulkCopyTimeout = 120;

                this.UpdateFlowControl(0, FlowId);

                sqlConnection.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error while loading into import schema: {0}", e);

                sqlConnection.Open();

                this.UpdateFlowControl(999, FlowId);

                sqlConnection.Close();
            }
            

        }

        public void ImportToTargetSchema(string storedProcedure, int FlowId)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {
                sqlConnection.Open();

                Console.WriteLine("\n 2 ... Data Transformation into Production Schema.");

                SqlCommand TransferData = new SqlCommand(storedProcedure, sqlConnection);

                TransferData.CommandTimeout = 300;

                TransferData.CommandType = CommandType.StoredProcedure;

                TransferData.ExecuteNonQuery();

                sqlConnection.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to execute stored procedure: {0}", e);

                sqlConnection.Open();

                this.UpdateFlowControl(999, FlowId);

                sqlConnection.Close();
            }
        }

        public void UpdateFlowControl(int ErrorCode, int FlowID)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    string UpdateString = "UPDATE configuration.FlowControl set TaskSuccess = " + ErrorCode + " WHERE FlowID IN ( " + FlowID + ")";

                    SqlCommand UpdateFlowData = new SqlCommand(UpdateString, sqlConnection);

                    UpdateFlowData.CommandTimeout = 300;

                    UpdateFlowData.ExecuteNonQuery();
                }
            }catch(Exception e)
            {
                Console.WriteLine("Could not Update FlowControl Task: {0}", e.Message);

            }
        }
    }
}
