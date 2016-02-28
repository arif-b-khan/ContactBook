using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ContactBook.Domain.ImageRepository
{
    public class FileImageRepository : IImageRepository
    {
        private string userName;
        public FileImageRepository(string imagePath, string avatarPath)
        {
            if (string.IsNullOrEmpty(imagePath) &&
                string.IsNullOrEmpty(avatarPath))
            {
                throw new ArgumentNullException("One of the parameters pass is null");
            }

            ImagePath = imagePath;
            AvatarPath = avatarPath;
        }

        private void SetFolderList()
        {
            FolderList = new List<String>()
            {
                AvatarPath,
                ImagePath
            };
        }

        private string ImagePath { get; set; }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                ImagePath = Path.Combine(ImagePath, userName);
                SetFolderList();
            }
        }

        private string AvatarPath { get; set; }

        private List<string> FolderList { get; set; }

        public async void SaveAsync(string fileName, Stream stream)
        {
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }

            using (var fileStream = new FileStream(ImagePath + fileName, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        public List<string> GetFileNames()
        {
            var fileNames = new List<string>();

            foreach (string folderPath in FolderList)
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    fileNames.Add(Path.GetFileName(file));
                }
            }

            return fileNames;
        }

        public ImageFileData GetFileData(string fileName)
        {
            var retImageFileData = new ImageFileData();
            var filePath = FolderList.Select(f => Path.Combine(f, fileName)).SingleOrDefault(pth =>
            {
                return File.Exists(pth);
            });
            retImageFileData.FilePath = filePath;
            retImageFileData.FileData = File.ReadAllBytes(filePath);
            retImageFileData.FileExtension = Path.GetExtension(filePath);
            retImageFileData.FileName = fileName;

            return retImageFileData;
        }
    }
}
