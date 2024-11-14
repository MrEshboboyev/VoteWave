using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class CastVoteHandler(IPollRepository pollRepository) : ICommandHandler<CastVote>
{
    private readonly IPollRepository _pollRepository = pollRepository;

    public async Task HandleAsync(CastVote command)
    {
        var (pollId, optionId, userId) = command;

        var poll = await _pollRepository.GetByIdAsync(pollId)
            ?? throw new PollNotFoundException(pollId);

        poll.CastVote(optionId, userId);
    }
}