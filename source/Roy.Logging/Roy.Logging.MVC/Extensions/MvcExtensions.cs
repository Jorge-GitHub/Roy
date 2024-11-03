using Avalon.Base.Extension.Types;
using Microsoft.AspNetCore.Builder;
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
    public static void UseRoyExceptionHandler(this WebApplication app, 
        bool logMissingFiles = false)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context => {
                ErrorService.LogError(context);
            });
        });

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
        builder.LoadRoySettings();
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