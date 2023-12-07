using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Roy.Logging.Domain.Attributes;
using Roy.Logging.Domain.Contants;
using Roy.Logging.Domain.DTO;
using Roy.Logging.Domain.DTO.Program;
using Roy.Logging.Domain.Program;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Roy.Logging.Extensions.DTO;

/// <summary>
/// Message detail DTO extensions.
/// </summary>
public static class MessageDetailDTOExtensions
{
    /// <summary>
    /// Converts a ExceptionDetail into a ExceptionDTO.
    /// </summary>
    /// <param name="message">
    /// Message to converts.
    /// </param> 
    /// <returns>
    /// ExceptionDTO object.
    /// </returns>
    public static ExceptionDTO ToDTO(this ExceptionDetail message)
    {
        if (message.IsNotNull())
        {
            //ExceptionDTO exceptionDTO = (ExceptionDTO)message.ToMessageDTO();
            ExceptionDTO exceptionDTO = new ExceptionDTO()
            {
                ExceptionMessage = message.ExceptionMessage,
                StackTrace = message.StackTrace,
                ExceptionTrace = message.ExceptionTrace
            };
            exceptionDTO.LoadMessageDTO(message);

            return exceptionDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a LogDetail into a LogDTO.
    /// </summary>
    /// <param name="message">
    /// Message to converts.
    /// </param> 
    /// <returns>
    /// LogDTO object.
    /// </returns>
    public static LogDTO ToDTO(this LogDetail message)
    {
        if (message.IsNotNull())
        {
            //LogDTO logDTO = (LogDTO)message.ToMessageDTO();
            LogDTO logDTO = new LogDTO()
            {
                LogValue = message.LogValue
            };
            logDTO.LoadMessageDTO(message);

            return logDTO;
        }

        return null;
    }

    /// <summary>
    /// Loads a MessageDTO with data from a MessageDetail object.
    /// </summary>
    /// <param name="message">
    /// Message to load data into.
    /// </param>
    /// <param name="detail">
    /// MessageDetail to populate the MessageDTO object.
    /// </param>
    public static void LoadMessageDTO(this MessageDTO message, MessageDetail detail)
    {
        if (message.IsNotNull())
        {
            message.Id = detail.Id;
            message.Date = detail.Date;
            message.Level = detail.Level.ToString();
            message.Message = detail.Message;
            message.CustomListOfParameters = detail.CustomListOfParameters;
            message.MachineInformation = detail.MachineInformation.ToDTO();
            message.ApplicationInformation = detail.ApplicationInformation.ToDTO();
            message.WebApplicationInformation = detail.WebApplicationInformation.ToDTO();
            message.StackFrame = detail.StackFrame.ToDTO();
        }
    }

    /// <summary>
    /// Converts a Machine into a MachineDTO.
    /// </summary>
    /// <param name="machine">
    /// Machine to converts.
    /// </param> 
    /// <returns>
    /// MachineDTO object.
    /// </returns>
    public static MachineDTO ToDTO(this Machine machine)
    {
        if (machine.IsNotNull())
        {
            MachineDTO machineDTO = new MachineDTO()
            {
                CLRVersion = machine. CLRVersion,
                DomainName = machine.DomainName,
                Name = machine.Name,
                OperativeSystemVersion = machine.OperativeSystemVersion,
                UserAccountName = machine.UserAccountName,
                OperativeSystem = machine.OperativeSystem,
            };

            return machineDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a Application into a ApplicationDTO.
    /// </summary>
    /// <param name="application">
    /// Application to converts.
    /// </param> 
    /// <returns>
    /// ApplicationDTO object.
    /// </returns>
    public static ApplicationDTO ToDTO(this Application application)
    {
        if (application.IsNotNull())
        {
            ApplicationDTO applicationDTO = new ApplicationDTO()
            {
                IsDebuggingEnabled = application.IsDebuggingEnabled,
                PhysicalApplicationPath = application.PhysicalApplicationPath,
                AssemblyLocation = application.AssemblyLocation,
                FriendlyName = application.FriendlyName,
                IsFullyTrusted = application.IsFullyTrusted,
                UserDomainName = application.UserDomainName,
                UserName = application.UserName
            };

            return applicationDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a WebApplication into a WebApplicationDTO.
    /// </summary>
    /// <param name="webApplication">
    /// Web application to converts.
    /// </param> 
    /// <returns>
    /// WebApplicationDTO object.
    /// </returns>
    public static WebApplicationDTO ToDTO(this WebApplication webApplication)
    {
        if (webApplication.IsNotNull())
        {
            WebApplicationDTO webApplicationDTO = new WebApplicationDTO()
            {
                CurrentURL = webApplication.CurrentURL,
                CurrentURLParameters = webApplication.CurrentURLParameters,
                PreviousURL = webApplication.PreviousURL,
                UserHostIP = webApplication.UserHostIP,
                IsSecureConnection = webApplication.IsSecureConnection,
                DomainName = webApplication.DomainName,
                CookiesValues = webApplication.CookiesValues,
                HeadersValues = webApplication.HeadersValues,
                UserLanguagePreferences = webApplication.UserLanguagePreferences
            };

            return webApplicationDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a Method into a MethodDTO.
    /// </summary>
    /// <param name="method">
    /// Method to converts.
    /// </param> 
    /// <returns>
    /// MethodDTO object.
    /// </returns>
    public static MethodDTO ToDTO(this Method method)
    {
        if (method.IsNotNull())
        {
            MethodDTO methodDTO = new MethodDTO()
            {
                CallerFileName = method.CallerFileName,
                CallerMethodName = method.CallerMethodName,
                CallerLineNumber = method.CallerLineNumber,
            };

            return methodDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a list of Parameter objects into a list of ParameterDTO objects.
    /// </summary>
    /// <param name="parameters">
    /// List of Parameters.
    /// </param> 
    /// <returns>
    /// List of ParameterDTO objects.
    /// </returns>
    public static List<ParameterDTO> ToDTO(this List<Parameter> parameters)
    {
        if (parameters.HasElements())
        {
            List<ParameterDTO> parametersDTO = new List<ParameterDTO>();
            foreach (Parameter parameter in parameters)
            {
                parametersDTO.Add(parameter.ToDTO());
            }

            return parametersDTO;
        }

        return null;
    }

    /// <summary>
    /// Converts a Parameter into a ParameterDTO.
    /// </summary>
    /// <param name="parameter">
    /// Parameter to converts.
    /// </param> 
    /// <returns>
    /// ParameterDTO object.
    /// </returns>
    public static ParameterDTO ToDTO(this Parameter parameter)
    {
        if (parameter.IsNotNull())
        {
            ParameterDTO parameterDTO = new ParameterDTO()
            {
                Name = parameter.Name,
                Value = parameter.Value,
            };

            return parameterDTO;
        }

        return null;
    }
}