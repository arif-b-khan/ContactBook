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
using ContactBook.Domain.ImageRepository;

namespace ContactBook.WebApi.Controllers
{

    [Authorize]
    [RoutePrefix("api/ApiImages")]
    public class ApiImagesController : ApiController
    {
        private readonly IImageRepository _imageRepository;

        public ApiImagesController()
        {
            _imageRepository = new FileImageRepository(ConfigurationManager.AppSettings["UserImagePath"], ConfigurationManager.AppSettings["AvatarPath"]);
        }

        [HttpGet]
        [Route("GetImageFileNames")]
        public IHttpActionResult GetImageFileNames()
        {
            List<string> fileNames;

            try
            {
                _imageRepository.UserName = User.Identity.Name;
                fileNames = _imageRepository.GetFileNames();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok<List<string>>(fileNames);
        }

        // GET api/<controller>
        [HttpGet]
        [AllowAnonymous]
        [Route("{fileName}")]
        [FileNameValidationFilter("fileName")]
        public IHttpActionResult GetImage(string fileName, string userName)
        {
            _imageRepository.UserName = userName;
            return new ImageReponseMessage(_imageRepository, fileName);
        }

        [HttpPost]
        [Route("UploadImage")]
        public IHttpActionResult UploadImage()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                _imageRepository.UserName = User.Identity.Name;
                var fileExtensions = new List<string>();

                FileNameValidationFilterAttribute.LoadFileExtensions(fileExtensions);
                string imagePath = Path.Combine(ConfigurationManager.AppSettings["UserImagePath"], User.Identity.Name);

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
                    var savePath = Path.Combine(imagePath, httpPostedFile.FileName);
                    _imageRepository.SaveAsync(savePath, httpPostedFile.InputStream);
                }
                else
                {
                    ModelState.AddModelError("FileSize", "File size cannot be more than 1 mb");
                    return BadRequest(ModelState);
                }
            }
            return Ok();
        }


    }

    public class ImageReponseMessage : IHttpActionResult
    {
        private readonly IImageRepository _imageRepository;
        private string _fileName;

        public ImageReponseMessage(IImageRepository imageRepository, string fileName)
        {
            _imageRepository = imageRepository;
            _fileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            ImageFileData fileData = _imageRepository.GetFileData(_fileName);

            if (string.IsNullOrEmpty(fileData.FilePath))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string mediaType = "image/" + fileData.FileExtension;

            var taskMessage = Task<HttpResponseMessage>.Run(() =>
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                var memStream = new MemoryStream(fileData.FileData);
                responseMessage.Content = new StreamContent(memStream);
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                return responseMessage;
            });

            return taskMessage;
        }
    }

}