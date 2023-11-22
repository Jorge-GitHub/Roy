using System.Numerics;

namespace Roy.Logging.Domain.Program;

/// <summary>
/// Web application details.
/// </summary>
public class WebApplication : Application
{
    /// <summary>
    /// Current URL.
    /// </summary>
    public string CurrentURL { get; set; }
    /// <summary>
    /// Current page parameters.
    /// </summary>
    public string CurentURLParameters { get; set; }
    /// <summary>
    /// Previous URL.
    /// </summary>
    public string PreviousURL {  get; set; }
    /// <summary>
    /// Previous page parameters.
    /// </summary>
    public string PreviousURLParameters { get; set; }
    /// <summary>
    /// User's DNS.
    /// </summary>
    public string UserDNS { get; set; }
    /// <summary>
    /// User's Host's IP.
    /// </summary>
    public string UserHostIP { get; set; }
    /// <summary>
    /// User preference languages.
    /// </summary>
    public string UserLanguagePreferences { get; set; }
    /// <summary>
    /// Flag that indicates whether custom errors are 
    /// enabled for the current HTTP request.
    /// </summary>
    public bool IsCustomErrorEnabled { get; set; }
    /// <summary>
    /// Server's name.
    /// </summary>
    public string ServerName { get; set; }
    /// <summary>
    /// Flag that indicates whether the application is running in a secure connection.
    /// </summary>
    public bool IsSecureConnection { get; set; }
    /// <summary>
    /// Domain's name.
    /// </summary>
    public string DomainName { get; set; }
    /// <summary>
    /// Cookies values.
    /// </summary>
    public string CookiesValues { get; set; }
    /// <summary>
    /// Browser's information.
    /// </summary>
    public WebBrowser WebBrowserInformation { get; set; }

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
    /// <param name="loadBrowserInformation">
    /// Flag that indicates whether to load the browser information or not.
    /// </param>
    public WebApplication(bool load, bool loadBrowserInformation) : base(load)
    {
        this.loadObject(load, loadBrowserInformation);
    }

    /// <summary>
    /// Loads the object.
    /// </summary>
    /// <param name="load">
    /// Flag that indicates whether to load/build the object or not.
    /// </param>
    private void loadObject(bool load, bool loadBrowserInformation)
    {
        if (load)
        {
            try
            {
                if(!this.FailedToLoad)
                {

                }
                if(loadBrowserInformation)
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
