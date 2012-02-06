using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Chimp.Services
{
    public class DefaultImageTransformer:ImageTransformer
    {
        public Image ConstrainedScale(Image image, int width, int height)
        {
            return this.ConstrainedScale(image, width, height, Color.White);
        }
        public Image ConstrainedScale(Image image, int width, int height, Color canvasColor)
        {
            //original deminsions
            var sourceWidth = image.Width;
            var sourceHeight = image.Height;
            var sourceX = 0;
            var sourceY = 0;

            //new deminsions
            var destX = 0;
            var destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //calculate the percentage 
            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((height -
                              (sourceHeight * nPercent)) / 2);
            }

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(width, height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(image.HorizontalResolution,
                             image.VerticalResolution);

            using (var grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.Clear(canvasColor);
                grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(image,
                                  new Rectangle(destX, destY, destWidth, destHeight),
                                  new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                grPhoto.Dispose();
            }
            return bmPhoto;
        }

        public Image Resize(Image image, Size size)
        {
            var ratio = Math.Min((float)size.Width / (float)image.Width, (float)size.Height / (float)image.Height);
            var destWidth = ratio * image.Width;
            var destHeight = ratio * image.Height;

            var bmPhoto = new Bitmap((int)destWidth, (int)destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(image, 0, 0, destWidth, destHeight);
                grPhoto.Dispose();
            }
            return bmPhoto;
        }
        public Image Crop(Image image, int x, int y, Size size)
        {
            var cropArea = new Rectangle(x,y, size.Width, size.Height);
            var bmpImage = new Bitmap(image);
            return bmpImage.Clone(cropArea,bmpImage.PixelFormat);
        }
    }
}
