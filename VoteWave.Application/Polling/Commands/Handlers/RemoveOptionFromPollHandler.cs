using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class RemoveOptionFromPollHandler(IPollRepository pollRepository,
    IOptionRepository optionRepository) : ICommandHandler<RemoveOptionFromPoll>
{
    private readonly IPollRepository _pollRepository = pollRepository;
    private readonly IOptionRepository _optionRepository = optionRepository;

    public async Task HandleAsync(RemoveOptionFromPoll command)
    {
        var poll = await _pollRepository.GetByIdAsync(command.PollId)
            ?? throw new PollNotFoundException(command.PollId);

        var option = await _optionRepository.GetByIdAsync(command.OptionId)
            ?? throw new OptionNotFoundException(command.OptionId);

        poll.RemoveOption(option);

        await _pollRepository.UpdateAsync(poll);
    }
}