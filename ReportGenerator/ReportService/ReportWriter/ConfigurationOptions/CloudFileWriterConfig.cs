using System;
namespace FileWriter.ConfigurationOption
{
    public class CloudFileWriterConfig
    {

        public const string CloudFileWriterConfigSection = "CloudFileWriterConfig";

        public string BaseFolder { get; set; }

        public  string StorageConnection { get; set; }

        public string ContainerName { get; set; }

        public CloudFileWriterConfig()
        {
        }
    }
}
