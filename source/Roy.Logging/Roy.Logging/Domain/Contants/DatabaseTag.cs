namespace Roy.Logging.Domain.Contants;

/// <summary>
/// Database tags.
/// </summary>
internal struct DatabaseTag
{
    /// <summary>
    /// Tag for replacing the database's name on the query template.
    /// </summary>
    public const string DatabaseName = "*|DatabaseName|*";
    /// <summary>
    /// Tag for replacing the table's name on the query template.
    /// </summary>
    public const string TableName = "*|TableName|*";
    /// <summary>
    /// Default exception table's name.
    /// </summary>
    public const string DefaultExceptionTableName = "RoyException";
    /// <summary>
    /// Default log table's name.
    /// </summary>
    public const string DefaultLogTableName = "RoyLog";
}