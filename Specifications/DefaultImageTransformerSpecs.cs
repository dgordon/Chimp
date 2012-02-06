using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Chimp.Services;
using Machine.Specifications;

namespace DefaultImageTransformerSpecs
{
    public class When_scaling_an_image_constrained
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
    public class When_resizing_an_image
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

            _size = new Size(300, 100);
        };

        Because of = () => _imageMod = _imageTransformer.Resize(_imageOrig, _size);

        It should_change_height = () => _imageMod.Height.ShouldBeLessThanOrEqualTo(_size.Height);

        It should_change_width = () => _imageMod.Width.ShouldBeLessThanOrEqualTo(_size.Width);

        static Size _size;
        static ImageTransformer _imageTransformer;
        static Image _imageOrig;
        static Image _imageMod;
    }
    public class When_croping_an_image
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
            _size = new Size(20,50);
        };

        Because of = () => _imageMod = _imageTransformer.Crop(_imageOrig, 10, 10, _size);

        It should_change_height = () => _imageMod.Height.ShouldEqual(_size.Height);

        It should_change_width = () => _imageMod.Width.ShouldEqual(_size.Width);

        static Size _size;
        static ImageTransformer _imageTransformer;
        static Image _imageOrig;
        static Image _imageMod;
    }
}
