using Microsoft.AspNetCore.Mvc.RazorPages;
using Roy.Logging;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Communication;
using Roy.Logging.Domain.Settings;
using Roy.Logging.Extensions.DTO;

namespace Roy.Mvc.UT.Pages;

public class IndexModel : PageModel
{
    public async void OnGet()
    {
        // Uncomment a line below for testing.
        ProcessMessageDTO message = await (new Author { Description = "Great singer", Name = "Roy" }).LogAsync();
        // FileStream file = new FileStream("filedoesnotexist.json", FileMode.Open);
        // TestPost();
    }

    public async void TestPost()
    {
        string saveExceptionURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/RoyTest/SaveException";
        string logURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/RoyTest/Log";

        HttpClient client = null;
        try
        {
            client = new HttpClient();

            ExceptionDetail exception = new ExceptionDetail( 
                new Exception(), Level.Alert, "0001", "Test error", null,
                LogExtension.Settings.Exception.LoadInformationSettings, null, null);

            LogDetail log = new LogDetail(
                new Author { Description = "Great singer", Name = "Roy" }, 
                Level.Alert, "0001", "Test error", null,
                LogExtension.Settings.Exception.LoadInformationSettings, null, null);

            var exceptionResult = await client.PostAsJsonAsync(saveExceptionURL, exception.ToDTO());
            var logResult = await client.PostAsJsonAsync(logURL, log.ToDTO());
        }
        catch (Exception ex) 
        { 
            string error = ex.Message;
        }
        finally
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
    }
}

/// <summary>
/// Test class. 
/// </summary>
public class Author
{
    public string Name { get; set; }
    public string Description { get; set; }
}