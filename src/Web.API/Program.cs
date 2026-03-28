using Application;
using Infrastructure.AppSettings;
using Infrastructure.AppSettings.Models;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Storage;
using NLog;
using NLog.Web;
using Web.API.Infrastructure;

var logger = LogManager.Setup()
    .LoadConfigurationFromAppSettings(nlogConfigSection: nameof(NLogSettings))
    .GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration.AddJsonConfigurations();

    builder.UseNLog();

    builder.Services
        .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails()
        .AddSwagger()
        .AddEndpoints()
        .ConfigureSerializerOptions()
        .AddPersistence(configuration)
        .AddIdentity()
        .AddMinioBlobStorage(configuration)
        .AddServices()
        .AddRequestHandlers()
        .AddMessaging(configuration);
        

    var app = builder.Build();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapEndpoints(app.MapGroup("api"));

    if (app.Environment.IsDevelopment())
    {
        await app.ApplyMigrations();
        app.MapOpenApi();
        app.UseSwagger();
    }

    app.UseHttpsRedirection();


    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Error(ex, "Exception occured during startup {Message}.", ex.Message);
}
finally
{
    LogManager.Shutdown();
}
