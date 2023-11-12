using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using QRCoder;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services.Identity;

public class QRCodeService : IQRCodeService
{
    public byte[] GenerateQRCode(string userId, string serviceId, int usages)
    {
        var data = new { UserId = userId, ServiceId = serviceId, Usages = usages };
        var jsonContent = JsonSerializer.Serialize(data);

        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(jsonContent, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);

        return qrCode.GetGraphic(20);
    }
}