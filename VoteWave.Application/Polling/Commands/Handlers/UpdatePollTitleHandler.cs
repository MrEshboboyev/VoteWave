using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

internal sealed class UpdatePollTitleHandler(IPollRepository pollRepository) : ICommandHandler<UpdatePollTitle>
{
    private readonly IPollRepository _pollRepository = pollRepository;

    public async Task HandleAsync(UpdatePollTitle command)
    {
        var (pollId, newTitle) = command;

        var poll = await _pollRepository.GetByIdAsync(pollId)
            ?? throw new PollNotFoundException(pollId);

        poll.UpdateTitle(newTitle);

        await _pollRepository.UpdateAsync(poll);
    }
}
