using System.Drawing;
using System.IO;
using Chimp.Configuration;
using Chimp.Models;

namespace Chimp
{
    public interface ImagePersistor
    {
        Image ImageFrom(Stream stream);
        ImageDetails Save(Image image,string filename, DirectoryConfig directory);
        void Delete(string filename, DirectoryConfig directory);
    }
}
