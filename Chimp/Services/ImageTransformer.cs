using System.Drawing;

namespace Chimp.Services
{
    public interface ImageTransformer
    {
        Image ConstrainedScale(Image image, int width, int height);
        Image ConstrainedScale(Image image, int width, int height, Color canvasColor);
        Image ProportionalScale(Image image, Size size);
        Image ProportionalScale(Image image, Size size, Color canvasColor);
        Image Crop(Image image, int x, int y, Size size);
    }
}
