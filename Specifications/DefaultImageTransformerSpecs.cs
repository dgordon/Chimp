using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Chimp.Services;
using Machine.Specifications;

namespace DefaultImageTransformerSpecs
{
    public class When_scale_image_constrained
    {
        Establish context = () =>
            {
                var uriPath =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;

                var stream = new MemoryStream();
                _imageOrig = Image.FromFile(localPath + "Images\\test picture.jpg");
                _imageOrig.Save(stream, ImageFormat.Jpeg);

                _imageTransformer = new DefaultImageTransformer();

                _height = 100;
                _width = 300;
            };

        Because of = () => _imageMod = _imageTransformer.ConstrainedScale(_imageOrig, _width, _height, Color.Blue);

        It should_change_height = () => _imageMod.Height.ShouldBeLessThanOrEqualTo(_height);

        It should_change_width = () => _imageMod.Width.ShouldBeLessThanOrEqualTo(_width);

        static int _height;
        static int _width;
        static ImageTransformer _imageTransformer;
        static Image _imageOrig;
        static Image _imageMod;
    }
    public class When_scale_image_proportionally
    {
        Establish context = () =>
        {
            var uriPath =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;

            var stream = new MemoryStream();
            _imageOrig = Image.FromFile(localPath + "Images\\test picture.jpg");
            _imageOrig.Save(stream, ImageFormat.Jpeg);

            _imageTransformer = new DefaultImageTransformer();

            _height = 100;
            _width = 300;
        };

        Because of = () => _imageMod = _imageTransformer.ProportionalScale(_imageOrig, _width, _height, Color.White);

        It should_change_height = () => _imageMod.Height.ShouldBeLessThanOrEqualTo(_height);

        It should_change_width = () => _imageMod.Width.ShouldBeLessThanOrEqualTo(_width);

        static int _height;
        static int _width;
        static ImageTransformer _imageTransformer;
        static Image _imageOrig;
        static Image _imageMod;
    }
}
