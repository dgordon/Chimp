using System.Drawing.Imaging;
using System.Linq;

namespace Chimp.Services
{
    public abstract class ImageCompression
    {
        protected long _compressionLevel;
        protected ImageFormat _imageFormat;

        protected ImageCompression(long compressionLevel, ImageFormat imageFormat)
        {
            CompressionLevel = compressionLevel;
            _imageFormat = imageFormat;
        }

        protected virtual long CompressionLevel
        {
            set
            {
                if (value < 0)
                    _compressionLevel = 0;
                else if (value > 100)
                    _compressionLevel = 100;
                else
                _compressionLevel = value;
            }
        }

        public virtual EncoderParameters CompressionParameters()
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, _compressionLevel);
            return encoderParameters;
        }
        public virtual ImageCodecInfo CodecInfo()
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == _imageFormat.Guid);
        }
    }
}