using Microsoft.Extensions.Logging;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Infrastructure.Logging;

internal sealed class LoggingCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler = commandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger = logger;

    public async Task HandleAsync(TCommand command)
    {
        var commandType = command.GetType().Name;

        try
        {
            _logger.LogInformation($"Started processing {commandType} command.");
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation($"Finished processing {commandType} command.");
        }
        catch 
        {
            _logger.LogError($"Failed to process {commandType} command.");
            throw;
        }
    }
}
