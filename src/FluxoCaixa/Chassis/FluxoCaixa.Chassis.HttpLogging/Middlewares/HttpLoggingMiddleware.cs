using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Logging.Models;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;

namespace FluxoCaixa.Chassis.HttpLogging.Middlewares
{
    public class HttpLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpLogger _logger;
        private readonly string _app;

        public HttpLoggingMiddleware(RequestDelegate next, IHttpLogger logger)
        {
            _next = next;
            _logger = logger;
            _app = GetServiceName();
        }

        public async Task Invoke(HttpContext context)
        {
            var log = await LogRequestAsync(context);
            log = await LogResponseAsync(log, context);
            _logger.Log(log);
        }

        private async Task<HttpLog> LogRequestAsync(HttpContext context)
        {
            var body = string.Empty;
            var method = context.Request.Method;

            if (AcceptRequestBody(method))
            {
                context.Request.EnableBuffering();
                body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            return new HttpLog
            {
                Application = _app,
                RequestUrl = $"{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}",
                RequestHeaders = JsonSerializer.Serialize(context.Request.Headers),
                RequestMethod = method,
                RequestBody = body
            };
        }

        private async Task<HttpLog> LogResponseAsync(HttpLog log, HttpContext context)
        {
            var originalBody = context.Response.Body;
            await using var newBody = new MemoryStream();
            context.Response.Body = newBody;
            await _next(context);

            string body = null;
            var statusCode = context.Response.StatusCode;
            if (AcceptResponseBody(statusCode))
            {
                newBody.Seek(0, SeekOrigin.Begin);
                body = await new StreamReader(context.Response.Body).ReadToEndAsync();
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);
            }

            log.ResponseBody = body;
            log.ResponseDate = DateTime.UtcNow;
            log.StatusCode = context.Response.StatusCode;
            return log;
        }

        private bool AcceptRequestBody(string method)
        {
            method = method.ToLower();
            return method is "post" or "put" or "patch";
        }

        private bool AcceptResponseBody(int statusCode)
        {
            return statusCode != 204;
        }

        private string GetServiceName()
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            return assemblyName?.Split(".").Last();
        }
    }
}
