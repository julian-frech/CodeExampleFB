using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BoersenDatenService2.Services
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

        public async Task<int> BulkInsert(DataTable dataTable, string targetTable, int FlowId)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {

                Console.WriteLine("\n 1 ... Data Transfer into Import Schema.");

                SqlBulkCopy objbulk = new SqlBulkCopy(sqlConnection);

                objbulk.DestinationTableName = targetTable;

                try
                {
                    sqlConnection.Open();

                    await objbulk.WriteToServerAsync(dataTable);

                    sqlConnection.Close();

                    await this.UpdateFlowControl(3, FlowId);

                    return 3;

                }
                catch(Exception e)
                {
                    Console.WriteLine("Error while loading into import schema: {0}", e);

                    await this.UpdateFlowControl(999, FlowId);

                    return 999;
                }
            }
            catch(Exception e)
            {

                Console.WriteLine("Could not open Database Connection: {0}", e);

                sqlConnection.Open();

                await this.UpdateFlowControl(999, FlowId);

                sqlConnection.Close();

                return 999;

            }
        }

        public async Task ImportToTargetSchema(string storedProcedure, int FlowId)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {
                sqlConnection.Open();

                Console.WriteLine("\n 2 ... Data Transformation into Production Schema.");

                SqlCommand TransferData = new SqlCommand(storedProcedure, sqlConnection);

                TransferData.CommandType = CommandType.StoredProcedure;

                await TransferData.ExecuteNonQueryAsync();

                sqlConnection.Close();

            }

            catch (Exception e)
            {

                await this.UpdateFlowControl(999, FlowId);

                Console.WriteLine("Failed to execute stored procedure: {0}", e);
            }
        }

        public async Task<int> UpdateFlowControl(int ErrorCode, int FlowID)
        {
            SqlConnection sqlConnection = this.connectionToDatabase();

            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();

                    string UpdateString = "UPDATE configuration.FlowControl set TaskSuccess = " + ErrorCode + " WHERE FlowID IN ( " + FlowID + ")";

                    SqlCommand UpdateFlowData = new SqlCommand(UpdateString, sqlConnection);

                    await UpdateFlowData.ExecuteNonQueryAsync();

                    sqlConnection.Close();

                    return 3;
                }
            }catch(Exception e)
            {
                Console.WriteLine("Could not Update FlowControl Task: {0}", e.Message);

                return 999;

            }
        }
    }
}
