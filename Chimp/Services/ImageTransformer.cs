using System.Drawing;

namespace Chimp.Services
{
    public interface ImageTransformer
    {
        Image ConstrainedScale(Image image, int width, int height);
        Image ConstrainedScale(Image image, int width, int height, Color canvasColor);
        Image ProportionalScale(Image image, int width, int height);
        Image ProportionalScale(Image image, int width, int height, Color canvasColor);
        //Image Crop(Image image, int x, int y, int width, int height);
    }
}
