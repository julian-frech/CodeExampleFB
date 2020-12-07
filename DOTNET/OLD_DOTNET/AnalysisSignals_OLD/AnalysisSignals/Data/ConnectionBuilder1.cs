using System;
using System.Data.SqlClient;
using AnalysisSignals.Services;

namespace AnalysisSignals.Data
{
    public class ConnectionBuilder1: IConnectionBuilder
    {
        string ConnectionString { get; }

        public SqlConnection connectionToDatabase()
        {
            SqlConnectionStringBuilder ConBuilder = new SqlConnectionStringBuilder {
                DataSource = "teq92.database.windows.net",
                UserID = "Developer",
                Password = "WeCreateCoolFeatures2020WithBugs",
                InitialCatalog = "Datenbank01",
                ConnectTimeout = 120
            };
            return new SqlConnection(ConBuilder.ConnectionString);
        }
    }
}
