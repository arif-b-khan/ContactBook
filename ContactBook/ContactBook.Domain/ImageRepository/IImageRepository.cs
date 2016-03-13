using System.Collections.Generic;
using System.IO;

namespace ContactBook.Domain.ImageRepository
{
    public interface IImageRepository
    {
        void SaveAsync(string fileName, Stream stream);
        List<string> GetFileNames();
        ImageFileData GetFileData(string _fileName);
        string UserName { get; set; }
    }
}
