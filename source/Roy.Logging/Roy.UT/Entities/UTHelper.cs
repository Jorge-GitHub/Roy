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
    public UTSampleObject GetDefaultSamplleObject()
    {
        return new UTSampleObject()
        {
            Name = "Homer",
            Description = "Amazing Story to Log About.",
            LastName = "Pegasus",
            Id = "001"
        };
    }
}
