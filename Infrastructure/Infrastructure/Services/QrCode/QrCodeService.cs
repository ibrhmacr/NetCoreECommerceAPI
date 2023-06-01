using Application.Abstractions.Services;
using QRCoder;

namespace Infrastructure.Services.QrCode;

public class QrCodeService : IQrCodeService
{
    
    public byte[] GenerateQrCode(string text)
    {
        QRCodeGenerator generator = new();
        QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);//todo QrCode ilgili urunun sayfasina gidecek sekilde ayarlanicak.
        PngByteQRCode qrCode = new(data);
        byte[] byteGraphic = qrCode.GetGraphic(10,
            new byte[] { 78, 42, 132 }, 
            new byte[] { 240, 240, 240 });
        return byteGraphic;
    }
}