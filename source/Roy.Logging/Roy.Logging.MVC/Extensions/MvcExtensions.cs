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
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="builder">
    /// WebApplication builder.
    /// </param>
    /// <param name="loadSettings">
    /// Flag that determinate whether to load or  not the settings.
    /// </param>
    /// <param name="logRequestInformation">
    /// Optional: Flag that determinate whether to log the request information, 
    /// such as browser and user information.
    /// </param>
    /// <remarks>
    /// We will use the default settings if we fail to load the 
    /// settings from the configuration file.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app,
        WebApplicationBuilder builder, bool loadSettings,
        bool logRequestInformation = false)
    {
        RoySetting settings = null;
        if (loadSettings && builder.IsNotNull())
        {
            try
            {
                settings = builder.Configuration.GetSection("RoyLogging")
                    .Get<RoySetting>();
            }
            catch { }
        }
        app.UseRoyExceptionHandler(settings, logRequestInformation);
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
    /// <param name="logRequestInformation">
    /// Optional: Flag that determinate whether to log the request information, 
    /// such as browser and user information.
    /// </param>
    /// <remarks>
    /// We will use the default settings if the settings parameter is null.
    /// </remarks>
    public static void UseRoyExceptionHandler(this WebApplication app, 
        RoySetting settings, bool logRequestInformation = false)
    {
        if (settings.IsNotNull())
        {
            LogExtension.Settings = settings;
        }
        app.UseRoyExceptionHandler(logRequestInformation);
    }

    /// <summary>
    /// Load Roy logging on errors.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    /// <param name="logRequestInformation">
    /// Optional: Flag that determinate whether to log the request information, 
    /// such as browser and user information.
    /// </param>
    public static void UseRoyExceptionHandler(this WebApplication app, 
        bool logRequestInformation = false)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context => {
                ErrorService.LogError(context, logRequestInformation);
            });
        });
    }
}