using System;
namespace FileWriter.ConfigurationOption
{
    public class FileWriterConfig
    {
        public const string FileWriterConfigSection = "FileWriterConfig";

        public string BaseFolder { get; set; }

        public FileWriterConfig()
        {
        }
    }
}
