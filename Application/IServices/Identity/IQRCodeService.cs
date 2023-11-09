namespace SportComplexResourceOptimizationApi.Application.IServices.Identity;

public interface IQRCodeService
{
    byte[] GenerateQRCode(string userId, string serviceId, int usages);
    
}