using System;
using System.Drawing;
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
            filename = string.Format("{0}.{1}"
                , filename
                , _imageCompression.ImageFormat.ToString().ToLower());

            var encoderParams = _imageCompression.EncoderParameters();
            var encoder = _imageCompression.CodecInfo();

            image.Save(Path.Combine(directory.Path, filename), encoder, encoderParams);

            return new ImageDetails
                            { 
                                Name = filename,
                                Size = new Size(image.Width,image.Height)
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
