using Avalon.Base.Extension.Types;
using Roy.Domain.Contants;
using System.Reflection;
using System.Text.Json;

namespace Roy.Logging.Helpers;

/// <summary>
/// File's service.
/// </summary>
internal class FileService
{
    /// <summary>
    /// Save the exception.
    /// </summary>
    /// <param name="exceptionDetail">
    /// Exception's detail.
    /// </param>
    /// <param name="logId">
    /// Log's Id.
    /// </param>
    /// <param name="folderLocation">
    /// Folder location to save the exception.
    /// If null or empty, the folder to use will be the running 
    /// assembly location + exceptions.
    /// </param>
    /// <param name="fileName">
    /// File's name.
    /// If null or empty, each exception will have its own file.
    /// The file's name will be the exception's Id.
    /// </param>
    /// <param name="defaultFolderLocationName">
    /// Default folder location name to use in case the folder 
    /// location is null or empty.
    /// </param>
    /// <param name="level">
    /// Log's level
    /// </param>
    /// <param name="appendLog">
    /// Flag that determinate whether to append or not the log.
    /// </param>
    public async void SaveAsync<TValue>(TValue value,
        string logId, string folderLocation, string fileName,
        Level level, string defaultFolderLocationName,
        bool appendLog)
    {
        string fileLocation = this.GetFileLocation(
            folderLocation, fileName, logId,
            level, defaultFolderLocationName);
        string json = JsonSerializer.Serialize(value);
        this.LogTextAsync(json, fileLocation, appendLog);
    }

    /// <summary>
    /// Log the text.
    /// </summary>
    /// <param name="text">
    /// Text to log.
    /// </param>
    /// <param name="fileLocation">
    /// File location to log the text.
    /// </param>
    /// <param name="appendLog">
    /// Flag that determinate whether to append or not the log.
    /// </param>
    private async void LogTextAsync(string text,
        string fileLocation, bool appendLog)
    {
        if (appendLog)
        {
            File.AppendAllTextAsync(fileLocation, text);
        }
        else
        {
            File.WriteAllTextAsync(fileLocation, text);
        }
    }

    /// <summary>
    /// Get the file location to save the exception.
    /// </summary>
    /// <param name="folderLocation">
    /// Folder location to save the exception.
    /// If null or empty, the folder to use will be the running 
    /// assembly location + exceptions.
    /// </param>
    /// <param name="fileName">
    /// File name.
    /// If null or empty, each exception will have its own file.
    /// The file's name will be the exception's Id.
    /// </param>
    /// <param name="id">
    /// Id.
    /// </param>
    /// <param name="level">
    /// Exception's level
    /// </param>
    /// <param name="defaultFolderName">
    /// Default folder name to use in case the folder 
    /// location is null or empty. If folder location is null, it will save 
    /// the file within the assembly folder.
    /// </param>
    /// <returns>
    /// File location to save the exception.
    /// </returns>
    private string GetFileLocation(string folderLocation,
        string fileName, string id, Level level,
        string defaultFolderName)
    {
        if (folderLocation.IsNullOrEmpty())
        {
            folderLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            if (defaultFolderName.IsNotNullOrEmpty())
            {
                folderLocation = Path.Combine(folderLocation,
                    defaultFolderName);
            }
        }
        if (fileName.IsNullOrEmpty())
        {
            fileName = $"{level.ToString()}.{id}.txt";
        }
        this.SetFolder(folderLocation);

        return Path.Combine(folderLocation,
            fileName);
    }

    /// <summary>
    /// Set the folder in case it does not exists.
    /// </summary>
    /// <param name="folder">
    /// Folder location.
    /// </param>
    private void SetFolder(string folderLocation)
    {
        if (!Directory.Exists(folderLocation))
        {
            Directory.CreateDirectory(folderLocation);
        }
    }
}