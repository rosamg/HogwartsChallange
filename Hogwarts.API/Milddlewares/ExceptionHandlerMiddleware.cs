using FluentValidation;
using Hogwarts.API.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hogwarts.API.Milddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.request = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception exception)
            {

                if (!(exception is ValidationException validationException))
                {
                    throw exception;
                }

                var errors = validationException.Errors.Select(err => new ErrorModelViewModel
                {
                    PropertyName = err.PropertyName,
                    Message = err.ErrorMessage
                });

                var errorText = JsonConvert.SerializeObject(errors);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(errorText, Encoding.UTF8);
            }
        }
    }
}