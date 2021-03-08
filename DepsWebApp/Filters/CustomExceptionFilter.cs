using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace DepsWebApp.Filters
{
#pragma warning disable CS1591 
    public class CustomExceptionFilter:ExceptionFilterAttribute,IExceptionFilter
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var occurredError = ErrorConstructor(context.Exception);
            context.Result = new JsonResult(occurredError);
            return Task.CompletedTask;
        }
        
        private static ResponseError ErrorConstructor(Exception exception)
        {
            return (exception.GetType().ToString()) switch
            {
                "System.InvalidOperationException" => new ResponseError
                {
                    Code = 102,
                    Message = exception.Message
                },
                "System.NullReferenceException" => new ResponseError
                {
                    Code = 103,
                    Message = exception.Message
                },
                "System.ArgumentNullException" => new ResponseError
                {
                    Code = 104,
                    Message = exception.Message
                },
                "System.ArgumentOutOfRangeException" => new ResponseError
                {
                    Code = 105,
                    Message = exception.Message
                },
                "System.NotImplementedException" => new ResponseError
                {
                    Code = 228,
                    Message = exception.Message
                },
                _ => new ResponseError
                {
                    Code = 999,
                    Message = "Smth went wrong\n" +
                  $"[CHECK PLEASE THIS MSG]:\n{exception.Message}"
                },
            };
        }

    }
#pragma warning restore CS1591 
}
