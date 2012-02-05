using System.Drawing.Imaging;

namespace Chimp.Services
{
    public class DefaultImageCompressor : ImageCompression
    {
        public DefaultImageCompressor(long compressionLevel, ImageFormat imageFormat)
            :base(compressionLevel, imageFormat) {}

        public long CompressionLevel
        {
            get { return _compressionLevel; }
        }
        public ImageFormat ImageFormat
        {
            get { return _imageFormat; }
        }
    }
}
