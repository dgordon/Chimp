using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using Machine.Specifications;
using System.Drawing;
using Chimp;
using Chimp.Models;
using Chimp.Configuration;
using Chimp.Services;

namespace Specifications.Services
{
    public class When_creating_image_from_stream
    {
        Establish context = () => 
                                {
                                    var uriPath = 
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                                    var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
                                    
                                    _directory = A.Fake<DirectoryConfig>();
                                    A.CallTo(() => _directory.Path)
                                        .Returns(localPath);

                                    //stream for testing only
                                    _stream = new MemoryStream();
                                    var image = Image.FromFile(localPath + "Images/test picture.jpg");
                                    image.Save(_stream, ImageFormat.Jpeg);
                                    
                                    _imagePersistor = new DefaultImagePersistor(A.Fake<ImageCompression>());
                                };

        Because of = () => _image = _imagePersistor.ImageFrom(_stream);

        It should_return_image = () => _image.ShouldNotBeNull();

        static Stream _stream;
        static ImagePersistor _imagePersistor;
        static Image _image;
        static DirectoryConfig _directory;
    }
    public class When_saving_image
    {
        Establish context = () =>
                                {
                                    var uriPath = 
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                                    var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath + "Images";
                                    _image = Image.FromFile(Path.Combine(localPath,"test picture.jpg"));

                                    _directory = A.Fake<DirectoryConfig>();
                                    A.CallTo(() => _directory.Path)
                                        .Returns(localPath);

                                    _imagePersistor = A.Fake<ImagePersistor>();
                                    _imageCompression = A.Fake<ImageCompression>();

                                    var encoderParameters = new EncoderParameters(1);
                                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L);

                                    A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
                                        .Returns(encoderParameters);

                                    A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
                                        .Returns(ImageCodecInfo.GetImageDecoders()
                                            .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid));

                                    _imagePersistor = new DefaultImagePersistor(_imageCompression);
                                };

        Because of = () => _details = _imagePersistor.Save(_image, "test image saved", _directory);

        It should_get_directory_path_for_image = () =>
                                A.CallTo(() => _directory.Path)
                                    .MustHaveHappened(Repeated.Exactly.Once);

        It should_create_image_encode_parameters = () =>
                                A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
                                        .MustHaveHappened(Repeated.Exactly.Once);

        It should_get_image_codec = () =>
                                A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
                                        .MustHaveHappened(Repeated.Exactly.Once);

        It should_have_saved_the_image = () =>  File.Exists(Path.Combine(_directory.Path, _details.Name)).ShouldBeTrue();

        It should_return_image_details = () => _details.ShouldNotBeNull();

        Cleanup by_deleting_the_saved_image = () =>
                                 {
                                     var binDirectory = new DirectoryInfo(_directory.Path);
                                     foreach (var file in binDirectory.GetFiles("*.jpeg", SearchOption.TopDirectoryOnly))
                                         file.Delete();
                                 };

        static DirectoryConfig _directory;
        static ImagePersistor _imagePersistor;
        static ImageCompression _imageCompression;
        static Image _image;
        static ImageDetails _details;
    }
    
    public class When_deleting_image
    {
        Establish context = () =>
            {
                var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                _localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath + "Images";

                _directory = A.Fake<DirectoryConfig>();
                A.CallTo(() => _directory.Path)
                    .Returns(_localPath);

                _image = Image.FromFile(Path.Combine(_localPath ,"test picture.jpg"));
                _testFilename = "delete me.jpeg";

                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L);

                var encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                _image.Save(Path.Combine(_localPath, _testFilename), encoder, encoderParameters);

                _imagePersistor = new DefaultImagePersistor(A.Fake<ImageCompression>());
            };

        Because of = () => _imagePersistor.Delete(_testFilename, _directory);

        It should_get_file_directory_location = () =>
            A.CallTo(() => _directory.Path).MustHaveHappened(Repeated.Exactly.Twice);

        It should_have_removed_the_image_from_the_file_system = () =>
            File.Exists(Path.Combine(_localPath, _testFilename)).ShouldBeFalse();

        static string _localPath;
        static DirectoryConfig _directory;
        static ImagePersistor _imagePersistor;
        static Image _image;
        static string _testFilename;
    }
    
    public class When_deletting_image_fails
    {
        Establish context = () =>
        {
            //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //_localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath + "Images";

            _directory = A.Fake<DirectoryConfig>();
            A.CallTo(() => _directory.Path)
                .Returns("some/fake/path");

            //_image = Image.FromFile(Path.Combine(_localPath, "test picture.jpg"));
            _testFilename = "does not exist.jpeg";

            //var encoderParameters = new EncoderParameters(1);
            //encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L);

            //var encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
            //_image.Save(Path.Combine(_localPath, _testFilename), encoder, encoderParameters);

            _imagePersistor = new DefaultImagePersistor(A.Fake<ImageCompression>());
        };

        Because of = () => _imagePersistor.Delete(_testFilename, _directory);

        It should_get_file_directory_location = () =>
            A.CallTo(() => _directory.Path).MustHaveHappened(Repeated.Exactly.Once);

        It should_cause_an_error = () =>
            A.CallTo(()=> _imagePersistor.Delete(A<string>._, A<DirectoryConfig>._))
            .Throws(new FileNotFoundException());
            //File.Exists(Path.Combine(_localPath, _testFilename)).ShouldBeFalse();

        static DirectoryConfig _directory;
        static ImagePersistor _imagePersistor;
        static string _testFilename;
    }
}