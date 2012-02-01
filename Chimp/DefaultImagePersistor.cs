﻿using System;
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

        public ImageDetails Save(Image image, string filename, DirectoryConfiguration directory)
        {
            //todo: refactor
            var imageFormat = ImageFormat.Jpeg;
            filename = string.Format("{0}.{1}", filename, imageFormat.ToString().ToLower());

            var encoderParams = _imageCompression.GetImageCompressionParams(60L);
            var encoder = _imageCompression.GetImageCodec(imageFormat);

            image.Save(Path.Combine(directory.Path, filename), encoder, encoderParams);
            
            return new ImageDetails() 
                            { 
                                Name = filename 
                            };
        }

        public void Delete(ImageDetails imageDetails)
        {
            throw new NotImplementedException();
        }
    }
}
