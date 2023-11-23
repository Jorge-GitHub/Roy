using Avalon.Base.Extension.Types;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Logging.Domain.Program;

/// <summary>
/// Application details.
/// </summary>
public class Application
{
    /// <summary>
    /// Flag that indicates whether the current 
    /// HTTP request is in debug mode
    /// </summary>
    public bool IsDebuggingEnabled { get; set; }
    /// <summary>
    /// Physical file system of the current 
    /// executing server application's root directory.
    /// </summary>
    public string PhysicalApplicationPath { get; set; }
    /// <summary>
    /// Assembly that the current code is running from.
    /// </summary>
    public string AssemblyLocation { get; set; }
    /// <summary>
    /// Application friendly name.
    /// </summary>
    public string FriendlyName { get; set; }
    /// <summary>
    /// Flag that determinate whether the application is executing in full trust.
    /// </summary>
    public bool? IsFullyTrusted { get; set; }
    /// <summary>
    /// Flag that determinate whether the object failed to load.
    /// </summary>
    public bool FailedToLoad { get; set; }

    /// <summary>
    /// constructor.
    /// </summary>
    public Application() { }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    public Application(bool load)
    {
        this.loadObject(load);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    private void loadObject(bool load)
    {
        if (load)
        {
            try
            {
                this.AssemblyLocation = this.GetAssemblyLocation();
                this.IsDebuggingEnabled = Debugger.IsAttached;
                if (AppDomain.CurrentDomain.IsNotNull())
                {
                    this.PhysicalApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
                    this.FriendlyName = AppDomain.CurrentDomain.FriendlyName;
                    this.IsFullyTrusted = AppDomain.CurrentDomain.IsFullyTrusted;
                }
            }
            catch 
            {
                this.FailedToLoad = true;
            }
        }
    }

    // Get assembly location.
    private string GetAssemblyLocation()
    {
        string location = Assembly.GetExecutingAssembly().Location;

        return location.IsNotNullOrEmpty() ? 
            Path.GetDirectoryName(location) : string.Empty;
    }
}