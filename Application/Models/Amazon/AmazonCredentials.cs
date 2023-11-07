namespace SportComplexResourceOptimizationApi.Application.Models.Amazon;

public class AmazonCredentials
{
    public string AccessKey { get; set; }
    
    public string SecretKey { get; set; }
    
    public string BucketName { get; set; } = null;
}