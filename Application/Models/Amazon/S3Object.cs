namespace SportComplexResourceOptimizationApi.Application.Models.Amazon;

public class S3Object
{
    public string Name { get; set; } = null;
    
    public MemoryStream InputStream { get; set; } = null;
    
    public string BucketName { get; set; } = null;
}