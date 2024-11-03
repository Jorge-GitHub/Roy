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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "DefaultApi",
    pattern: "api/{controller}/{action}");

app.UseRoyExceptionHandler(builder, true);
//app.UseRoyExceptionHandler(builder);
//app.UseRoyToLogMissingFiles();
//app.UseRoyToLogMissingFiles(builder);

app.Run();
