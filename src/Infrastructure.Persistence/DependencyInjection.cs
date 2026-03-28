using Domain.Users;
using Infrastructure.AppSettings;
using Infrastructure.AppSettings.Models;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistence(IConfiguration configuration)
        {
            return services
                .AddSettings<DbSettings>(configuration)
                .AddDataContext<IDataContext, DataContext>()
                .AddDataContext<IReadOnlyDataContext, ReadOnlyDataContext>();
        }

        private IServiceCollection AddDataContext<TService, TContext>(QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll)
            where TService : class
            where TContext : DbContext, TService
        {
            return services
                .AddDbContextFactory<TContext>((sp, options) =>
                {
                    var connectionString = sp.GetRequiredService<IOptions<DbSettings>>().Value.ConnectionString;

                    options.UseNpgsql(connectionString, b =>
                        {
                            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                            b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                        })
                        .UseQueryTrackingBehavior(trackingBehavior)
                        .UseSnakeCaseNamingConvention();
                })
                .AddScoped<TService, TContext>((sp) => sp.GetRequiredService<IDbContextFactory<TContext>>().CreateDbContext());
        }

        public IServiceCollection AddIdentity()
        {
            services.AddIdentityCore<User>(options =>
                {
                    options.Password.RequiredLength = 12;

                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme);

            services.AddAuthorization();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.LoginPath = null;
                options.AccessDeniedPath = null;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    },
                };
            });

            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public async Task ApplyMigrations()
        {
            await using var scope = app.ApplicationServices.CreateAsyncScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (dbContext.Database.IsRelational())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
