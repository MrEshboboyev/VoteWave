using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Application.Polling.Exceptions;

public class PollNotFoundException(Guid pollId) : 
    DomainException($"Poll with Id {pollId} not found!")
{
    public Guid PollId { get; } = pollId;
}
