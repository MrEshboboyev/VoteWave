using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

internal class EmptyPollTitleException : DomainException
{
    public EmptyPollTitleException() : base("Poll title cannot be empty.")
    {
    }
}
