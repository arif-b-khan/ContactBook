using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using ContactBook.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.WebApi.Filters
{
    public class BookIdValidationFilterAttribute : ActionFilterAttribute
    {
        string parameterName;
        const string InvalidBookIdMessage = "Invalid bookId. Value of bookid must be a valid number";
        public BookIdValidationFilterAttribute(string bookId)
        {
            this.parameterName = bookId;
        }
        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            long bookId;
            string bookIdStr = Convert.ToString(actionContext.ActionArguments[parameterName]);

            if (string.IsNullOrEmpty(bookIdStr))
            {
                actionContext.ModelState.AddModelError(parameterName, InvalidBookIdMessage);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            if (long.TryParse(bookIdStr, out bookId))
            {
                var validate = new ValidateBookIdAttribute();

                ValidationResult result = validate.GetValidationResult(bookId, new ValidationContext(bookId));
                if (result != ValidationResult.Success)
                {
                    actionContext.ModelState.AddModelError(parameterName, result.ErrorMessage);
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                }
            }
            else
            {
                actionContext.ModelState.AddModelError(parameterName, InvalidBookIdMessage);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
        
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

    }
}