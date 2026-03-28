using Infrastructure.AppSettings;
using Infrastructure.AppSettings.Models;
using Infrastructure.Storage.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Storage;

public static class DependencyInjection
{
    public static IServiceCollection AddMinioBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<MinioSettings>(configuration);
        return services.AddSingleton<IBlobStorage, MinioBlobStorage>();
    }
}
