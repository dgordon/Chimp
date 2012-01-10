using System.Drawing.Imaging;

namespace PaintingWithFire.Data.Services.Imaging
{
    public interface ImageCompression
    {
        EncoderParameters GetImageCompressionParams(long compression);
        ImageCodecInfo GetImageCodec(ImageFormat imageFormat);
    }
}