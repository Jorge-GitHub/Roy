namespace Roy.Logging.Domain.Application;

/// <summary>
/// Parameter information.
/// </summary>
public class Parameter
{
    public string Name { get; set; }
    public string Content { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">
    /// Parameter's name.
    /// </param>
    /// <param name="content">
    /// Parameter's content.
    /// </param>
    public Parameter(string name, string content)
    {
        this.Name = name;
        this.Content = content;
    }
}