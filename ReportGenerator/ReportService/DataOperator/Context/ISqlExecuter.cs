using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataOperator.Context
{
    public interface ISqlExecuter
    {
        DataTable GetSqlFeedback(string SqlString, DateTime? DateParameter);
    }

    public class SqlExecuter : ISqlExecuter
    {
        public IConfiguration Configuration { get; }

        private readonly ILogger<SqlExecuter> _logger;

        public SqlExecuter(IConfiguration configuration, ILogger<SqlExecuter> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public DataTable GetSqlFeedback(string SqlCommand, DateTime? DateParameter)
        {
            var connectionString = Configuration["ConnectionString:SqlDatabaseContext"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = SqlCommand;

                using (var dataAdapter = new SqlDataAdapter())
                {
                    var dataFromSql = new DataTable();
                    
                    dataAdapter.SelectCommand = new SqlCommand(SqlCommand, connection);
                    
                    //// avoid SQL-Injection
                    dataAdapter.SelectCommand.Parameters.Add("@DATE", SqlDbType.Date);
                    
                    dataAdapter.SelectCommand.Parameters["@DATE"].Value = DateParameter;

                    _logger.LogInformation(dataAdapter.SelectCommand.CommandText);
                    
                    try
                    {
                        connection.Open();

                        dataAdapter.Fill(dataFromSql);

                        return dataFromSql;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                        
                        return null;
                    }
                }
            }
        }


    }
}
