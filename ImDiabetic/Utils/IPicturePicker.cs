using System;
using System.IO;
using System.Threading.Tasks;

namespace ImDiabetic.Utils
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
