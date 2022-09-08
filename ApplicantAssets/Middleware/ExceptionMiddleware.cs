namespace ApplicantAssets.Api.Middleware;

using System.Net;
using System.Text.Json;
using ApplicantAssets.Api.Exceptions;
using Microsoft.Extensions.Logging;

/// <summary>
/// A middleware for handling any kind of server erros.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly IHostEnvironment env;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">A method to call next middleware.</param>
    /// <param name="logger">A logger.</param>
    /// <param name="env">A host environment.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        this.next = next;
        this.logger = logger;
        this.env = env;
    }

    /// <summary>
    /// Method to invoke in request pipeline.
    /// </summary>
    /// <param name="context">An http context.</param>
    /// <returns>A <see cref="Task"/> that represents an asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "The server error occured: {Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = this.env.IsDevelopment()
                ? new ServerError(ex.Message, context.Response.StatusCode, ex.StackTrace!)
                : new ServerError(
                    "Internal Serve Error. If error persists, please contant the support team",
                    context.Response.StatusCode);

            await context.Response.WriteAsJsonAsync(
                response,
                typeof(ServerError),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }
    }
}
