using System;
using System.Collections.Generic;
using System.Text;

namespace OptionsPatternWorker.Models
{
    public class FileWatchedConfig
    {
        public const string LocationRead = "LocationRead";

        public string FileName { get; set; }
        public string FileLocation { get; set; }
    }

    public class FileWritingConfig
    {
        public const string LocationWrite = "LocationWrite";

        public string FileName { get; set; }
        public string FileLocation { get; set; }
    }
}
