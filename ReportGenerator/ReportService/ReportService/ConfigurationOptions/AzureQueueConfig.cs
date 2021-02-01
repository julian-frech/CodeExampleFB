using System;
namespace ReportService.ConfigurationLogic
{
    public class AzureQueueConfig
    {
        public AzureQueueConfig()
        {
        }

        public const string AzureQueueConfigSection = "AzureQueueConfig";

        public string StorageConnection { get; set; }

        public string QueueName { get; set; }

    }
}


