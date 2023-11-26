using Roy.Logging.Domain.Settings.Web.APIAspect;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using System.Reflection;

namespace Roy.UT.Entities;

/// <summary>
/// UT helper.
/// </summary>
internal class UTHelper
{
    /// <summary>
    /// Get the full path to the file.
    /// </summary>
    /// <param name="DefaultFolderName">
    /// Default folder's name.
    /// </param>
    /// <param name="fileName">
    /// File's name.
    /// </param>
    /// <returns>
    /// File's location.
    /// </returns>
    public string GetfullPathToFile(string DefaultFolderName,
        string fileName)
    {
        string folderLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);
        folderLocation = Path.Combine(folderLocation,
            DefaultFolderName);

        return Path.Combine(folderLocation, fileName);
    }

    /// <summary>
    /// Get default UT sample object.
    /// </summary>
    /// <returns>
    /// UT sample object.
    /// </returns>
    public Artist GetDefaultSampleObject()
    {
        return new Artist()
        {
            Name = "Roy",
            LastName = "Orbinson",
            Description = "Roy Kelton Orbison was an American singer, songwriter, and musician.",            
            Id = "#1"
        };
    }

    /// <summary>
    /// Get the email settings.
    /// </summary>
    /// <returns>
    /// Email settings.
    /// </returns>
    public EmailSetting GetEmailSetting()
    {
        EmailSetting settings = new EmailSetting();
        settings.Server.Host = "smtp.ethereal.email";
        settings.From = "roy@yahoo.com";
        settings.DisplayNameFrom = "ROY Logger";
        settings.DefaultIsTextBody = false;
        settings.UserAccount = "shania60@ethereal.email";
        // This is a fake SMTP that is why I am hard coding this fake password.
        settings.UserPassword = "wK2jPUxzqdnsDBNCUj";
        ReceiverSetting receiver = new ReceiverSetting();
        receiver.To = "royorbinson@gmail.com";
        settings.Receivers.Add(receiver);

        return settings;
    }

    /// <summary>
    /// Get API settings.
    /// </summary>
    /// <returns>
    /// API settings.
    /// </returns>
    public List<APISetting> GetAPISetting()
    {
        List< APISetting> apis = new List<APISetting>();
        apis.Add(new APISetting() { URL = "https://api.restful-api.dev/objects" });
        apis.Add(new APISetting() { URL = "https://jsonplaceholder.typicode.com/posts" });

        return apis;
    }
}