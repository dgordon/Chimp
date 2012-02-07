using System.Drawing.Imaging;
using System.Linq;
using Chimp.Services;
using Machine.Specifications;

namespace DefaultImageCompressionSpecs
{
    public class When_getting_image_compression_parameters
    {
        Establish context = () => _imageCompressor = new DefaultImageCompressor(100L, ImageFormat.Jpeg);

        Because of = () => _encoderParams = _imageCompressor.EncoderParameters();

        It should_have_atleast_one_encoder_parameter = () => _encoderParams.Param.Any().ShouldBeTrue();

        static DefaultImageCompressor _imageCompressor;
        static EncoderParameters _encoderParams;
    }

    public class When_getting_image_codec_information
    {
        Establish context = () =>
            {
                _imageFormat = ImageFormat.Jpeg;
                _imageCompressor = new DefaultImageCompressor(100L, _imageFormat);
            };

        Because of = () => _codecInfo = _imageCompressor.CodecInfo();

        It should_return_the_correct_image_codec = () => _codecInfo.FormatID.ShouldEqual(_imageFormat.Guid);

        static DefaultImageCompressor _imageCompressor;
        static ImageFormat _imageFormat;
        static ImageCodecInfo _codecInfo;
    }
    public class When_the_compression_level_above_one_hundred
    {
        Establish context = () => _imageCompressor = new DefaultImageCompressor(5000L, ImageFormat.Jpeg);

        Because of = () => _compressionLevel = _imageCompressor.CompressionLevel;

        It should_change_the_compression_level_to_one_hundred = () => _compressionLevel.ShouldEqual(100L);

        static DefaultImageCompressor _imageCompressor;
        static long _compressionLevel;
    }
    public class When_the_compression_level_below_zero
    {
        Establish context = () => _imageCompressor = new DefaultImageCompressor(-2345L, ImageFormat.Jpeg);

        Because of = () => _compressionLevel = _imageCompressor.CompressionLevel;

        It should_change_the_compression_level_to_zero = () => _compressionLevel.ShouldEqual(0);

        static DefaultImageCompressor _imageCompressor;
        static long _compressionLevel;
    }
}
