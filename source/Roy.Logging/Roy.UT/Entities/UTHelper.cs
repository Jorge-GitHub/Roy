using Roy.Logging.Domain.DTO.Communication;
using Roy.Logging.Domain.Settings;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Domain.Settings.Web.APIAspect;
using Roy.Logging.Domain.Settings.Web.EmailAspect;
using System.Text.Json;

namespace Roy.UT.Entities;

/// <summary>
/// UT helper.
/// </summary>
internal class UTHelper
{
    /// <summary>
    /// Folder for this test's files. Each test gets its own, deleted on cleanup.
    /// </summary>
    public string Root { get; private set; }
    /// <summary>
    /// Folder where this test's exceptions are saved.
    /// </summary>
    public string ExceptionsFolder => Path.Combine(this.Root, "exceptions");
    /// <summary>
    /// Folder where this test's logs are saved.
    /// </summary>
    public string LogsFolder => Path.Combine(this.Root, "logs");

    /// <summary>
    /// Constructor.
    /// </summary>
    public UTHelper()
    {
        this.Root = Path.Combine(Path.GetTempPath(), "Roy.UT",
            Guid.NewGuid().ToString("N"));
    }

    /// <summary>
    /// Reset the settings to the defaults and point the files at this test's folder,
    /// so no test depends on the one that ran before it.
    /// </summary>
    public void ResetSettings()
    {
        LogExtension.Settings = new RoySetting();
        LogExtension.Settings.Exception.FolderLocation = this.ExceptionsFolder;
        LogExtension.Settings.Log.FolderLocation = this.LogsFolder;
    }

    /// <summary>
    /// Delete this test's folder.
    /// </summary>
    public void DeleteFiles()
    {
        try
        {
            if (Directory.Exists(this.Root))
            {
                Directory.Delete(this.Root, true);
            }
        }
        catch { }
    }

    /// <summary>
    /// Assert that no sink reported an error, naming the first one if any did.
    /// </summary>
    /// <param name="result">
    /// Result returned by the logging service.
    /// </param>
    public void AssertNoErrors(ProcessMessageDTO result)
    {
        Assert.AreEqual(0, result.Errors.Count,
            result.Errors.FirstOrDefault()?.ToString() ?? string.Empty);
    }

    /// <summary>
    /// Get the one file a test wrote.
    /// </summary>
    /// <param name="folder">
    /// Folder to look in.
    /// </param>
    /// <returns>
    /// The file's location. Fails the test if there is not exactly one file.
    /// </returns>
    public string GetSingleFile(string folder)
    {
        Assert.IsTrue(Directory.Exists(folder), $"Folder not created: {folder}");
        string[] files = Directory.GetFiles(folder);
        Assert.AreEqual(1, files.Length, $"Expected one file in {folder}");

        return files[0];
    }

    /// <summary>
    /// Read a file written by Roy as JSON.
    /// </summary>
    /// <param name="fileLocation">
    /// File's location.
    /// </param>
    /// <returns>
    /// The JSON root element.
    /// </returns>
    public JsonElement ReadJson(string fileLocation)
    {
        using (JsonDocument document = JsonDocument.Parse(File.ReadAllText(fileLocation)))
        {
            return document.RootElement.Clone();
        }
    }

    /// <summary>
    /// Get the full path to the file.
    /// </summary>
    /// <param name="folder">
    /// Folder's location.
    /// </param>
    /// <param name="fileName">
    /// File's name.
    /// </param>
    /// <returns>
    /// File's location.
    /// </returns>
    public string GetfullPathToFile(string folder, string fileName)
    {
        return Path.Combine(folder, fileName);
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
    /// Get email settings that point at a server nothing listens on, so any attempt
    /// to send fails at once and shows up in the errors.
    /// </summary>
    /// <returns>
    /// Email settings.
    /// </returns>
    public EmailSetting GetOfflineEmailSetting()
    {
        EmailSetting settings = this.GetEmailSetting();
        settings.Server.Host = "127.0.0.1";
        settings.Server.Port = 1;
        settings.UserAccount = string.Empty;

        return settings;
    }

    /// <summary>
    /// Get database settings whose connection string cannot be parsed, so any attempt
    /// to save fails before any network and shows up in the errors.
    /// </summary>
    /// <returns>
    /// Database settings.
    /// </returns>
    public DatabaseSetting GetOfflineDatabaseSetting()
    {
        return new DatabaseSetting()
        {
            DatabaseName = "Roy",
            StringConnection = "not a connection string"
        };
    }

    /// <summary>
    /// Get API settings that point at a port nothing listens on, so the post is
    /// refused at once and shows up in the errors.
    /// </summary>
    /// <returns>
    /// API settings.
    /// </returns>
    public APISetting GetOfflineAPISetting()
    {
        return new APISetting() { URL = "http://127.0.0.1:1/" };
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
