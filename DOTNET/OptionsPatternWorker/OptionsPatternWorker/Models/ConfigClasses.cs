using System;
using System.Collections.Generic;
using System.Text;

namespace OptionsPatternWorker.Models
{
    public class FileWatchedConfig
    {
        public const string Location = "Location";

        public string FileName { get; set; }
        public string FileLocation { get; set; }
    }
}
