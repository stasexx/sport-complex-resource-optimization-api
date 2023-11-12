using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices.Amazon;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;
using SportComplexResourceOptimizationApi.Application.Models.Amazon;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("s3")]
public class S3Controller : BaseController
{
    private readonly IStorageService _storageService;

    private readonly IQRCodeService _qrCodeService;

    private readonly IConfiguration _config;

    public S3Controller(IStorageService storageService, IQRCodeService qrCodeService, IConfiguration config)
    {
        _storageService = storageService;
        _qrCodeService = qrCodeService;
        _config = config;
    }
    
    [HttpPost("UploadQrCode")]
    public async Task<IActionResult> UploadQrCode(string ssId, string userId, string serviceId, int usages)
    {
        await using var memoryStr = new MemoryStream();

        var s3Obj = new S3Object()
        {
            BucketName = _config["AmazonS3Config:Bucket"],
            InputStream = memoryStr,
            Name = ""
        };

        var qrCodeImageBytes = _qrCodeService.GenerateQRCode(userId, serviceId, usages);
        s3Obj.BucketName = _config["AmazonS3Config:QRCodeBucket"];
        s3Obj.InputStream = new MemoryStream(qrCodeImageBytes);
        s3Obj.Name = $"{ssId}/qr-code/{Guid.NewGuid()}.png";

        var cred = new AmazonCredentials()
        {
            AccessKey = _config["AmazonS3Config:AccessKey"],
            SecretKey = _config["AmazonS3Config:SecretKey"]
        };

        var result = await _storageService.UploadFileAsync(s3Obj, cred);

        return Ok(result);
    }

    [HttpPost(Name = "UploadFile")]
    public async Task<IActionResult> UploadFile(IFormFile file, string bucketType, string photoType, string id)
    {
        var result = await _storageService.ProcessUploadedFileAsync(file, bucketType, photoType, id);

        return Ok(result);
    }
    
    
    [HttpGet("images/{sportComplexId}")]
    public async Task<ActionResult<StorageDto>> GetSportComplexImages(string sportComplexId)
    {
        var amazonS3Config = new AmazonCredentials
        {
            AccessKey = _config["AmazonS3Config:AccessKey"],
            SecretKey = _config["AmazonS3Config:SecretKey"],
            BucketName = _config["AmazonS3Config:Bucket"]
        };
        
        var imageUrls = await _storageService.GetSportComplexImagesAsync(sportComplexId, amazonS3Config);

        return Ok(imageUrls);
    }
    
    [HttpPost("/replace/{id}", Name = "ReplaceImage")]
    public async Task<IActionResult> ReplaceImage(IFormFile newFile, string oldImageName, string bucketType, string id)
    {
        await using var memoryStream = new MemoryStream();
        await newFile.CopyToAsync(memoryStream);

        var newObjectName = $"{id}/main/{Guid.NewGuid()}";

        var newS3Object = new S3Object()
        {
            BucketName = _config[$"AmazonS3Config:{bucketType}Bucket"],
            InputStream = memoryStream,
            Name = newObjectName
        };

        var cred = new AmazonCredentials()
        {
            AccessKey = _config["AmazonS3Config:AccessKey"],
            SecretKey = _config["AmazonS3Config:SecretKey"],
            BucketName = _config[$"AmazonS3Config:{bucketType}Bucket"]
        };
        
        var deleteResult = await _storageService.DeleteImageAsync(oldImageName, cred);

        if (deleteResult.StatusCode != 200)
        {
            return BadRequest($"Помилка при видаленні фотографії: {deleteResult.Message}");
        }

        var replaceResult = await _storageService.ReplaceImageAsync(oldImageName, newS3Object, cred);

        if (replaceResult.StatusCode == 200)
        {
            return Ok("Фотографія була успішно замінена");
        }
        else
        {
            return BadRequest($"Помилка при заміні фотографії: {replaceResult.Message}");
        }
    }
    
    [HttpDelete("DeleteImage")]
    public async Task<IActionResult> DeleteImage(string imageName, string bucketType)
    {

        var cred = new AmazonCredentials()
        {
            AccessKey = _config["AmazonS3Config:AccessKey"],
            SecretKey = _config["AmazonS3Config:SecretKey"],
            BucketName = _config[$"AmazonS3Config:{bucketType}Bucket"]
        };

        var result = await _storageService.DeleteImageAsync(imageName, cred);

        return Ok(result);
    }
}