<p align="center">
  <img src="https://jorge-github.github.io/Roy/web/images/logo-biggerfont-white.PNG" 
      alt="Anything you want, you got it."/>
</p>

# Roy - Logging for .NET Core
Anything you want, you got it. Anything you need, you got it. Anything at all, you got it, baby. Logging service library.


### Default Exception Logging.

The following code demonstrates basic usage of Roy exception logging. Just add Roy.Logging to your using statements.
Now you can run "SaveAsync" method to save the exception. No need for more configuration or object injection.

```cs
using Roy.Logging;

public void AmazingCode()
{
    try
    {
        // Evil code fails.
    }
    catch (Exception ex)
    {
        ex.SaveAsync();
    }
}
```
---
### Logging an object or any data.

The following code demonstrates basic usage of Roy logging. Just add Roy.Logging to your using statements.
Now you can run "LogAsync" method to log any object or primitive. No need for more configuration or object injection.

```cs
using Roy.Logging;

public void AmazingCode()
{
      Artist singer = new Artist();
      singer.Name = "Roy";
      singer.LastName = "Orbinson";
      singer.Description = "Roy Kelton Orbison was an American singer, songwriter, and musician.";
      singer.LogAsync();
      "Anything you want, you got it".LogAsync(); //You can log primitives too.
}
```
---
### Logging all the Exceptions on your MVC application

To log any exception on your MVC app, add the code below on the Program.cs class. Add it before the app.Run() method call.

```cs
    // Add the line below on Program.cs file, before the app.Run() method call.
    app.UseRoyExceptionHandler();

    ...
    app.Run();    
```

You can load the Roy Logging settings from the appsettings.json file by passing the builder object to the method UseRoyExceptionHandler.

```cs
    var builder = WebApplication.CreateBuilder(args);
    ...
    // Pass the builder object to the UseRoyExceptionHandler method.
    app.UseRoyExceptionHandler(builder);
    ...
    app.Run();    
```

Roy Logging JSON sample object:

```js client
  "RoyLogging": {
    "Exception": {
      "DefaultFolderName": "exceptions",
      "SaveLogOnFile": true,
      "LogSettings": {
        "LogApplicationInformation": true,
        "LogMachineInformation": true
      },
      "Emails": [
        {
          "UserAccount": "youremail@email.com",
          "UserPassword": "password",
          "From": "me@yahoo.com",
          "DisplayNameFrom": "ROY Logger",
          "Server": {
            "Host": "smtp.yourserver.com"
          },
          "Receivers": [
            {
              "To": "roy@email.com",
              "BCC": "",
              "CC": "",
              "Subject": ""
            }
          ],
          "Language": "English"
        }
      ]
    },
    "Log": {
      "DefaultFolderName": "logs",
      "SaveLogOnFile": true
    }
  }
```
---
By default, the exceptions and logs will be saved on the bin folder for instance, "bin\Debug\net7.0".
Inside this folder, Roy will create an “exceptions” and a "logs" folder.
This behavior can be changed by using the object LogExtension.Settings static property.

You can make any changes to the settings by calling LogExtension.Settings static property.
I will write more documentation as soon as I have some spare time.

[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
