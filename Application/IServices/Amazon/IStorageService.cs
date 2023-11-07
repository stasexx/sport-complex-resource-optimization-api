using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Amazon;

namespace SportComplexResourceOptimizationApi.Application.IServices.Amazon;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3Object, AmazonCredentials amazonS3Config);

    Task<StorageDto> GetSportComplexImagesAsync(string sportComplexId, AmazonCredentials amazonS3Config);
}