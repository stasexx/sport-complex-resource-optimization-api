﻿using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.IServices.Amazon;
using SportComplexResourceOptimizationApi.Application.Models.Amazon;
using S3Object = SportComplexResourceOptimizationApi.Application.Models.Amazon.S3Object;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services.AmazonService;

public class StorageService : IStorageService
{
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
                CannedACL = S3CannedACL.NoACL
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
        
        var mainImage = responseMain.S3Objects.Select(s3Object => s3Object.Key).FirstOrDefault();
        
        var requestNormal = new ListObjectsV2Request
        {
            BucketName = amazonS3Config.BucketName,
            Prefix = $"{sportComplexId}/normal/"
        };
        
        var responseNormal = await client.ListObjectsV2Async(requestNormal);
        
        var normalImages = responseNormal.S3Objects.Select(s3Object => s3Object.Key).ToList();

        var result = new StorageDto()
        {
            MainPhoto = mainImage,
            Photos = normalImages
        };

        return result;
    }
}