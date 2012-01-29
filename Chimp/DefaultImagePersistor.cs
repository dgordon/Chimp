using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Chimp.Services;
using Chimp.Configuration;
using Chimp.Models;

namespace Chimp
{
    public class DefaultImagePersistor : ImagePersistor
    {
        //readonly ApplicationConfiguration _configuration;
        //readonly ImageCompression _imageCompression;
        //readonly ImageTransformer _imageTransformer;

        //public WebImagePersistor(
        //    ApplicationConfiguration configuration,
        //    ImageCompression imageCompression,
        //    ImageTransformer imageTransformer
        //    )
        //{
        //    _configuration = configuration;
        //    _imageCompression = imageCompression;
        //    _imageTransformer = imageTransformer;
        //}

        //public FileSummary Save(Image image)
        //{
        //    var filename = string.Format("{0}.jpeg", Guid.NewGuid());
        //    string blogImageFilePath = Path
        //        .Combine(_configuration.ApplicationRootDirectory, _configuration.BlogImagePath, filename);
            
        //    var encoderParams = _imageCompression.GetImageCompressionParams(60L);
        //    var encoder = _imageCompression.GetImageCodec(ImageFormat.Jpeg);
            
        //    image = _imageTransformer.ScaleTo(image, 300, 350);

        //    image.Save(blogImageFilePath, encoder, encoderParams);
        //    return new FileSummary() { Name = filename };
        //}

        //public void DeleteBlogPicture(string filename)
        //{
        //    throw new NotImplementedException();
        //}
        public override Image ImageFrom(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override ImageDetails Save(Image image, DirectoryConfiguration directory)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ImageDetails imageDetails)
        {
            throw new NotImplementedException();
        }
    }
}
