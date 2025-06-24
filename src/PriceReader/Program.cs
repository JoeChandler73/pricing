var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web host");
    CreateWebApplication(args).Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
static WebApplication CreateWebApplication(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    builder.AddServiceDefaults();
    builder.AddApplicationServices();
    
    var app = builder.Build();
    app.MapDefaultEndpoints();
    app.UseStatusCodePages();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.AddSwaggerPages();
    app.MapPriceReaderApi();
    
    return app;
}