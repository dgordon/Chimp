using System.Drawing.Imaging;

namespace Chimp.Services
{
    public interface ImageCompression
    {
        EncoderParameters GetImageCompressionParams(long compression);
        ImageCodecInfo GetImageCodec(ImageFormat imageFormat);
    }
}