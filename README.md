![Anything you want, you got it.](https://jorge-github.github.io/Roy/web/images/logo-biggerfont-white.PNG)

# Roy - Logging for .NET Core

_“Anything you want, you got it._  
_Anything you need, you got it._  
_Anything at all, you got it, baby…”_

Logging service library for .NET.

[Roy's Website](https://jorge-github.github.io/Roy/)

[![Donate](https://jorge-github.github.io/Roy/web/images/donate.png)](https://www.paypal.com/donate/?hosted_button_id=R7WNFY544K8LQ)

---
### Features

- Logging into local files (append or create a new file)
- Logging into multiple databases
- E-mail logs to multiple accounts
- Submit logs to APIs 
- Logging into the event system (Windows/Linux)
- Log any exception (MVC Only)
- Easy to customize

By default, Roy will save the exceptions and logs in the “exceptions” and "logs" folders. These folders will be created inside the bin folder. This behavior can be changed by using the LogExtension.Settings object.

Roy can also save the exceptions and logs on the computer event system (Windows/Linux), send the exceptions and logs by E-mail, 
save exceptions and logs into multiple databases, and/or submit them to multiple APIs. 
These behaviors can be set by using the LogExtension.Settings object. Check the Settings Wiki for more information.

---
### Default Exception Logging

The following code demonstrates the basic usage of Roy exception logging. Just add Roy.Logging to your using statements. 
You can now run the "SaveAsync" method to save the exception. There is no need for any further configuration or object injection.

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
### Logging Objects or Data

The following code demonstrates the basic usage of Roy logging. Just add Roy.Logging to your using statements. 
You can now run the "LogAsync" method to log any object or primitive. There is no need for any further configuration or object injection.

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
### Logging all the Exceptions on Your MVC Application

To log any exception on your MVC app, add the code below to the Program.cs class before the app.Run() method call.

```cs
    // Add the line below on Program.cs file, before the app.Run() method call.
    app.UseRoyExceptionHandler();

    ...
    app.Run();    
```

You can load the Roy Logging settings from the appsettings.json file by passing the builder object to the UseRoyExceptionHandler method.

```cs
    var builder = WebApplication.CreateBuilder(args);
    ...
    // Pass the builder object to the UseRoyExceptionHandler method.
    app.UseRoyExceptionHandler(builder);
    ...
    app.Run();    
```

### Roy Logging JSON Sample Object:

```js client
{
  "AllowedHosts": "*",
  "RoyLogging": {
    "Exception": {
      "DefaultFolderName": "exceptionsUT",
      "SaveLogOnFile": true,
      "LoadInformationSettings": {
        "LogApplicationInformation": true,
        "LogMachineInformation": true,
        "LogMethodInformation": true
      },
      "Emails": [
        {
          "UserAccount": "shania60@ethereal.email",
          "UserPassword": "wK2jPUxzqdnsDBNCUj",
          "From": "roy@yahoo.com",
          "DisplayNameFrom": "ROY Logger",
          "Server": {
            "Host": "smtp.ethereal.email"
          },
          "Receivers": [
            {
              "To": "royorbinson@gmail.com",
              "BCC": "",
              "CC": "",
              "Subject": ""
            }
          ],
          "Language": "English"
        }
      ],
      "APIs": [
        {
          "URL": "https://api.restful-api.dev/objects",
          "LevelsToPost": [
            "Critical"
          ]
        }
      ],
      "Databases": [
        {
          "DatabaseName": "RoyDev",
          "StringConnection": "Data Source=DBName;Initial Catalog=RoyDev;Integrated Security=false;Trusted_Connection=True;TrustServerCertificate=True;User ID=madona;Password=45Pasword3",
          "TableName": ""
        }
      ]
    },
    "Log": {
      "DefaultFolderName": "logsUT",
      "LoadInformationSettings": {
        "LogApplicationInformation": true,
        "LogMachineInformation": true,
        "LogMethodInformation": true
      },
      "Emails": [
        {
          "UserAccount": "shania60@ethereal.email",
          "UserPassword": "wK2jPUxzqdnsDBNCUj",
          "From": "roy@yahoo.com",
          "DisplayNameFrom": "ROY Logger",
          "Server": {
            "Host": "smtp.ethereal.email"
          },
          "Receivers": [
            {
              "To": "royorbinson@gmail.com",
              "BCC": "",
              "CC": "",
              "Subject": ""
            }
          ],
          "Language": "English",
          "LevelsToReport": [ "Warning" ]
        }
      ],
      "APIs": [
        {
          "URL": "https://api.restful-api.dev/objects"
        }
      ],
      "Databases": [
        {
          "DatabaseName": "RoyDev",
          "StringConnection": "Data Source=DBName;Initial Catalog=RoyDev;Integrated Security=false;Trusted_Connection=True;TrustServerCertificate=True;User ID=madona;Password=45Pasword3",
          "TableName": ""
        }
      ]
    }
  }
}
```
---
### How to Use

Download the latest [release](https://github.com/Jorge-GitHub/Roy/releases), then add a reference to Avalon.Base.Extension.dll and Roy.Logging.dll libraries. If you are developing an MVC application, you will also need to add a reference to Roy.Logging.MVC.dll.

By default, the exceptions and logs will be saved in the bin folder. For instance, "bin\Debug\net7.0". 
Inside this folder, Roy will create an “exceptions” and a "logs" folder. This behavior can be changed 
by modifying the object LogExtension.Settings static property.

You can make any changes to the settings by calling the LogExtension.Settings static property.

I will write more documentation as soon as I have some spare time.

For more information visit Roy's [wiki](https://github.com/Jorge-GitHub/Roy/wiki).

## Author
- [Jorge Gonzalez](https://github.com/Jorge-GitHub)

[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)

[![Donate](https://jorge-github.github.io/Roy/web/images/donate.png)](https://www.paypal.com/donate/?hosted_button_id=R7WNFY544K8LQ)
