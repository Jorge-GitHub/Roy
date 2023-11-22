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
    /// Flag that determinate whether the object failed to load.
    /// </summary>
    public bool FailedToLoad { get; set; }

    /// <summary>
    /// constructor.
    /// </summary>
    public Application()
    {
    }

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

            }
            catch 
            {
                this.FailedToLoad = true;
            }
        }
    }
}