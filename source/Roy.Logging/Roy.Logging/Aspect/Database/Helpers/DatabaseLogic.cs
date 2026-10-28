using Avalon.Base.Extensions.Collections;
using Avalon.Base.Extensions.Data;
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
    public async Task ExecuteQueryAsync(string stringConnection, string query,
        List<SqlParameter> parameters)
    {
        parameters.ToSafeSQL();
        using (SqlConnection connection = new SqlConnection(stringConnection))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters.HasElements())
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}
