using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;

namespace VoteWave.Infrastructure.Logging;

public class RequestLoggingMiddleware() : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        Log.Information("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

        await next(context);

        stopwatch.Stop();
        Log.Information("Completed {Method} {Path} with status code {StatusCode} in {ElapsedMilliseconds} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
