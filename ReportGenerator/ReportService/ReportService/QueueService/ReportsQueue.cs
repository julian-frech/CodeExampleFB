using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReportService.ConfigurationLogic;

namespace ReportService.QueueService
{

    public interface IReportsQueue
    {
        Task DequeueMessages(QueueMessage[] messages, QueueClient queueClient);
        QueueClient CreateQueue();
        QueueMessage[] GetMessages(QueueClient queueClient);
    }

    public class ReportsQueue : IReportsQueue
    {

        private readonly ILogger<ReportsQueue> _logger;

        private readonly AzureQueueConfig _azureQueueConfig;

        public ReportsQueue(ILogger<ReportsQueue> logger
            , IConfiguration configuration
            , IOptions<AzureQueueConfig> azureQueueConfig)
        {
            _logger = logger;
            _azureQueueConfig = azureQueueConfig.Value;
        }

        public async Task DequeueMessages(QueueMessage[] messages, QueueClient queueClient)
        {
            if (queueClient.Exists())
            {
                foreach (QueueMessage message in messages)
                {
                    try { 
                    _logger.LogInformation($"De-queued message: '{message.MessageText}'");

                    // Delete the message
                    await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                    }
                    catch(Exception exc)
                    {
                        _logger.LogError($"Could not dequeue message: '{exc.Message}'");
                    }
                }
            }
        }


        public QueueMessage[] GetMessages(QueueClient queueClient)
        {
            try
            {//numofmessages = 32 max
                return queueClient.ReceiveMessages(32);
            }
            catch (Exception exc)
            {
                _logger.LogError($"Could not receive messages from Queue: '{exc.Message}'");
                return null;
            }
        }

        public QueueClient CreateQueue()
        {

            string connectionString = _azureQueueConfig.StorageConnection;

            string queueName = _azureQueueConfig.QueueName;

            try
            {
                // Get the connection string from app settings
                //string connectionString = Configuration["ConnectionString:StorageConnection"];

                // Instantiate a QueueClient which will be used to create and manipulate the queue
                QueueClient queueClient = new QueueClient(connectionString, queueName);

                // Create the queue
                queueClient.CreateIfNotExists();

                if (queueClient.Exists())
                {
                    _logger.LogInformation($"Queue created: '{queueClient.Name}'");
                    return queueClient;
                }
                else
                {
                    _logger.LogInformation($"Make sure the Azurite storage emulator running and try again.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception: {ex.Message}\n\n");
                _logger.LogInformation($"Make sure the Azurite storage emulator running and try again.");
                return null;
            }
        }


    }


    public class AwsQueueClient
    {
        private static async Task<ReceiveMessageResponse> GetMessage(
IAmazonSQS sqsClient, string qUrl, int waitTime = 0, int MaxMessages = 10)
        {
            return await sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = qUrl,
                MaxNumberOfMessages = MaxMessages,
                WaitTimeSeconds = waitTime
                // (Could also request attributes, set visibility timeout, etc.)
            });
        }
    }
 
}
