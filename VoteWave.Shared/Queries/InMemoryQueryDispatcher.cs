using Microsoft.Extensions.DependencyInjection;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Shared.Queries;

internal sealed class InMemoryQueryDispatcher(IServiceProvider serviceProvider)
    : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await (Task<TResult>)
            handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))
            .Invoke(handler, [query]);
    }
}