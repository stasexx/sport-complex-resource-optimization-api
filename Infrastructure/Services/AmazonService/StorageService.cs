using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SportComplexResourceOptimizationApi.Application.IServices.Amazon;
using SportComplexResourceOptimizationApi.Application.Models.Amazon;
using S3Object = SportComplexResourceOptimizationApi.Application.Models.Amazon.S3Object;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services.AmazonService;

public class StorageService : IStorageService
{
    private readonly IConfiguration _config;

    public StorageService(IConfiguration config)
    {
        _config = config;
    }
    
    public async Task<S3ResponseDto> UploadFileAsync(S3Object s3Object, AmazonCredentials amazonS3Config)
    {
        var credentials = new BasicAWSCredentials(amazonS3Config.AccessKey, amazonS3Config.SecretKey);

        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
        };

        var response = new S3ResponseDto();

        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3Object.InputStream,
                Key = s3Object.Name,
                BucketName = s3Object.BucketName,
                CannedACL = S3CannedACL.PublicReadWrite
            };

            using var client = new AmazonS3Client(credentials, config);

            var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Message = $"{s3Object.Name} has been uploaded successfully";
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
    
    public async Task<S3ResponseDto> ProcessUploadedFileAsync(IFormFile file, string bucketType, string photoType, string id)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var objName = $"{id}/{photoType}/{Guid.NewGuid()}";

        var s3Obj = new S3Object()
        {
            BucketName = _config[$"AmazonS3Config:{bucketType}Bucket"],
            InputStream = memoryStream,
            Name = objName
        };

        var cred = new AmazonCredentials()
        {
            AccessKey = _config["AmazonS3Config:AccessKey"],
            SecretKey = _config["AmazonS3Config:SecretKey"]
        };

        return await UploadFileAsync(s3Obj, cred);
    }
    
    public async Task<StorageDto> GetSportComplexImagesAsync(string sportComplexId, AmazonCredentials amazonS3Config)
    {
        var credentials = new BasicAWSCredentials(amazonS3Config.AccessKey, amazonS3Config.SecretKey);
    
        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
        };

        using var client = new AmazonS3Client(credentials, config);

        var requestMain = new ListObjectsV2Request
        {
            BucketName = amazonS3Config.BucketName,
            Prefix = $"{sportComplexId}/main/"
        };

        var responseMain = await client.ListObjectsV2Async(requestMain);
    
        var mainImage = responseMain.S3Objects.Select(s3Object => GetImageS3Url(amazonS3Config.BucketName, sportComplexId, s3Object.Key)).FirstOrDefault();
    
        var requestNormal = new ListObjectsV2Request
        {
            BucketName = amazonS3Config.BucketName,
            Prefix = $"{sportComplexId}/normal/"
        };
    
        var responseNormal = await client.ListObjectsV2Async(requestNormal);
    
        var normalImages = responseNormal.S3Objects.Select(s3Object => GetImageS3Url(amazonS3Config.BucketName, sportComplexId, s3Object.Key)).ToList();

        var result = new StorageDto()
        {
            MainPhoto = mainImage,
            Photos = normalImages
        };

        return result;
    }
    
    public async Task<S3ResponseDto> DeleteImageAsync(string imageName, AmazonCredentials amazonS3Config)
    {
        var credentials = new BasicAWSCredentials(amazonS3Config.AccessKey, amazonS3Config.SecretKey);

        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
        };

        var response = new S3ResponseDto();

        try
        {
            using var client = new AmazonS3Client(credentials, config);

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = amazonS3Config.BucketName,
                Key = imageName
            };

            await client.DeleteObjectAsync(deleteRequest);

            response.StatusCode = 200;
            response.Message = $"{imageName} has been deleted successfully";
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
    
    public async Task<S3ResponseDto> ReplaceImageAsync(string oldImageName, S3Object newImage, AmazonCredentials amazonS3Config)
    {
        var credentials = new BasicAWSCredentials(amazonS3Config.AccessKey, amazonS3Config.SecretKey);

        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
        };

        var response = new S3ResponseDto();

        try
        {
            using var client = new AmazonS3Client(credentials, config);

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = amazonS3Config.BucketName,
                Key = oldImageName
            };

            await client.DeleteObjectAsync(deleteRequest);

            await UploadFileAsync(newImage, amazonS3Config);

            response.StatusCode = 200;
            response.Message = $"{oldImageName} has been replaced with a new image";
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }


    private string GetImageS3Url(string bucketName, string sportComplexId, string imageKey)
    {
        return $"https://{bucketName}.s3.eu-north-1.amazonaws.com/{sportComplexId}/normal/{imageKey}";
    }
}