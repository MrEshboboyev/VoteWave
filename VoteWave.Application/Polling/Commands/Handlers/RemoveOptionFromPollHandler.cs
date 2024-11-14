using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class RemoveOptionFromPollHandler(IOptionRepository optionRepository) : ICommandHandler<RemoveOptionFromPoll>
{
    private readonly IOptionRepository _optionRepository = optionRepository;

    public async Task HandleAsync(RemoveOptionFromPoll command)
    {
        var option = await _optionRepository.GetByIdAsync(command.OptionId)
            ?? throw new OptionNotFoundException(command.OptionId);

        await _optionRepository.RemoveAsync(option);
    }
}