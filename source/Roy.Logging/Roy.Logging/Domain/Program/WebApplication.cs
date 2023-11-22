namespace Roy.Logging.Domain.Program;

/// <summary>
/// Web application details.
/// </summary>
public class WebApplication : Application
{
    /// <summary>
    /// constructor.
    /// </summary>
    public WebApplication()
    {
    }

    /// <summary>
    /// Constructor that builds the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    public WebApplication(bool load) : base(load)
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
                if(!this.FailedToLoad)
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
