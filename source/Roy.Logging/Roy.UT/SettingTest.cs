using Roy.Logging.Domain.Settings.Attributes;
using Roy.Logging.Domain.Settings.Database;
using Roy.Logging.Domain.Settings.Web.APIAspect;

namespace Roy.UT;

/// <summary>
/// Settings tests.
/// </summary>
[TestClass]
public class SettingTest
{
    /// <summary>
    /// Test that every list a setting has starts empty, never null.
    /// </summary>
    [TestMethod]
    public void TestListsStartEmpty()
    {
        IssueSetting issue = new IssueSetting();

        Assert.IsNotNull(issue.Emails);
        Assert.IsNotNull(issue.APIs);
        Assert.IsNotNull(issue.Databases);
        Assert.IsNotNull(issue.LevelsToSaveOnFile);
        Assert.IsNotNull(issue.LevelsToLogOnSystemEvent);
        Assert.IsNotNull(new DatabaseSetting().LevelsToSave);
        Assert.IsNotNull(new APISetting().LevelsToPost);
    }
}
