using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Factories;

public class VoteFactory : IVoteFactory
{
    public Vote Create(Guid pollId, Guid optionId, Guid userId)
        => new(pollId, optionId, userId);
}
