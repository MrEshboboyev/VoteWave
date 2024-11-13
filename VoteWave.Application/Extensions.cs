using Microsoft.Extensions.DependencyInjection;
using VoteWave.Domain.Factories;
using VoteWave.Shared.Commands;

namespace VoteWave.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<IUserFactory, UserFactory>();
        services.AddSingleton<IRoleFactory, RoleFactory>();

        return services;
    }
}

