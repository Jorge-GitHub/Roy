using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO.Program;
using Roy.Logging.Domain.Program;
using Roy.Logging.Extensions.DTO;
using Roy.Logging.Domain.Settings.Attributes;
using Roy.UT.Entities;
using System.Text.Json;

namespace Roy.UT;

/// <summary>
/// DTO tests.
/// </summary>
[TestClass]
public class DtoTest
{
    /// <summary>
    /// Test that the method's parameters reach the DTO an API receives.
    /// </summary>
    [TestMethod]
    public void TestMethodParametersReachTheDTO()
    {
        Method method = new Method()
        {
            CallerMethodName = "AmazingCode",
            Parameters = new List<Parameter>()
            {
                new Parameter("first", "1"),
                new Parameter("second", "2")
            }
        };

        MethodDTO dto = method.ToDTO();

        Assert.AreEqual("AmazingCode", dto.CallerMethodName);
        Assert.IsNotNull(dto.Parameters);
        Assert.AreEqual(2, dto.Parameters.Count);
        Assert.AreEqual("first", dto.Parameters[0].Name);
        Assert.AreEqual("2", dto.Parameters[1].Value);
    }

    /// <summary>
    /// Test that the DTO of a thrown exception at Debug level serializes, full exception included.
    /// </summary>
    [TestMethod]
    public void TestExceptionDtoSerializesAtDebug()
    {
        ExceptionDetail detail = new ExceptionDetail(new UTHelper().ThrowAndCatch(),
            Level.Debug, "id", string.Empty, null!, new InformationSetting(), null!, null!);

        string json = JsonSerializer.Serialize(detail.ToDTO());

        StringAssert.Contains(json, "\"ExceptionTrace\"");
        StringAssert.Contains(json, "ThrowAndCatch");
    }
}
