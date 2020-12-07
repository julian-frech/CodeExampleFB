using ApiWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWrapper.Services
{
    public interface IFileProcessor
    {
        FileWatchedConfig ReadAppSettings();

        string ReadFile(string fileLocation);

    }
}
