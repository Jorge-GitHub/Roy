using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Data;
using Microsoft.Data.SqlClient;

namespace Roy.Logging.Aspect.Database.Helpers;

/// <summary>
/// Database logic.
/// </summary>
internal class DatabaseLogic
{
    /// <summary>
    /// Runs a store procedure and returns the number 
    /// of rows affected.
    /// </summary>
    /// <param name="stringConnection">
    /// Database's string connection.
    /// </param>
    /// <param name="query">
    /// SQL query to run.
    /// </param>
    /// <param name="parameters">
    /// List of SQL parameters.
    /// </param>
    public void ExecuteQuery(string stringConnection, string query,
        List<SqlParameter> parameters)
    {
        SqlConnection connection = null;
        try
        {
            parameters.ToSafeSQL();
            connection = new SqlConnection(stringConnection);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters.HasElements())
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
            command.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            if (connection != null)
            {
                connection.Dispose();
                connection.Close();
            }
        }
    }
}