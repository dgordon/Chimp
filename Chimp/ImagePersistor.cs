using System.Drawing;
using System.IO;


namespace PaintingWithFire.Data.Services.Imaging
{
    public interface ImagePersistor
    {
        Image ImageFrom(Stream stream);

        void SavePicture(Image image);
        void DeletePicture(string filename);
    }
}
