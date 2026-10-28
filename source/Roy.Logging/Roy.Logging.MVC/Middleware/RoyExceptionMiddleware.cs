using Microsoft.AspNetCore.Http;
using System.Runtime.ExceptionServices;

namespace Roy.Logging.MVC.Middleware;

/// <summary>
/// Log any exception the request pipeline throws, then throw it again so whatever
/// handles it next (the application's own error page, or the server) still runs.
/// </summary>
internal class RoyExceptionMiddleware
{
    private readonly RequestDelegate _nextComponent;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="nextComponent">
    /// A function that can process an HTTP request.
    /// </param>
    public RoyExceptionMiddleware(RequestDelegate nextComponent)
    {
        this._nextComponent = nextComponent;
    }

    /// <summary>
    /// Invoke the pipeline component.
    /// </summary>
    /// <param name="context">
    /// The <see cref="HttpContext"/> for the request.
    /// </param>
    /// <returns>
    /// The current task
    /// </returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._nextComponent(context);
        }
        catch (Exception exception)
        {
            await ErrorService.LogErrorAsync(context, exception);
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}
