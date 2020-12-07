using Analysis.Core.Stocks;
using Analysis.Core.Time;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Analysis.Data.DataFetcher
{
    /// <summary>
    /// Konkrete Implementierung eines DataFetchers, der als Datenquelle eine Datenbank nutzt.
    /// </summary>
    public class DatabaseFetcher : IDataFetcher
    {       
        public List<StockTradingValue> LoadData(string symbol, DateTime begin, TimeInterval interval)
        {
            SqlConnection connection = ConnectionToDatabase();
            string sqlStatement = BuildSqlCommand(symbol, begin, interval);
            DataTable dataTable = ReadFromDatabase(connection, sqlStatement);
            return GenerateStockData(dataTable);
        }

        public List<StockTradingValue> LoadData(string symbol, DateTime from, DateTime to, TimeInterval interval)
        {
            SqlConnection connection = ConnectionToDatabase();
            string sqlStatement = BuildSqlCommand(symbol, from, to, interval);
            DataTable dataTable = ReadFromDatabase(connection, sqlStatement);
            return GenerateStockData(dataTable);
        }

        /// <summary>
        /// Antwortet eine Datanbankverbindung.
        /// </summary>
        protected SqlConnection ConnectionToDatabase()
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

        /// <summary>
        /// Erzeugt einen Select nach Symbol und Zeitstempel >= dem gegebenen Zeitpunkt.
        /// </summary>
        private string BuildSqlCommand(string symbol, DateTime from, TimeInterval interval)
        {
            string timestamp = from.ToString("yyyy-MM-dd H:mm:ss");
            return String.Format("SELECT * FROM  ANALYSIS.V_DATA_FOR_ANALYSIS WHERE Market_Timestamp >= '{0}' AND Symbol = '{1}' AND TimeType = {2} ORDER BY Market_Timestamp",
                timestamp, symbol, interval.GetKey());
        }

        /// <summary>
        /// Erzeugt einen Select nach Symbol und Zeitstempel zwischen den angegebenen Zeitpunkten.
        /// </summary>
        private string BuildSqlCommand(string symbol, DateTime from, DateTime to, TimeInterval interval)
        {
            string timestampFrom = from.ToString("yyyy-MM-dd H:mm:ss");
            string timestampTo = to.ToString("yyyy-MM-dd H:mm:ss");
            return 
                String.Format("SELECT * FROM  ANALYSIS.V_DATA_FOR_ANALYSIS WHERE Market_Timestamp >= '{0}' AND Market_Timestamp <= '{1}' AND Symbol = '{2}' AND TimeType = {3}" +
                " ORDER BY Market_Timestamp",
                timestampFrom, timestampTo, symbol, interval.GetKey());
        }

        /// <summary>
        /// Führt einen Sql-Befehl auf der Datenbank aus
        /// </summary>
        protected DataTable ReadFromDatabase(SqlConnection connection, string sqlStatement)
        {
            DataTable dataTable = new DataTable();
            connection.Open();
            SqlCommand ReadView = new SqlCommand(sqlStatement, connection);
            SqlDataReader reader = ReadView.ExecuteReader();
            dataTable.Load(reader);
            connection.Close();
            return dataTable;
        }

        /// <summary>
        /// Erzeugt aus einer DataTable eine Liste aus StockTradingValue
        /// </summary>
        protected List<StockTradingValue> GenerateStockData(DataTable data)
        {
            List<StockTradingValue> stockData = new List<StockTradingValue>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string symbol = data.Rows[i]["Symbol"].ToString();
                bool tradingDay = Convert.ToBoolean(data.Rows[i]["TimeType"]);
                DateTime timestamp = Convert.ToDateTime(data.Rows[i]["Market_Timestamp"]);
                //Handelstage sollen konsistent den Zeitstempel auf Mitternacht setzen
                if (tradingDay)
                {
                    timestamp = new DateTime(timestamp.Year, timestamp.Month, timestamp.Day, 0, 0, 0);
                }
                decimal open = Convert.ToDecimal(data.Rows[i]["Open"]);
                decimal high = Convert.ToDecimal(data.Rows[i]["High"]);
                decimal low = Convert.ToDecimal(data.Rows[i]["Low"]);
                decimal close = Convert.ToDecimal(data.Rows[i]["Close"]);
                long volume = Convert.ToInt32(data.Rows[i]["AGG_Volume"]);

                StockTradingValue stockValue = new StockTradingValue(symbol, timestamp, tradingDay, open, high, low, close, volume);
                stockData.Add(stockValue);
            }
            return stockData;
        }
    }
}
