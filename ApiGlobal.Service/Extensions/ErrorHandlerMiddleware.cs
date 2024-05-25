using ApiGlobal.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Extensions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var result = new ResponseData()
                {
                    Data = null,
                    Message = error.Message,
                    StatusCode = (HttpStatusCode)(error.Data["StatusCode"] ?? HttpStatusCode.BadGateway),
                    Success = false
                };

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                response.StatusCode = (int)result.StatusCode;
                await response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        }
    }
}
