using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FlowController.Interfaces;
using FlowController.Model;
using Z.BulkOperations;

namespace FlowController.Data
{ 
    public class DatabaseFetcher : IDataFetcher
    {
        readonly IConnectionBuilder ConBuilder;

        public DatabaseFetcher(IConnectionBuilder ConBuilder)
        {
            this.ConBuilder = ConBuilder;
        }

        public List<configuration_FlowControl> GetFlowData()
        {
            SqlConnection Connection = this.ConBuilder.connectionToDatabase();

            string SqlStatement = buildSqlCommand( DateTime.Now);

            DataTable RawFlowData = readFromDatabase(Connection, SqlStatement);

            List<configuration_FlowControl> FlowData = ConvertData(RawFlowData);


            /*if(FlowData.Exists(x => x.WatchId > 0))
            {
                this.UpdateDataBaseWatcherTaskId();
            }*/

            return FlowData;


        }

        private void UpdateDataBaseWatcherTaskId()
        {
            SqlConnection conn = this.ConBuilder.connectionToDatabase();

            try
            {
                conn.Open();

                SqlCommand updateWatchIdTaskId = new SqlCommand("Update configuration.DataBaseWatcher set TaskId = 0 where TaskId = 1", conn);

                updateWatchIdTaskId.ExecuteNonQueryAsync();

                conn.Close();


            }
            catch(Exception e)
            {
                Console.WriteLine("Could not execute update on configuration.DataBaseWatcher: {0}", e.Message);
            }
           

        }

        public DataTable readFromDatabase(SqlConnection Connection, string sqlStatement)
        {
            DataTable dataTable = new DataTable();

            using (Connection)
            {
                Connection.Open();

                SqlCommand ReadView = new SqlCommand(sqlStatement, Connection);

                SqlDataReader reader = ReadView.ExecuteReader();

                ReadView.CommandTimeout = 600;

                dataTable.Load(reader);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["TaskSuccess"] = 1;
                }

                using var bulk = new BulkOperation(Connection);

                bulk.DestinationTableName = "configuration.FlowControl";

                bulk.BulkUpdate(dataTable);

                //SqlCommand updateFlowControl = new SqlCommand("Update configuration.FlowControl set TaskSuccess = 1 where TaskSuccess = 0", Connection);

                //updateFlowControl.CommandTimeout = 600;

                //updateFlowControl.ExecuteNonQueryAsync();
                //updateFlowControl.ExecuteNonQuery();

                Connection.Close();
            }

            return dataTable;
        }

        private string buildSqlCommand(DateTime from)
        {
            string timestamp = from.ToString("yyyy-MM-dd H:mm:ss");
            return String.Format("SELECT TOP(10000) [FlowID]" + 
                  ",[AppID]" +
                  ",[AppName]" +
                  ",[Parameter]" +
                  ",[SourceTable1]" +
                  ",[SourceTable2]" + 
                  ",[TargetTable1]" +
                  ",[TargetTable2]" +
                  ",[TaskSuccess]" +
                  ",[TaskPlanned]" +
                  ",[WatchId]" +
                  ",[AnalysisId] " +
                    "FROM[configuration].[FlowControl]"+
                        "WHERE TaskSuccess = 0", //+
                        //"AND GETDATE() >= TaskPlanned",
                            timestamp);
        }

        public List<configuration_FlowControl> ConvertData(DataTable data)
        {
            List<configuration_FlowControl> _Data = new List<configuration_FlowControl>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int FlowID = Convert.ToInt32(data.Rows[i]["FlowID"]);
                int AppID = Convert.ToInt32(data.Rows[i]["AppID"]);
                string AppName = data.Rows[i]["AppName"].ToString();
                string Parameter = data.Rows[i]["Parameter"].ToString() + "FLOWID=" + FlowID.ToString() + ";";
                string SourceTable1 = data.Rows[i]["SourceTable1"].ToString();
                string SourceTable2 = data.Rows[i]["SourceTable2"].ToString();
                string TargetTable1 = data.Rows[i]["TargetTable1"].ToString();
                string TargetTable2 = data.Rows[i]["TargetTable2"].ToString();
                int TaskSuccess = Convert.ToInt32(data.Rows[i]["TaskSuccess"]);
                string TaskPlanned = data.Rows[i]["TaskPlanned"].ToString();
                int? WatchId = (data.Rows[i]["WatchId"].Equals(DBNull.Value)) ? (int?) null : Convert.ToInt32(data.Rows[i]["WatchId"]);
                int? AnalysisId = (data.Rows[i]["AnalysisId"].Equals(DBNull.Value)) ? (int?)null : Convert.ToInt32(data.Rows[i]["AnalysisId"]);

                configuration_FlowControl FlowData = new configuration_FlowControl(FlowID, AppID, AppName, Parameter, SourceTable1,SourceTable2, TargetTable1, TargetTable2, TaskSuccess, TaskPlanned, WatchId, AnalysisId);

                _Data.Add(FlowData);
            }
            return _Data;
        }

        public int UpdateFlowData(List<string> FlowID)
        {
            SqlConnection Connection = this.ConBuilder.connectionToDatabase();

            string[] FlowIDsWithTriggerFile = FlowID.ToArray();

            using (Connection)
            {
                Connection.Open();

                string UpdateString = "UPDATE configuration.FlowControl set TaskSuccess = 2 WHERE FlowID IN ( " + string.Join(",", FlowIDsWithTriggerFile) + ")";

                SqlCommand UpdateFlowData = new SqlCommand(UpdateString, Connection);

                UpdateFlowData.ExecuteNonQuery();
            }

        return 1;

        }
    }
}
