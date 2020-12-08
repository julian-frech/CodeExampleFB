
using System;
using System.Collections.Generic;
using System.Text;
using OptionsPatternWorker.Models;

namespace OptionsPatternWorker.Services
{
    public interface IFileProcessor
    {
        FileWatchedConfig ReadAppSettingsRead();

        string ReadFile(string fileLocation);

        CsvOutput WriteFile();
    }
}
