using System.Linq;
using System.Drawing.Imaging;

namespace Chimp.Services
{
    //http://msdn.microsoft.com/en-us/library/bb882583.aspx#Y296
    public class DefaultImageCompressor : ImageCompression
    {
        public DefaultImageCompressor(long compression, ImageFormat imageFormat)
            :base(compression, imageFormat){}
    }
}
