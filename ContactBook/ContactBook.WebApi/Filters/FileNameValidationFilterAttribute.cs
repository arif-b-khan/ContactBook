using ContactBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace ContactBook.WebApi.Filters
{
    public class FileNameValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly string parameterName;
        public FileNameValidationFilterAttribute(string paramName)
        {
            parameterName = paramName;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            string _fileName = Convert.ToString(actionContext.ActionArguments[parameterName]);

            if (string.IsNullOrEmpty(_fileName))
            {
                actionContext.ModelState.AddModelError(parameterName, "File name cannot be empty");
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "File name cannot be empty");
            }

            List<string> extensions = new List<string>();
            LoadFileExtensions(extensions);
            string fileExtension = Path.GetExtension(_fileName);

            if (fileExtension == null)
            {
                actionContext.ModelState.AddModelError(parameterName, "No file extension.");
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Not a valid file extension");
            }

            if(!extensions.Contains(fileExtension))
            {
                actionContext.ModelState.AddModelError(parameterName, "File extension not supported. Valid file extension are: " + string.Join(",",extensions.ToArray()));
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Not a valid file extension");
            }
        }

        public static void LoadFileExtensions(List<string> fileExtensions)
        {
            string[] listExtensions = ConfigurationManager.AppSettings[ContactBookConstants.AppSettings_FileUploadExtensions].Split(',');
            foreach (string extensions in listExtensions)
            {
                fileExtensions.Add(extensions);
            }
        }
    }
}