using System.Drawing;

namespace PaintingWithFire.Data.Services.Imaging
{
    public interface ImageTransformer
    {
        Image ResizeTo(Image image, int width, int height);
        Image ScaleTo(Image image, int width, int height);
    }
}
