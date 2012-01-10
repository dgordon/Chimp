using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FakeItEasy;
using Machine.Specifications;
using PaintingWithFire.Data.Models;
using PaintingWithFire.Data.Services;
using System.Drawing;
using PaintingWithFire.Data.Services.Imaging;

namespace Specifications.Services
{
    public class When_saving_a_blog_picture
    {
        Establish context = () =>
                                {
                                    var uriPath = Path.GetDirectoryName(
                                        System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                                    _localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
                                    _image = Image.FromFile(_localPath + "Services/Imaging/test picture.jpg");
                                    
                                    _configuration = A.Fake<ApplicationConfiguration>();
                                    A.CallTo(() => _configuration.BlogImagePath)
                                        .Returns("bin");
                                    A.CallTo(() => _configuration.ApplicationRootDirectory)
                                        .Returns(_localPath);

                                    _imageCompression = A.Fake<ImageCompression>();
                                    var myEncoderParameters = new EncoderParameters(1);
                                    myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L);
                                    A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
                                        .Returns(myEncoderParameters);
                                    A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
                                        .Returns(ImageCodecInfo.GetImageDecoders()
                                            .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid));
                                    
                                    _imageTransformer = A.Fake<ImageTransformer>();
                                    A.CallTo(() =>
                                        _imageTransformer.ScaleTo(A<Image>._, A<int>._, A<int>._))
                                        .Returns(_image);

                                    _imagePersistor = new WebImagePersistor(
                                        _configuration,_imageCompression,_imageTransformer);
                                };

        Because of = () => _fileSummary = _imagePersistor.SaveBlogPicture(_image);

        It should_get_application_root_directory = () => 
                                A.CallTo(() => _configuration.ApplicationRootDirectory)
                                    .MustHaveHappened(Repeated.Exactly.Once);
        It should_get_path_for_blog_images = () => 
                                A.CallTo(() => _configuration.BlogImagePath)
                                     .MustHaveHappened(Repeated.Exactly.Once);
        It should_scale_the_image = () =>
                                A.CallTo(() => _imageTransformer.ScaleTo(A<Image>._, A<int>._, A<int>._))
                                    .MustHaveHappened(Repeated.Exactly.Once);
        It should_create_image_encode_parameters = () =>
                                A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
                                        .MustHaveHappened(Repeated.Exactly.Once);
        It should_get_image_codec = () =>
                                A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
                                        .MustHaveHappened(Repeated.Exactly.Once);
        It should_have_saved_the_image = () => File.Exists(_localPath + "/bin/" + _fileSummary.Name);

        Cleanup by_deleting_the_saved_image = () =>
                                 {
                                     var binDirectory = new DirectoryInfo(_localPath + "/bin/");
                                     foreach (var file in binDirectory.GetFiles("*.jpeg", SearchOption.TopDirectoryOnly))
                                         file.Delete();
                                 };

        static string _localPath;
        static ImagePersistor _imagePersistor;
        static ImageCompression _imageCompression;
        static ImageTransformer _imageTransformer;
        static ApplicationConfiguration _configuration;
        static Image _image;
        static FileSummary _fileSummary;
    }
}
