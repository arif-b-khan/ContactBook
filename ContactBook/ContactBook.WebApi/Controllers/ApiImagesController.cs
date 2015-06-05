using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using ContactBook.WebApi.Filters;
using System.Configuration;
using System.Security.AccessControl;
using ContactBook.Domain.Common;

namespace ContactBook.WebApi.Controllers
{
    public class ImageReponseMessage : IHttpActionResult
    {
        string _fileName;
        List<string> rootPaths;

        public ImageReponseMessage(string fileName, string userName)
        {
            _fileName = fileName;
            rootPaths = new List<string>();
            rootPaths.Add(ConfigurationManager.AppSettings["AvatarPath"]);
            rootPaths.Add(Path.Combine(ConfigurationManager.AppSettings["UserImagePath"], userName));
        }

        public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var filePath = rootPaths.Select(f => Path.Combine(f, _fileName)).SingleOrDefault(pth =>
            {
                return File.Exists(pth);
            });

            if (filePath == null)
            {
                throw new HttpResponseException(HttpStatusCode.OK);
            }

            string fileExtension = Path.GetExtension(filePath);
            string mediaType = "image/" + fileExtension;

            var taskMessage = Task<HttpResponseMessage>.Run(() =>
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] fileData = File.ReadAllBytes(filePath);

                var memStream = new MemoryStream(fileData);
                responseMessage.Content = new StreamContent(memStream);
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                return responseMessage;
            });

            return taskMessage;
        }
    }

    [Authorize]
    [RoutePrefix("ApiImages")]
    public class ApiImagesController : ApiController
    {
        
        [HttpGet]
        [Route("GetImageFileNames")]
        public IHttpActionResult GetImageFileNames()
        {
            List<string> fileNames = new List<string>();

            var folderList = new List<String>()
            {
                ConfigurationManager.AppSettings["AvatarPath"],
                Path.Combine(ConfigurationManager.AppSettings["UserImagePath"], User.Identity.Name)
            };

            foreach (string folderPath in folderList)
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    fileNames.Add(Path.GetFileName(file));
                }
            }

            return Ok<List<string>>(fileNames);
        }

        // GET api/<controller>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetImage/{userName}/{fileName}")]
        public IHttpActionResult GetImage(string userName, string fileName)
        {
            return new ImageReponseMessage(fileName, userName);
        }

        [HttpPost]
        [Route("UploadImage")]
        public IHttpActionResult UploadImage()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string directoryPath = Path.Combine(ConfigurationManager.AppSettings["UserImagePath"], User.Identity.Name);
                var fileExtensions = new List<string>();
                
                LoadFileExtensions(fileExtensions);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                string fileExtension = Path.GetExtension(httpPostedFile.FileName);
                
                if (fileExtensions != null && !fileExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ExtensionSupport", "Extension not supported. Supported extensions are : " + string.Join(",", fileExtensions.ToArray()));
                    return BadRequest(ModelState);
                }

                int uploadFileSize;
                int.TryParse(ConfigurationManager.AppSettings["FileSize"], out uploadFileSize);

                if (httpPostedFile.ContentLength <= uploadFileSize)
                {
                    var savePath = Path.Combine(directoryPath, httpPostedFile.FileName);
                    httpPostedFile.SaveAs(savePath);
                }
                else
                {
                    ModelState.AddModelError("FileSize", "File size cannot be more than 1 mb");
                    return BadRequest(ModelState);
                }
            }
            return Ok();
        }

        private void LoadFileExtensions(List<string> fileExtensions)
        {
            string[] listExtensions = ConfigurationManager.AppSettings[ContactBookConstants.AppSettings_FileUploadExtensions].Split(',');
            foreach (string extensions in listExtensions)
            {
                fileExtensions.Add(extensions);
            }
        }
    }
}