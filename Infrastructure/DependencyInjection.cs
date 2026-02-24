using Data.Db;
using Data.Db.Entities;
using Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.ConfigureAppSettings(configuration)
            .AddDataContext<DataContext>()
            .AddDataContext<ReadOnlyDataContext>(QueryTrackingBehavior.NoTracking)
            .AddIdentity();
    }

    private static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        return services;
    }
    
    private static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll) where TContext : DbContext
    {
        return services.AddDbContext<TContext>((sp, options) => options
                .UseNpgsql(sp.GetRequiredService<IOptions<DatabaseSettings>>().Value.ConnectionString, b =>
                {
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                })
                .UseQueryTrackingBehavior(trackingBehavior)
                .UseSnakeCaseNamingConvention());
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAuthentication()
            .AddCookie();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.LoginPath = null;
            options.AccessDeniedPath = null;
            // todo: override events
        });
        
        return services;
    }
}