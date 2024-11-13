using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VoteWave.Shared.Services;

internal sealed class AppInitializer(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(a => 
                typeof(DbContext).IsAssignableFrom(a) && 
                !a.IsInterface &&
                a != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();

        foreach (var dbContextType in dbContextTypes)
        {
            if (scope.ServiceProvider.GetRequiredService(dbContextType) 
                is not DbContext dbContext) continue;
            
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}