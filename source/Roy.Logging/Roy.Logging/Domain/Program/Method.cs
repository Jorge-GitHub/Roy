using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using System.Diagnostics;
using System.Reflection;

namespace Roy.Logging.Domain.Program;

/// <summary>
/// Stack frame making the call to the method.
/// </summary>
public class Method
{
    /// <summary>
    /// File name containing the code being executed. This
    /// information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public string? CallerFileName { get; set; }
    /// <summary>
    /// Method name making the call.
    /// </summary>
    public string? CallerMethodName { get; set;}
    /// <summary>
    /// Line number in the file containing the code being executed.
    /// This information is normally extracted from the debugging symbols
    /// for the executable.
    /// </summary>
    public int CallerLineNumber { get; set; }
    /// <summary>
    /// Parameters.
    /// </summary>
    public List<Parameter> Parameters { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Method() { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="frame">
    /// Stack frame containing the caller member information.
    /// </param>
    public Method(StackFrame frame) 
    {
        this.LoadObject(frame);
    }

    /// <summary>
    /// Load object.
    /// </summary>
    /// <param name="frame">
    /// Stack frame containing the caller member information.
    /// </param>
    private void LoadObject(StackFrame frame)
    {
        if(frame.IsNotNull())
        {
            try
            {
                this.LoadMethodInformation(frame.GetMethod());
                this.CallerFileName = frame.GetFileName();
                this.CallerLineNumber = frame.GetFileLineNumber();
            }
            catch { }
        }
    }

    /// <summary>
    /// Load the method information.
    /// </summary>
    /// <param name="method">
    /// Method information.
    /// </param>
    private void LoadMethodInformation(MethodBase method)
    {
        if (method != null)
        {
            this.CallerMethodName = method.Name;
            try
            {
                this.LoadParametersInformation(method);
            }
            catch { }
        }
    }

    /// <summary>
    /// Load the parameters information.
    /// </summary>
    private void LoadParametersInformation(MethodBase method)
    {
        this.Parameters = new List<Parameter>();
        ParameterInfo[] parameterInfos = method.GetParameters();
        parameterInfos.Span(parameter =>
        {
            if (parameter.Name.IsNotNullOrEmpty()) {
                this.Parameters.Add(new Parameter(
                    parameter.Name,
                    parameter.ToJSON()));
            }
        });
    }
}