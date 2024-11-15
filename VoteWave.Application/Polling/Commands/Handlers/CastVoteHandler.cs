using VoteWave.Application.Polling.Exceptions;
using VoteWave.Domain.Factories;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class CastVoteHandler(IPollRepository pollRepository,
    IVoteFactory voteFactory) : ICommandHandler<CastVote>
{
    private readonly IPollRepository _pollRepository = pollRepository;
    private readonly IVoteFactory _voteFactory = voteFactory;

    public async Task HandleAsync(CastVote command)
    {
        var (pollId, optionId, userId) = command;

        var poll = await _pollRepository.GetByIdAsync(pollId)
            ?? throw new PollNotFoundException(pollId);

        var vote = _voteFactory.Create(pollId, optionId, userId);

        poll.CastVote(vote);

        await _pollRepository.UpdateAsync(poll);
    }
}