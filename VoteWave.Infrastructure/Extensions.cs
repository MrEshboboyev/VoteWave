using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using VoteWave.Infrastructure.Auth;
using VoteWave.Infrastructure.Auth.Configuration;
using VoteWave.Infrastructure.Auth.Options;
using VoteWave.Infrastructure.EF;
using VoteWave.Infrastructure.Logging;
using VoteWave.Infrastructure.Seeding;
using VoteWave.Shared.Abstractions.Auth;
using VoteWave.Shared.Abstractions.Commands;
using VoteWave.Shared.Queries;

namespace VoteWave.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostges(configuration);
        services.AddQueries();

        services.TryDecorate(typeof(ICommandHandler<>),
            typeof(LoggingCommandHandlerDecorator<>));

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Configure Serilog using appsettings.json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)      // Reads from appsettings.json
            .Enrich.FromLogContext()                    // Adds contextual information to logs
            .Enrich.WithExceptionDetails()              // Adds exception details (if using Serilog.Exceptions)
            .CreateLogger();

        // Register Serilog as the logging provider
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

        // Register other infrastructure services (e.g., JWT options, authentication)
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.AddJwtAuthentication(configuration);

        services.AddScoped<RequestLoggingMiddleware>();

        // Register DatabaseSeeder for seeding roles and admin user
        services.AddScoped<DatabaseSeeder>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>(); // Add request logging middleware
        return app;
    }
}
