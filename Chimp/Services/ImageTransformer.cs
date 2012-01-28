using System.Drawing;

namespace Chimp.Services
{
    public interface ImageTransformer
    {
        Image ResizeTo(Image image, int width, int height);
        Image ScaleTo(Image image, int width, int height);
    }
}
