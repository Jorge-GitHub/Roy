using Roy.Logging.Domain.Program;

namespace Roy.Logging.MVC.Domain.Program;

/// <summary>
/// MVC web application details.
/// </summary>
public class MVCWebApplication : WebApplication
{
    /// <summary>
    /// constructor.
    /// </summary>
    public MVCWebApplication()
    {
    }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    /// <param name="loadBrowserInformation">
    /// Flag that indicates whether to load the browser information or not.
    /// </param>
    public MVCWebApplication(bool load, bool loadBrowserInformation) 
        : base(load, loadBrowserInformation)
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
                if (!this.FailedToLoad)
                {

                }
            }
            catch
            {
                this.FailedToLoad = true;
            }
        }
    }
}
