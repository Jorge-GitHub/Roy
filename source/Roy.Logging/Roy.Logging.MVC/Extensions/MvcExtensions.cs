using Avalon.Base.Extension.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Roy.Logging.Domain.Settings;

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
    /// WebApplication builder.
    /// </param>
    /// <remarks>
    /// We will use the default settings if we fail to load the 
    /// settings from the configuration file.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app,
        WebApplicationBuilder builder)
    {
        builder.LoadRoySettings();
        app.UseRoyExceptionHandler();
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
    /// <remarks>
    /// We will use the default settings if the settings parameter is null.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app, 
        RoySetting settings)
    {
        if (settings.IsNotNull())
        {
            LogExtension.Settings = settings;
        }
        app.UseRoyExceptionHandler();
    }

    /// <summary>
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    public static void UseRoyExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context => {
                ErrorService.LogError(context);
            });
        });
    }
}