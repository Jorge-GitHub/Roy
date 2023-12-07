using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Communication;
using Roy.Logging.Domain.DTO.Communication;

namespace Roy.Logging.Extensions.Communication;

/// <summary>
/// ProcessMessage extensions.
/// </summary>
internal static class ProcessMessageExtensions
{
    /// <summary>
    /// Converts a ProcessMessage into a ProcessMessageDTO
    /// </summary>
    /// <param name="process">
    /// Message returned by the logging service.
    /// </param>
    /// <returns>
    /// ProcessMessageDTO object.
    /// </returns>
    public static ProcessMessageDTO ToDTO(this ProcessMessage process)
    {
        if(process.IsNotNull())
        {
            return new ProcessMessageDTO()
            {
                Errors = process.Errors
            };
        }

        return null;
    }
}