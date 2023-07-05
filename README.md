# Roy
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
### Logging and object or any data.

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
}
```
By default, the exceptions and logs will be saved on the bin folder for instance, "bin\Debug\net7.0".
Inside this folder, Roy will create an “exception” and a "logs" folder.
This behavior can be changed by using the object LogSetting.
You can call make any changes to the settings by calling LogExtension.Settings static property.
I will write more documentation as soon as I have some spare time.
