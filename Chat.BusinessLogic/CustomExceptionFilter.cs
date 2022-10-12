using Chat.BusinessLogic.Exceptions;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Chat.BusinessLogic
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var exceptionStack = context.Exception.StackTrace;
            var exceptionMessage = context.Exception.Message;
            var statusCode = 400;

            switch (true)
            {
                case { } when context.Exception is EntityNotFoundException:
                    {
                        statusCode = 404;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            context.Result = new JsonResult(exceptionMessage)
            {
                StatusCode = statusCode
            };

            _logger.LogError($"В методе {actionName} возникло исключение: \n{exceptionMessage} \n {exceptionStack}\n");
        }
    }
}
