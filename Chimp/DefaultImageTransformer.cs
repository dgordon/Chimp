using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Chimp.Services;

namespace Chimp
{
    public class WebImageTransformer:ImageTransformer
    {
        public Image ResizeTo(Image image, int width, int height)
        {
            //original deminsions
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;

            //new deminsions
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //calculate the percentage 
            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(image.HorizontalResolution,
                             image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                   InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(image,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public Image ScaleTo(Image image, int width, int height)
        {
            var ratio = Math.Min((float)width / (float)image.Width, (float)height / (float)image.Height);
            float destWidth = ratio * image.Width;
            float destHeight = ratio * image.Height;

            Bitmap bmPhoto = 
                new Bitmap((int)destWidth, (int)destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(
                image.HorizontalResolution, image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(image, 0, 0, destWidth, destHeight);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
