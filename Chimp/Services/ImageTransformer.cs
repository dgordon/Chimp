using System.Drawing;

namespace Chimp.Services
{
    public interface ImageTransformer
    {
        Image ResizeTo(Image image, int width, int height);
        Image ScaleTo(Image image, int width, int height);
        Image Crop(Image image, int x, int y, int width, int height);
    }
}
