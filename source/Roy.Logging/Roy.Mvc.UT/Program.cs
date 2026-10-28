using Roy.Logging.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

// Roy goes after the app's own error handling: it logs the exception and throws it
// again, so the /Error page above still runs.
app.UseRoyExceptionHandler(builder, true);
//app.UseRoyExceptionHandler(builder);
//app.UseRoyToLogMissingFiles();
//app.UseRoyToLogMissingFiles(builder);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "DefaultApi",
    pattern: "api/{controller}/{action}");

app.Run();
