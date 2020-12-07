using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnalysisSignals.Model;
using AnalysisSignals.Services;
using Z.BulkOperations;

namespace AnalysisSignals.Data
{ 
    public class DatabaseFetcher : IDataFetcher
    {
        readonly IConnectionBuilder ConBuilder;

        public DatabaseFetcher(IConnectionBuilder ConBuilder)
        {
            this.ConBuilder = ConBuilder;
        }


        public async Task BulkMerge(string TargetTable, List<AnalysisData> analysisDatas)
        {
            SqlConnection Connection = this.ConBuilder.connectionToDatabase();
            try
            {
                Connection.Open();


                using var bulk = new BulkOperation(Connection);

                bulk.DestinationTableName = TargetTable;

                bulk.BatchTimeout = 120;

                await bulk.BulkMergeAsync(analysisDatas);

                Connection.Close();
            }
            catch (Exception e)
            {
                Connection.Close();

                Console.WriteLine("Could not bulk insert into table:{0}. {1}", TargetTable, e.Message);
            }
        }

        public async Task<DataTable> ReadFromDatabase(string sqlStatement)
        {
            DataTable dataTable = new DataTable();

            SqlConnection Connection = this.ConBuilder.connectionToDatabase();

            try
            {

                Connection.Open();

                SqlCommand ReadView = new SqlCommand(sqlStatement, Connection);

                SqlDataReader reader = await ReadView.ExecuteReaderAsync();

                ReadView.CommandTimeout = 120;

                dataTable.Load(reader);

                Connection.Close();

            }
            catch(Exception e)
            {
                Connection.Close();

                Console.WriteLine("Could not open Connection:{0}", e.Message);
            }
        
            return dataTable;
        }

    }
}
