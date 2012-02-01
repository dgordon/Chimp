using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
                                        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                                    var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
                                    
                                    _directory = A.Fake<DirectoryConfiguration>();
                                    A.CallTo(() => _directory.Path)
                                        .Returns(localPath);

                                    //setup stream for testing
                                    var stream = new MemoryStream();
                                    var image = Image.FromFile(localPath + "Images/test picture.jpg");
                                    image.Save(stream, ImageFormat.Jpeg);
                                    

                                    _imagePersistor = new DefaultImagePersistor(A.Fake<ImageCompression>());
                                };

        Because of = () => _image = _imagePersistor.ImageFrom(_stream);

        It should_return_image = () => _image.ShouldNotBeNull();

        Cleanup by_deleting_the_image = () => { };

        static Stream _stream;
        static ImagePersistor _imagePersistor;
        static ImageCompression _imageCompression;
        static Image _image;
        static DirectoryConfiguration _directory;
    }
    public class When_saving_image
    {
        Establish context = () =>
                                {
                                    //var uriPath = Path.GetDirectoryName(
                                    //    System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                                    //_localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
                                    //_image = Image.FromFile(_localPath + "Services/Imaging/test picture.jpg");
                                    
                                    //_configuration = A.Fake<ApplicationConfiguration>();
                                    //A.CallTo(() => _configuration.BlogImagePath)
                                    //    .Returns("bin");
                                    //A.CallTo(() => _configuration.ApplicationRootDirectory)
                                    //    .Returns(_localPath);

                                    //_imageCompression = A.Fake<ImageCompression>();
                                    //var myEncoderParameters = new EncoderParameters(1);
                                    //myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 60L);
                                    //A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
                                    //    .Returns(myEncoderParameters);
                                    //A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
                                    //    .Returns(ImageCodecInfo.GetImageDecoders()
                                    //        .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid));
                                    
                                    //_imageTransformer = A.Fake<ImageTransformer>();
                                    //A.CallTo(() =>
                                    //    _imageTransformer.ScaleTo(A<Image>._, A<int>._, A<int>._))
                                    //    .Returns(_image);

                                    //_imagePersistor = new WebImagePersistor(
                                    //    _configuration,_imageCompression,_imageTransformer);
                                    _imagePersistor = A.Fake<ImagePersistor>();
                                    _imageCompression = A.Fake<ImageCompression>();
                                };

        //Because of = () => _details = _imagePersistor.Save(_image, _directory);

        It should_get_directory_path_for_image = () =>
                                A.CallTo(() => _directory.Path)
                                    .MustHaveHappened(Repeated.Exactly.Once);
        
        //It should_create_image_encode_parameters = () =>
        //                        A.CallTo(() => _imageCompression.GetImageCompressionParams(A<long>._))
        //                                .MustHaveHappened(Repeated.Exactly.Once);
        
        //It should_get_image_codec = () =>
        //                        A.CallTo(() => _imageCompression.GetImageCodec(A<ImageFormat>._))
        //                                .MustHaveHappened(Repeated.Exactly.Once);

        It should_have_saved_the_image = () => File.Exists(_localPath + "/bin/" + _details.Name);

        It should_return_image_details = () => _details.ShouldNotBeNull();

        Cleanup by_deleting_the_saved_image = () =>
                                 {
                                     var binDirectory = new DirectoryInfo(_localPath + "/bin/");
                                     foreach (var file in binDirectory.GetFiles("*.jpeg", SearchOption.TopDirectoryOnly))
                                         file.Delete();
                                 };

        static string _localPath;
        static DirectoryConfiguration _directory;
        static ImagePersistor _imagePersistor;
        static ImageCompression _imageCompression;
        static ImageTransformer _imageTransformer;
        static Image _image;
        static ImageDetails _details;
    }
    public class When_deleting_image
    {
        Establish context = () => { };

        Because of = () => _imagePersistor.Delete(_details);

        It should_get_file_directory_location = () =>
            A.CallTo(() => _directory.Path).MustHaveHappened(Repeated.Exactly.Once);

        It should_have_removed_the_image_from_the_file_system = () => {};

        static DirectoryConfiguration _directory;
        static ImagePersistor _imagePersistor;
        static Image _image;
        static ImageDetails _details;
    }
}