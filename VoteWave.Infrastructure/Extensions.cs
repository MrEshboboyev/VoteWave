using Microsoft.Extensions.DependencyInjection;
using VoteWave.Infrastructure.Auth;
using VoteWave.Shared.Abstractions.Auth;
using VoteWave.Shared.Queries;

namespace VoteWave.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddQueries();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
