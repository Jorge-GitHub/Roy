using Roy.Domain.Attributes;

namespace Roy.Logging.Aspect.Email;

/// <summary>
/// Email body decorator.
/// </summary>
internal class Decorator
{ 
    public string GenerateBody(string body, MessageDetail bodyDetail)
    {
        return body;
    }
}