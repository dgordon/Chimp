using System.Drawing;
using System.IO;
using Chimp.Configuration;
using Chimp.Models;

namespace Chimp
{
    public interface ImagePersistor
    {
        Image ImageFrom(Stream stream);
        ImageDetails Save(Image image,string filename, DirectoryConfiguration directory);
        void Delete(ImageDetails imageDetails);
    }
}
