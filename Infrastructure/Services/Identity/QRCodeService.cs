using System.Text.Json;
using QRCoder;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services.Identity;

public class QRCodeService : IQRCodeService
{
    public byte[] GenerateQRCode(string userFirstName, string userLastName, int usages)
    {
        var data = new { FirstName = userFirstName, LastName = userLastName, Usages = usages };
        var jsonContent = JsonSerializer.Serialize(data);

        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(jsonContent, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);

        return qrCode.GetGraphic(20);
    }
}