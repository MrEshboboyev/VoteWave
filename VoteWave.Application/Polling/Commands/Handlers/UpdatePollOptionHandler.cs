using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

internal sealed class UpdatePollOptionHandler(IOptionRepository optionRepository) : ICommandHandler<UpdatePollOption>
{
    private readonly IOptionRepository _optionRepository = optionRepository;

    public async Task HandleAsync(UpdatePollOption command)
    {
        var (pollId, optionId, newText) = command;

        var option = await _optionRepository.GetByIdAsync(optionId)
            ?? throw new OptionNotFoundException(optionId);

        option.UpdateText(newText);

        await _optionRepository.UpdateAsync(option);
    }
}
