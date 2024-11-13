using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Factories;

public interface IVoteFactory
{
    Vote Create(Guid PollId, Guid OptionId, Guid UserId);
}