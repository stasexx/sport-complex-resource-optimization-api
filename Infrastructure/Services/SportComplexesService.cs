using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class SportComlexesService : ISportComlexesService
{
    private readonly ISportComplexesRepository _sportComplexesRepository;

    private readonly IConfiguration _configuration;

    public SportComlexesService(ISportComplexesRepository sportComplexesRepository, IConfiguration configuration)
    {
        _sportComplexesRepository = sportComplexesRepository;
        _configuration = configuration;
    }

    public async Task UploadImageToBlobStorage(Stream imageStream, string imageName)
    {
        string connectionString = _configuration.GetConnectionString("AzureStorage:ConnectionStrings");
        string containerName = "puctures";
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        using (var stream = new MemoryStream(/* ваш поток данных из загруженного файла */))
        {
            await containerClient.UploadBlobAsync(imageName, stream);
        }
    }

    public List<string> GetSportComplexImages(string sportComplexId)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=sportcomplexorsblod;AccountKey=432UfBpjF6snXBXLT9Gdv8M+G86UakZ+FxPvQIcQ2UiAfjMD6iJG4KRpZXedsJi8kwFVwDNXt8KX+AStKk7t+Q==;EndpointSuffix=core.windows.net";
        string containerName = $"pictures"; 
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        var imageUrls = new List<string>();

        foreach (var blobItem in containerClient.GetBlobs())
        {
            // Формуємо URL для кожного Blob-об'єкта
            string imageUrl = $"https://{containerClient.Uri.Host}/{containerName}/{blobItem.Name}";
            imageUrls.Add(imageUrl);
        }

        return imageUrls;
    }
}