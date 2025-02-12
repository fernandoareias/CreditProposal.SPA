﻿using System;
using Atividade02.Core.Common.CQRS;
using Atividade02.Core.Common.Enums;
using System.Text.Json;

namespace Atividade04.BFF.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var result = JsonSerializer.Serialize(new BaseResponse<View>(System.Net.HttpStatusCode.InternalServerError, new List<ResponseErroView> { new ResponseErroView(EBaseErro.INTERNAL_SERVER_ERROR.ToString(), "INTERNAL_SERVER_ERROR", "Sorry, an internal server error occurred. Please try again later."
) }));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}

