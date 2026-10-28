using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Roy.Logging.Domain.Contants;
using Roy.Logging.MVC.Extensions;
using Roy.UT.Entities;
using System.Net;
using System.Text.Json;

namespace Roy.UT;

/// <summary>
/// MVC tests: a pipeline on a test server, an endpoint that throws, and Roy's handler
/// with and without the application's own error page.
/// </summary>
[TestClass]
public class MvcTest
{
    /// <summary>
    /// This test's helper and folder.
    /// </summary>
    private UTHelper Helper { get; set; }

    /// <summary>
    /// Fresh settings and an empty folder for every test.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        this.Helper = new UTHelper();
        this.Helper.ResetSettings();
    }

    /// <summary>
    /// Delete the test's files.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        this.Helper.DeleteFiles();
    }

    /// <summary>
    /// The order Roy.Mvc.UT had before epic 01: Roy first, the application's page after.
    /// The page is nearer the throw, handles the exception, and Roy never sees it.
    /// Kept to document the order the README warns about.
    /// </summary>
    [TestMethod]
    public async Task TestRoyBeforeTheAppsErrorPageSeesNothing()
    {
        WebApplication app = this.CreateApp();
        app.UseRoyExceptionHandler();
        app.UseExceptionHandler(handler => handler.Run(this.ErrorPage));
        this.MapThrow(app);
        await app.StartAsync();
        try
        {
            HttpResponseMessage response = await app.GetTestClient().GetAsync("/throw");

            Assert.AreEqual("error page", await response.Content.ReadAsStringAsync());
            Assert.IsFalse(Directory.Exists(this.Helper.ExceptionsFolder));
        }
        finally
        {
            await app.StopAsync();
        }
    }

    /// <summary>
    /// The order the README asks for: the application's page first, Roy after. Roy logs
    /// the exception and throws it again, and the page still shows.
    /// </summary>
    [TestMethod]
    public async Task TestRoyAfterTheAppsErrorPageLogsAndThePageShows()
    {
        WebApplication app = this.CreateApp();
        app.UseExceptionHandler(handler => handler.Run(this.ErrorPage));
        app.UseRoyExceptionHandler();
        this.MapThrow(app);
        await app.StartAsync();
        try
        {
            HttpResponseMessage response = await app.GetTestClient().GetAsync("/throw");

            Assert.AreEqual("error page", await response.Content.ReadAsStringAsync());
            string file = this.Helper.GetSingleFile(this.Helper.ExceptionsFolder);
            Assert.AreEqual("Test MVC Exception",
                this.Helper.ReadJson(file).GetProperty("ExceptionMessage").GetString());
        }
        finally
        {
            await app.StopAsync();
        }
    }

    /// <summary>
    /// Roy with no error page of the application's own: the exception is logged and
    /// goes on to the server.
    /// </summary>
    [TestMethod]
    public async Task TestRoyAloneLogs()
    {
        WebApplication app = this.CreateApp();
        app.UseRoyExceptionHandler();
        this.MapThrow(app);
        await app.StartAsync();
        try
        {
            try
            {
                HttpResponseMessage response = await app.GetTestClient().GetAsync("/throw");
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            }
            catch (InvalidOperationException)
            {
                // The test server hands the pipeline's exception back to the client.
            }

            string file = this.Helper.GetSingleFile(this.Helper.ExceptionsFolder);
            Assert.AreEqual("Test MVC Exception",
                this.Helper.ReadJson(file).GetProperty("ExceptionMessage").GetString());
        }
        finally
        {
            await app.StopAsync();
        }
    }

    /// <summary>
    /// A 404 for a path with an extension is logged as a missing file; a 404 for any
    /// other path is not.
    /// </summary>
    [TestMethod]
    public async Task TestMissingFilesAreLoggedAndOtherNotFoundsAreNot()
    {
        WebApplication app = this.CreateApp();
        app.UseRoyExceptionHandler(true);
        this.MapThrow(app);
        await app.StartAsync();
        try
        {
            HttpClient client = app.GetTestClient();

            HttpResponseMessage file = await client.GetAsync("/missing.css");
            HttpResponseMessage route = await client.GetAsync("/api/nothing");

            Assert.AreEqual(HttpStatusCode.NotFound, file.StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, route.StatusCode);
            string record = this.Helper.GetSingleFile(this.Helper.ExceptionsFolder);
            StringAssert.StartsWith(Path.GetFileName(record), Level.MissingFile.ToString() + ".");
            StringAssert.Contains(
                this.Helper.ReadJson(record).GetProperty("ExceptionMessage").GetString(),
                "/missing.css");
        }
        finally
        {
            await app.StopAsync();
        }
    }

    /// <summary>
    /// A web application on the test server, in the Production environment, so no
    /// developer exception page is added for us.
    /// </summary>
    /// <returns>
    /// The application, not started.
    /// </returns>
    private WebApplication CreateApp()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Logging.ClearProviders();

        return builder.Build();
    }

    /// <summary>
    /// Map the endpoint that throws.
    /// </summary>
    /// <param name="app">
    /// Web application.
    /// </param>
    private void MapThrow(WebApplication app)
    {
        app.MapGet("/throw", () =>
        {
            throw new InvalidOperationException("Test MVC Exception");
        });
    }

    /// <summary>
    /// The application's own error page.
    /// </summary>
    /// <param name="context">
    /// The request.
    /// </param>
    /// <returns>
    /// The current task.
    /// </returns>
    private Task ErrorPage(HttpContext context)
    {
        return context.Response.WriteAsync("error page");
    }
}
