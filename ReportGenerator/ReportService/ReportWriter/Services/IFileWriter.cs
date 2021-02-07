using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ReportWriter.ConfigurationOption;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReportWriter.Service
{
    public interface IFileWriter
    {
        Task StringToFile(string targetFile, string inputString);
    }

    public class FileWriter: IFileWriter
    {
        private readonly ILogger<FileWriter> _logger;

        private readonly FileWriterConfig _fileWriterConfig;

        public FileWriter(ILogger<FileWriter> logger, IOptions<FileWriterConfig> fileWriterConfig)
        {
            _logger = logger;

            _fileWriterConfig = fileWriterConfig.Value;
        }
        public async Task StringToFile(string targetFile, string inputString)
        {
            try
            {
                var target = _fileWriterConfig.BaseFolder + targetFile;

                _logger.LogInformation("Writing to: " + target);

                await File.WriteAllTextAsync(target, inputString);

            }
            catch(Exception exc)
            {
                _logger.LogInformation(exc.Message);
            }
            
        }
    }

    public class CloudFileWriter : IFileWriter
    {
        private readonly ILogger<CloudFileWriter> _logger;

        private readonly CloudFileWriterConfig _cloudFileWriterConfig;

        public CloudFileWriter(ILogger<CloudFileWriter> logger
            , IOptions<CloudFileWriterConfig> cloudFileWriterConfig)
        {
            _logger = logger;
            _cloudFileWriterConfig = cloudFileWriterConfig.Value;
        }

        public async Task StringToFile(string targetFile, string inputString)
        {

            string stoargeConnection = _cloudFileWriterConfig.StorageConnection;    

            string containerName = _cloudFileWriterConfig.ContainerName;  

            string target = _cloudFileWriterConfig.BaseFolder + targetFile;

            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(stoargeConnection);

                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(
                        new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(target);

                var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString ?? ""));

                await cloudBlockBlob.UploadFromStreamAsync(fileStream);

                _logger.LogInformation($"'{target}' uploaded to '{containerName}'");
                
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc.Message);
            }
            
        }
    }

}
