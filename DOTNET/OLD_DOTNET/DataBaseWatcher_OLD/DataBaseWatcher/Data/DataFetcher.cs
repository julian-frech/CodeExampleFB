using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataBaseWatcher.Model;
using Newtonsoft.Json;
using Z.BulkOperations;

namespace DataBaseWatcher.Data
{
    public class DataFetcher : IDataFetcher
    {
        readonly IConnectionBuilder connectionBuilder;

        public DataFetcher(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        public List<string> GetFlowData()
        {

            // Get the Database for which tables we have to search for Delta
            DataTable RawFlowData = ReadFromDatabase(this.BuildReadSqlCommand(DateTime.Now));

            //Build Proper Object out of the raw data
            List<DataBaseWatcherObj> FlowData = ConvertData(RawFlowData);

            //For each object in FlowData set the statement to calculate the Delta
            foreach (DataBaseWatcherObj DaBaWaobject in FlowData)
            {
                this.BuildQueryExceptCommand(DaBaWaobject);

                DataTable FlowControlRaw = ReadFromDatabase(DaBaWaobject.QueryExceptCommand);

                DataTable MaxUniqueIdentifier = ReadFromDatabase("SELECT MAX([" + DaBaWaobject.UniqueColumnName + "]) AS MaxUniqueIdentifier FROM " + DaBaWaobject.TableNameSource);

                DaBaWaobject.UniqueIdentifier = Convert.ToInt32(MaxUniqueIdentifier.Compute("MAX([MaxUniqueIdentifier])", ""));

                foreach (DataRow row in RawFlowData.Rows)
                {
                    row["UniqueIdentifier"] = DaBaWaobject.UniqueIdentifier;
                }

                //Update TaskId of each catched DataBaseWatcher ID
                this.UpdateTaskId(RawFlowData);

                for (int i = 0; i < FlowControlRaw.Rows.Count; i++)
                {
                    string temp = "";

                    foreach (DataColumn column in FlowControlRaw.Columns)
                    {
                        temp = temp + " " + column.ColumnName + "=" + FlowControlRaw.Rows[i][column].ToString() + ";";
                    }


                    DaBaWaobject.FlowDataParameter.Add(temp);

                }

                List<configuration_FlowControl> FlowObjList = new List<configuration_FlowControl>();

                // Build FlowControl Object list
                foreach (var item in DaBaWaobject.FlowDataParameter)
                {
                    

                    configuration_FlowControl ListObjs = new configuration_FlowControl(null,DaBaWaobject.AppID, DaBaWaobject.AppName, "Parameter="+DaBaWaobject.Parameter + ";" + item, DaBaWaobject.SourceTable1,null, DaBaWaobject.TargetTable1,null, 0, DateTime.Now.ToString(), DaBaWaobject.WatchId, DaBaWaobject.AnalysisId);
                    
                    FlowObjList.Add(ListObjs);
                    
                }

                //Create DataTable for BulkInsert
                DataTable FlowControlDataTable = ConvertToDatatable(FlowObjList);


                //Bulk insert into FlowControl
                if(FlowControlDataTable.Rows.Count > 0)
                {
                    this.BulkInsert(FlowControlDataTable, "configuration.FlowControl", 0);
                }
                

            }

            


            List<string> test = new List<string>();

            return test;
        }


        public void BulkInsert(DataTable dataTable, string targetTable, int FlowId)
        {
            var conn = connectionBuilder.connectionToDatabase();

            try
            {
                conn.Open();

                Console.WriteLine("\n 1 ... Data Transfer into Import Schema.");

                SqlBulkCopy objbulk = new SqlBulkCopy(conn);

                objbulk.BulkCopyTimeout = 600;

                objbulk.DestinationTableName = targetTable;

                try
                {
                    objbulk.WriteToServer(dataTable);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while loading into import schema: {0}", e);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open Database Connection: {0}", e);

            }
        }


        public void UpdateTaskId(DataTable RawFlowData)
        {
            var conn = connectionBuilder.connectionToDatabase();

            //Change TaskId accordingly
            /*foreach(DataRow row in RawFlowData.Rows)
            {
                row["TaskId"] = 1;
            }*/

            try
            {
                conn.Open();

                try
                {
                    using var bulk = new BulkOperation(conn);

                    bulk.BatchTimeout = 300;

                    bulk.DestinationTableName = "configuration.DataBaseWatcher";

                    bulk.BulkUpdate(RawFlowData);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while updating DataBaseWatcher TaskId into import schema: {0}", e);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open Database Connection: {0}", e);

            }
        }

        private DataTable ConvertToDatatable(List<configuration_FlowControl> list)
        {

            string json = JsonConvert.SerializeObject(list);

            DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);

            return pDt;
        }
          
        private DataTable ReadFromDatabase(string sqlStatement)
        {
            DataTable dataTable = new DataTable();

            var conn = connectionBuilder.connectionToDatabase();

            using (conn)
            {
                conn.Open();

                SqlCommand ReadView = new SqlCommand(sqlStatement, conn);

                ReadView.CommandTimeout = 600;

                SqlDataReader reader =  ReadView.ExecuteReader();

                dataTable.Load(reader);
            }

            return dataTable;
        }

        private List<DataBaseWatcherObj> ConvertData(DataTable data)
        {
            List<DataBaseWatcherObj> _Data = new List<DataBaseWatcherObj>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int WatchId = Convert.ToInt32(data.Rows[i]["WatchId"]);
                string TableNameSource = data.Rows[i]["TableNameSource"].ToString();
                string TableNameTarget = data.Rows[i]["TableNameTarget"].ToString();
                string ColumnNamesSource = data.Rows[i]["ColumnNamesSource"].ToString();
                string ColumnNamesTarget = data.Rows[i]["ColumnNamesTarget"].ToString();
                string FilterSource = data.Rows[i]["FilterSource"].ToString();
                string FilterTarget = data.Rows[i]["FilterTarget"].ToString();
                string AggregationInterval = data.Rows[i]["AggregationInterval"].ToString();
                int AppID = Convert.ToInt32(data.Rows[i]["AppID"]);
                string AppName = data.Rows[i]["AppName"].ToString();
                string Parameter = data.Rows[i]["Parameter"].ToString();
                string SourceTable1 = data.Rows[i]["SourceTable1"].ToString();
                string TargetTable1 = data.Rows[i]["TargetTable1"].ToString();
                DateTime TaskPlanned = Convert.ToDateTime(data.Rows[i]["TaskPlanned"]);
                DateTime ValidFrom = Convert.ToDateTime(data.Rows[i]["ValidFrom"]);
                DateTime ValidTo = Convert.ToDateTime(data.Rows[i]["ValidTo"]);
                DateTime ITS = Convert.ToDateTime(data.Rows[i]["ITS"]);
                DateTime UTS = Convert.ToDateTime(data.Rows[i]["UTS"]);
                string Status = data.Rows[i]["Status"].ToString();
                int TaskId = Convert.ToInt32(data.Rows[i]["TaskId"]);
                int AnalysisId = Convert.ToInt32(data.Rows[i]["AnalysisId"]);
                int UniqueIdentifier = Convert.ToInt32(data.Rows[i]["UniqueIdentifier"]);
                string UniqueColumnName = data.Rows[i]["UniqueColumnName"].ToString();
                string SourceTableGroupe = data.Rows[i]["SourceTableGroupe"].ToString();
                string TargetTableGroupe = data.Rows[i]["TargetTableGroupe"].ToString();
                List<string> Init = new List<string>();

                DataBaseWatcherObj DataBaseW = new DataBaseWatcherObj(WatchId, TableNameSource, TableNameTarget, ColumnNamesSource, ColumnNamesTarget, FilterSource, FilterTarget, AggregationInterval, AppID, AppName, Parameter, SourceTable1, TargetTable1, TaskPlanned, ValidFrom, ValidTo, ITS, UTS, Status, "", Init,TaskId, AnalysisId, UniqueIdentifier, UniqueColumnName, SourceTableGroupe, TargetTableGroupe);
                _Data.Add(DataBaseW);
            }
            return _Data;
        }

        private string BuildReadSqlCommand(DateTime from)
        {
            string timestamp = from.ToString("yyyy-MM-dd H:mm:ss");
            return String.Format("Select" +
                "[WatchId]"+
      ",[TableNameSource]"+
      ",[TableNameTarget]" +
      ",[ColumnNamesSource]" +
      ",[ColumnNamesTarget]" +
      ",[FilterSource]" +
      ",[FilterTarget]" +
      ",[AggregationInterval]" +
      ",[AppID]" +
      ",[AppName]" +
      ",[Parameter]" +
      ",[SourceTable1]" +
      ",[TargetTable1]" +
      ",[TaskPlanned]" +
      ",[ValidFrom]" +
      ",[ValidTo]" +
      ",[ITS]" +
      ",[UTS]" +
      ",[Status]"+
      ",[TaskId]" +
      ", [AnalysisId]" +
      ", [UniqueIdentifier] " +
      ", [UniqueColumnName]" +
      ", [SourceTableGroupe]"+
      ", [TargetTableGroupe] "+
      " FROM configuration.DataBaseWatcher"+
      " WHERE GETDATE() BETWEEN [ValidFrom] and ValidTo" +
      " AND Status = 'V'" +
      " AND TaskId = 0");
        }

        private void BuildQueryExceptCommand(DataBaseWatcherObj dataBaseWatcherObj)
        {

                string filtersSource = (dataBaseWatcherObj.FilterSource.Length == 0) ? " " : (" AND " + dataBaseWatcherObj.FilterSource);

                string filtersTarget = (dataBaseWatcherObj.FilterTarget.Length == 0) ? " " : (" AND " + dataBaseWatcherObj.FilterTarget);

                dataBaseWatcherObj.QueryExceptCommand = "SELECT " +
                    dataBaseWatcherObj.ColumnNamesSource +
                    " FROM " +
                    dataBaseWatcherObj.TableNameSource +
                    " WHERE " + dataBaseWatcherObj.UniqueColumnName+ ">"+ dataBaseWatcherObj.UniqueIdentifier+
                    filtersSource +" "+
                    dataBaseWatcherObj.SourceTableGroupe +
                    " EXCEPT " +
                    " SELECT " +
                    dataBaseWatcherObj.ColumnNamesTarget +
                    " FROM " +
                    dataBaseWatcherObj.TableNameTarget +
                    " WHERE AnalysisId = " + dataBaseWatcherObj.AnalysisId + 
                    filtersTarget;
        }
    }
}
