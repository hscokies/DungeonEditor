using Infrastructure.AppSettings;
using Infrastructure.AppSettings.Models;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSettings<MessagingSettings>(configuration)
            .AddMassTransit(x =>
        {
            x.UsingRabbitMq((context,cfg) =>
            {
                var settings = context.GetRequiredService<IOptions<MessagingSettings>>().Value;
                

                cfg.Host(settings.Host, settings.VirtualHost, h => {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
            
            x.AddConsumers(typeof(DependencyInjection).Assembly);
        });
    }
}
