﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Text.Json;
using OptionsPatternWorker.Models;
using System.Collections.Generic;

namespace OptionsPatternWorker.Services
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

        public CsvOutput WriteFile()
        {
            FileWritingConfig CsvOutputFileConfig = ReadAppSettingsWrite();

            CsvOutput NewCsvOutput =
    new CsvOutput { FirstLine = "first", SecondLine = "second", ThirdLine = "third" };

            using (var writer = new StreamWriter(string.Concat(CsvOutputFileConfig.FileLocation, CsvOutputFileConfig.FileName)))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<CsvOutput>();
                csv.NextRecord();
                csv.WriteRecord(NewCsvOutput);
            }

            return NewCsvOutput;
        }

        public FileWritingConfig ReadAppSettingsWrite()
        {
            var csvFile = new FileWritingConfig();
            _configuration.GetSection(FileWritingConfig.LocationWrite).Bind(csvFile);
            _logger.LogInformation(string.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, " : File writer for: ", csvFile.FileName, " at Location: ", csvFile.FileLocation));

            return csvFile;
        }

        public FileWatchedConfig ReadAppSettingsRead()
        {
            var csvFile = new FileWatchedConfig();
            _configuration.GetSection(FileWatchedConfig.LocationRead).Bind(csvFile);
            _logger.LogInformation(string.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, " : File watcher for: ", csvFile.FileName, " at Location: ",csvFile.FileLocation));
            return csvFile;
        }

    }
}
