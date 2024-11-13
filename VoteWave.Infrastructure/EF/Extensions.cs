using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteWave.Domain.Repositories;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Infrastructure.EF.Options;
using VoteWave.Infrastructure.EF.Repositories;
using VoteWave.Shared.Options;

namespace VoteWave.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddPostges(this IServiceCollection services,
        IConfiguration configuration)
    {
        // adding lifetimes
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<IRoleRepository, PostgresRoleRepository>();

        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<ReadDbContext>(ctx =>
             ctx.UseNpgsql(options.ConnectionString));
        services.AddDbContext<WriteDbContext>(ctx =>
             ctx.UseNpgsql(options.ConnectionString));

        return services;
    }
}

