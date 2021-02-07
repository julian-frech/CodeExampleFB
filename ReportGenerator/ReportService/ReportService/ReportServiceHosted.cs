using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReportService.QueueService;
using ReportWriter;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using DataOperator.Context;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace ReportService
{
    public class ReportServiceHosted : BackgroundService
    {
        private readonly ILogger<ReportServiceHosted> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly IReportFile _reportFile;
        private readonly IReportsQueue _reportsQueue;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly BaseDbContext _context;

        public ReportServiceHosted(ILogger<ReportServiceHosted> logger,
            TelemetryClient telemetryClient,
            IReportFile reportFile,
            IReportsQueue reportsQueue,
            ISqlExecuter sqlExecuter,
            BaseDbContext context)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _reportFile = reportFile;
            _reportsQueue = reportsQueue;
            _sqlExecuter = sqlExecuter;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var QueueReports = _reportsQueue.CreateQueue();

            while (!stoppingToken.IsCancellationRequested)
            {
                using (_telemetryClient.StartOperation<RequestTelemetry>("Execute Async"))
                {
                    try
                    {
                        //1. Receive messages from Queue
                        var messages = _reportsQueue.GetMessages(QueueReports);

                        var ConfiguredReports = _context.ReportConfigurations.ToList();

                        while (messages.Length > 0)
                        {

                            _logger.LogInformation($"'{DateTime.Now}': Number of Messages received: ['{messages.Count()}']");

                            Parallel.ForEach(messages, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async message =>
                            {
                                byte[] data = Convert.FromBase64String(message.MessageText);

                                string ReportNameDecodedString = Encoding.UTF8.GetString(data).ToLower();

                            //Check if Report is configured
                                if (ConfiguredReports.Where(x => x.Report_Name.ToLower() == ReportNameDecodedString).Any())
                                {
                                    //2. Get data for each message via SQL execution
                                    DataTable ReportData = _sqlExecuter.GetSqlFeedback(ConfiguredReports.Where(x => x.Report_Name == ReportNameDecodedString).First().Report_Sql, DateTime.Now);

                                    //    //3. Call ReportWriter for each data
                                    if (ReportData is not null)
                                        {
                                            await _reportFile.ReportAsFileAsync(ReportData,
                                                ConfiguredReports.Where(x => x.Report_Name == ReportNameDecodedString).First().Report_Name,
                                                "",
                                                ConfiguredReports.Where(x => x.Report_Name == ReportNameDecodedString).First().Report_Separator,
                                                ConfiguredReports.Where(x => x.Report_Name == ReportNameDecodedString).First().Header_Row);
                                            _logger.LogInformation($"'{DateTime.Now}': Report '{ConfiguredReports.Where(x => x.Report_Name == ReportNameDecodedString).First().Report_Name}' created.");
                                        }
                                        else
                                        {
                                            _logger.LogError($"No data found for '{ReportNameDecodedString}'");
                                        }
                                }
                                else
                                {
                                    _logger.LogError($"'{ReportNameDecodedString}' is not a configured report!");
                                }


                            });

                            await _reportsQueue.DequeueMessages(messages, QueueReports);

                            Array.Clear(messages, 0, messages.Length);

                        }

                        _logger.LogInformation("No messages in Queue. Sleeping for 10000ms.");

                        await Task.Delay(100000, stoppingToken);

                    }
                    catch (Exception exc)
                    {
                        _logger.LogCritical(string.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, " Critical error during worker execution: ", exc.Message));
                    }

                }
                
            }

            private async Task<string> DecodeMessage(message)
            {
                byte[] data = Convert.FromBase64String(message.MessageText);

                string ReportNameDecodedString = Encoding.UTF8.GetString(data).ToLower();

                return ReportNameDecodedString;
            }
        }
    }
}
