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
        readonly ImageCompression _imageCompression;

        public DefaultImagePersistor(ImageCompression imageCompression)
        {
            if(imageCompression==null)
                throw new ArgumentNullException();

            _imageCompression = imageCompression;
        }

        public Image ImageFrom(Stream stream)
        {
            return Image.FromStream(stream);
        }

        public ImageDetails Save(Image image, string filename, DirectoryConfig directory)
        {
            var imageFormat = ImageFormat.Jpeg;
            filename = string.Format("{0}.{1}", filename, imageFormat.ToString().ToLower());

            var encoderParams = _imageCompression.GetImageCompressionParams();
            var encoder = _imageCompression.GetImageCodec();

            image.Save(Path.Combine(directory.Path, filename), encoder, encoderParams);
            
            return new ImageDetails
                            { 
                                Name = filename 
                            };
        }

        public void Delete(string filename, DirectoryConfig directory)
        {
            if (!File.Exists(Path.Combine(directory.Path, filename)))
                throw new FileNotFoundException();

            var extension = filename.Split('.')[1];

            var binDirectory = new DirectoryInfo(directory.Path);
            foreach (var file in binDirectory.GetFiles("*." + extension, SearchOption.TopDirectoryOnly))
            {
                if(filename.Equals(file.Name, StringComparison.CurrentCultureIgnoreCase))
                    file.Delete();
            }
        }
    }
}
