using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

public class EmptyUsernameException : DomainException
{
    public EmptyUsernameException() : base("Username cannot be empty.")
    {
    }
}