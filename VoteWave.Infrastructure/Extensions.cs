using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteWave.Infrastructure.Auth;
using VoteWave.Infrastructure.EF;
using VoteWave.Shared.Abstractions.Auth;
using VoteWave.Shared.Queries;

namespace VoteWave.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostges(configuration);
        services.AddQueries();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
