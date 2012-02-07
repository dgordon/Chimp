using System.Drawing.Imaging;

namespace Chimp.Services
{
    public class DefaultImageCompressor : ImageCompression
    {
        public DefaultImageCompressor(long compressionLevel, ImageFormat imageFormat)
            :base(compressionLevel, imageFormat) {}
    }
}
