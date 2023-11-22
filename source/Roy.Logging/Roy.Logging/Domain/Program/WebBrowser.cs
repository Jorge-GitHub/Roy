namespace Roy.Logging.Domain.Program;

/// <summary>
/// Web browser information
/// </summary>
public class WebBrowser
{
    /// <summary>
    /// Browser's ID.
    /// </summary>
    public string BrowserID { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports ActiveX controls.
    /// </summary>
    public bool SupportsActiveXControls { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports cookies.
    /// </summary>
    public bool SupportsCookies { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser is a search engine crawler.
    /// </summary>
    public bool IsASearchEngineWebCrawler { get; set; }
    /// <summary>
    /// Ecma script version.
    /// </summary>
    public string EcmaScriptVersion { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports HTML frames.
    /// </summary>
    public bool SupportsHTMLFrames { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser has a back button.
    /// </summary>
    public bool HasBackButton { get; set; }
    /// <summary>
    /// Type o input supported by the browser.
    /// </summary>
    public string InputType { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser is in a mobile device.
    /// </summary>
    public bool IsMobileDevice { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports Java applets.
    /// </summary>
    public bool SupportsJavaApplets { get; set; }
    /// <summary>
    /// JavaScript version.
    /// </summary>
    public string JavaScriptVersion { get; set; }
    /// <summary>
    /// Mobile device manufacturer name.
    /// </summary>
    public string MobileDeviceManufacturerName { get; set; }
    /// <summary>
    /// Mobile device model name.
    /// </summary>
    public string MobileDeviceModelName { get; set; }
    /// <summary>
    /// Microsoft Document Object Model version.
    /// </summary>
    public string MSDomVersion { get; set; }
    /// <summary>
    /// Platform name that the client uses.
    /// </summary>
    public string Platform { get; set; }
    /// <summary>
    /// Screen bit depth.
    /// </summary>
    public int ScreenBitDepth { get; set; }
    /// <summary>
    /// Screen characters height.
    /// </summary>
    public int ScreenCharactersHeight { get; set; }
    /// <summary>
    /// Screen character width.
    /// </summary>
    public int ScreenCharactersWidth { get; set; }
    /// <summary>
    /// Screen pixels height.
    /// </summary>
    public int ScreenPixelsHeight { get; set; }
    /// <summary>
    /// screen pixels width.
    /// </summary>
    public int ScreenPixelsWidth { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports callback scripts.
    /// </summary>
    public bool SupportsCallbackScripts { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports CSS.
    /// </summary>
    public bool SupportsCSS {  get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports empty string in the cookies.
    /// </summary>
    public bool SupportsEmptyStringInCookieValue { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports redirect with cookies.
    /// </summary>
    public bool SupportsRedirectWithCookie { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports AJAX.
    /// </summary>
    public bool SupportsAJAX { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports HTML Tables.
    /// </summary>
    public bool SupportsHTMLTables { get; set; }
    /// <summary>
    /// Browser type.
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Flag that indicates whether the browser supports VB scripts.
    /// </summary>
    public bool SupportsVBScript { get; set; }
    /// <summary>
    /// Browser's version.
    /// </summary>
    public string Version { get; set; }
    /// <summary>
    /// W3x Dom version.
    /// </summary>
    public string W3CDomVersion { get; set; }
    /// <summary>
    /// Win base machine type.
    /// </summary>
    /// <remarks>
    /// Can be a Win16 or a Win32 base machine.
    /// </remarks>
    public string WinBasedType { get; set; }
    /// <summary>
    /// Operating system.
    /// </summary>
    public string OperatingSystem { get; set; }
}