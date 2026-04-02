using Domain.Users;
using Infrastructure.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public static  class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IBalanceService, BalanceService>();
    }
}
