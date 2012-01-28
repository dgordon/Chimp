using System.Drawing;
using System.IO;
using Chimp.Configuration;
using Chimp.Models;

namespace Chimp
{
    public abstract class ImagePersistor
    {
        public abstract Image ImageFrom(Stream stream);

        public abstract ImageDetails Save(Image image, DirectoryConfiguration directory);
        public abstract void Delete(string filename);
    }
}
