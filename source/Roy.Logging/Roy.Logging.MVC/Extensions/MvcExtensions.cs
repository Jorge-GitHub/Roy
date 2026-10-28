using Avalon.Base.Extensions.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Roy.Logging.Domain.Settings;
using Roy.Logging.MVC.Middleware;

namespace Roy.Logging.MVC.Extensions;

/// <summary>
/// Roy logging MVC extensions.
/// </summary>
public static class MvcExtensions
{
    /// <summary>
    /// Load settings from the configuration file.
    /// </summary>
    /// <param name="builder">
    /// WebApplication builder.
    /// </param>
    public static void LoadRoySettings(this WebApplicationBuilder builder)
    {
        if (builder.IsNotNull())
        {
            try
            {
                RoySetting settings = builder.Configuration.GetSection("RoyLogging")
                    .Get<RoySetting>();
                if (settings.IsNotNull())
                {
                    LogExtension.Settings = settings;
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// Load Roy logging on errors and log all the errors not managed by the code.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="builder">
    /// A builder for web applications and services.
    /// </param>
    /// <param name="logMissingFiles">
    /// Flag that determinate whether to log missing files or not.
    /// </param>
    /// <remarks>
    /// We will use the default settings if we fail to load the 
    /// settings from the configuration file.
    /// Call it after the application's own error handling (UseExceptionHandler,
    /// UseDeveloperExceptionPage) and before app.Run(): Roy logs the exception and
    /// throws it again, so that handling still runs.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app,
        WebApplicationBuilder builder, bool logMissingFiles = false)
    {
        builder.LoadRoySettings();
        app.UseRoyExceptionHandler(logMissingFiles);
    }

    /// <summary>
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="settings">
    /// Settings.
    /// </param>
    /// <param name="logMissingFiles">
    /// Flag that determinate whether to log missing files or not.
    /// </param>
    /// <remarks>
    /// We will use the default settings if the settings parameter is null.
    /// Call it after the application's own error handling (UseExceptionHandler,
    /// UseDeveloperExceptionPage) and before app.Run(): Roy logs the exception and
    /// throws it again, so that handling still runs.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app, 
        RoySetting settings, bool logMissingFiles = false)
    {
        if (settings.IsNotNull())
        {
            LogExtension.Settings = settings;
        }
        app.UseRoyExceptionHandler(logMissingFiles);
    }

    /// <summary>
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="logMissingFiles">
    /// Flag that determinate whether to log missing files or not.
    /// </param>
    /// <remarks>
    /// Call it after the application's own error handling (UseExceptionHandler,
    /// UseDeveloperExceptionPage) and before app.Run(): Roy logs the exception and
    /// throws it again, so that handling still runs. Called before it, Roy would
    /// never see the exception: the handler nearer the page catches it first.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app, 
        bool logMissingFiles = false)
    {
        app.UseMiddleware<RoyExceptionMiddleware>();

        if(logMissingFiles){
            app.UseRoyToLogMissingFiles();
        }
    }

    /// <summary>
    /// Log missing files.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="builder">
    /// A builder for web applications and services.
    /// </param>
    public static void UseRoyToLogMissingFiles(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseRoyToLogMissingFiles();
    }

    /// <summary>
    /// Log missing files.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    public static void UseRoyToLogMissingFiles(this WebApplication app)
    {
        app.UseMiddleware<FileRequestMiddleware>();
    }
}
