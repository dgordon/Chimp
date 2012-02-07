using System.Drawing.Imaging;
using System.Linq;

namespace Chimp.Services
{
    public abstract class ImageCompression
    {
        private long _compressionLevel;
        private readonly ImageFormat _imageFormat;

        protected ImageCompression(long compressionLevel, ImageFormat imageFormat)
        {
            _compressionLevel = compressionLevel;
            _imageFormat = imageFormat;
        }

        public virtual long CompressionLevel
        {
            set { _compressionLevel = value; }
            get
            {
                long val;
                if (_compressionLevel < 0)
                    val = 0;
                else if (_compressionLevel > 100)
                    val = 100;
                else
                    val = _compressionLevel;

                return val;
            }
        }
        public virtual ImageFormat ImageFormat
        {
            get { return _imageFormat; }
        }

        public virtual EncoderParameters EncoderParameters()
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, CompressionLevel);
            return encoderParameters;
        }
        public virtual ImageCodecInfo CodecInfo()
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == _imageFormat.Guid);
        }
    }
}