using System.Drawing.Imaging;
using System.Linq;

namespace Chimp.Services
{
    public abstract class ImageCompression
    {
        protected long _compression;
        protected ImageFormat _imageFormat;

        protected ImageCompression(long compression, ImageFormat imageFormat)
        {
            _compression = compression;
            _imageFormat = imageFormat;
        }

        public virtual EncoderParameters GetImageCompressionParams()
        {
            var myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, _compression);
            return myEncoderParameters;
        }
        public virtual ImageCodecInfo GetImageCodec()
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == _imageFormat.Guid);
        }
    }
}