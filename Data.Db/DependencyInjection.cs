using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Db;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, string connectionString, QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll) where TContext : DbContext
    {
        return services.AddDbContext<TContext>(
            options => options
                .UseNpgsql(connectionString, b =>
                {
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                })
                .UseQueryTrackingBehavior(trackingBehavior)
                .UseSnakeCaseNamingConvention());
    }
}