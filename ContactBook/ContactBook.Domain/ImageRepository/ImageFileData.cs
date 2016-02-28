using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.ImageRepository
{
    public class ImageFileData
    {
        public string FilePath { get; set; }
        public byte[] FileData { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
    }
}
