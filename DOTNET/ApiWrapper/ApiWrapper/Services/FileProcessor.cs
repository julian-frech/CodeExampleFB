using Microsoft.Extensions.Configuration;
using ApiWrapper.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiWrapper.Services
{
    /// <summary>
    /// Class to read appsettings.json based on the Options pattern : https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1
    /// </summary>
    public class FileProcessor : IFileProcessor
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileProcessor> _logger;
        public FileProcessor(IConfiguration configuration, ILogger<FileProcessor> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string ReadFile(string fileLocation)
        {
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvInput>() ;

                string jsonString = JsonSerializer.Serialize(records);

                return jsonString;
            }
        }




        public FileWatchedConfig ReadAppSettings()
        {
            var csvFile = new FileWatchedConfig();
            _configuration.GetSection(FileWatchedConfig.Location).Bind(csvFile);
            _logger.LogInformation(string.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, " : Filewatcher for: ", csvFile.FileName, " at Location: ",csvFile.FileLocation));
            return csvFile;
        }

    }
}
