namespace Roy.Logging.Domain.Program;

/// <summary>
/// Parameter information.
/// </summary>
public class Parameter
{
    /// <summary>
    /// Parameter's name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Parameter's value.
    /// </summary>
    public string Value { get; set; }

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
        this.Value = content;
    }
}