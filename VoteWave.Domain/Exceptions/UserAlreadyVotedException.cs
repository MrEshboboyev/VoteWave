using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

internal class UserAlreadyVotedException(Guid userId) : 
    DomainException($"User with {userId} already voted!")
{
    public Guid UserId { get; } = userId;
}
