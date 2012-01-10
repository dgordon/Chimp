﻿using System.Linq;
using System.Drawing.Imaging;

namespace PaintingWithFire.Data.Services.Imaging
{
    public class WebImageCompressor : ImageCompression
    {
        //http://msdn.microsoft.com/en-us/library/bb882583.aspx#Y296
        public EncoderParameters GetImageCompressionParams(long compression)
        {
            var myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, compression);
            return myEncoderParameters;
        }
        public ImageCodecInfo GetImageCodec(ImageFormat imageFormat)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == imageFormat.Guid);
        }
    }
}
