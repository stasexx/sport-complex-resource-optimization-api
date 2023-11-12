using Application.Models.Dtos;
using Microsoft.AspNetCore.Http;
using SportComplexResourceOptimizationApi.Application.Models.Amazon;

namespace SportComplexResourceOptimizationApi.Application.IServices.Amazon;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3Object, AmazonCredentials amazonS3Config);

    Task<S3ResponseDto> ProcessUploadedFileAsync(IFormFile file, string bucketType, string photoType, string id);

    Task<StorageDto> GetSportComplexImagesAsync(string sportComplexId, AmazonCredentials amazonS3Config);
    
    Task<S3ResponseDto> DeleteImageAsync(string imageName, AmazonCredentials amazonS3Config);

    Task<S3ResponseDto> ReplaceImageAsync(string oldImageName, S3Object newImage, AmazonCredentials amazonS3Config);

}