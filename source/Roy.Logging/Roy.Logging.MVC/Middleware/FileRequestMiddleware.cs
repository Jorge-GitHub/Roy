using Microsoft.AspNetCore.Http;
using Roy.Logging.Domain.Contants;

namespace Roy.Logging.MVC.Middleware;

/// <summary>
/// Log any missing files on the HTTP request.
/// </summary>
internal class FileRequestMiddleware
{
    private readonly int _notFoundId = 404;
    private readonly RequestDelegate _nextComponent;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="nextComponent">
    /// A function that can process an HTTP request.
    /// </param>
    public FileRequestMiddleware(RequestDelegate nextComponent)
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
    /// <remarks>
    /// A request is taken for a file when its path has an extension, so an API's
    /// own 404 answers are not logged as missing files.
    /// </remarks>
    public async Task InvokeAsync(HttpContext context)
    {
        await this._nextComponent(context);
        if (context.Response.StatusCode.Equals(this._notFoundId)
            && Path.HasExtension(context.Request.Path.Value))
        {
            await new Exception($"File not found for HTTP request: '{context.Request.Path}'. Please verify the file path and ensure the file exists.")
                .SaveAsync(Level.MissingFile);
        }
    }
}
