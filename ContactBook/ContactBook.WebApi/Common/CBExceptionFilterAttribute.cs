using ContactBook.Domain.Common.Logging;
using ContactBook.Domain.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using ContactBook.WebApi.App_Start;

namespace ContactBook.WebApi.Common
{
    public class CBExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly ICBLogger logger = DependencyFactory.Resolve<ICBLogger>();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string message = string.Format("Controller: {0}, Action: {1}",actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            logger.Error(message, actionExecutedContext.Exception);    
            
            base.OnException(actionExecutedContext);
        }
    }
}