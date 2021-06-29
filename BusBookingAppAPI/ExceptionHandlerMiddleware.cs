using BusBooking.Data.Context;
using BusBooking.Data.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusBooking.Business.Authenticate;

namespace BusBooking.Business.Authenticate
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, EntityContext obj)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex, obj).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception, EntityContext obj)
        {
            Log data = new Log
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message,
                ErrorSource = exception.Source
            };
            obj.Log.Add(data);
            obj.SaveChanges();

            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = exception
                //ErrorMessage = exception.Message,
                //Source = exception.Source
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
