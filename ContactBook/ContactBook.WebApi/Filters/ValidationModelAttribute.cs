using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactBook.WebApi.Filters
{
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                //filterContext.Controller.ViewData.ModelState.AddModelError(
            }
        }
    }
}