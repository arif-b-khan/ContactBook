using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Common.Uploader
{
    interface IImageUploader
    {
        List<string> GetPaths();
        bool DeleteImage(string fileName);
        void UploadImage();
        byte[] GetImage(string fileName, string userName);
    }
}
