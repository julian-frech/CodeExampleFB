
using System;
using System.Collections.Generic;
using System.Text;
using OptionsPatternWorker.Models;

namespace OptionsPatternWorker.Services
{
    public interface IFileProcessor
    {
        FileWatchedConfig ReadAppSettings();

        string ReadFile(string fileLocation);

    }
}
