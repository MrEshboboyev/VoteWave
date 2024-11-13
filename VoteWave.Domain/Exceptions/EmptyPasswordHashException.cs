using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

public class EmptyPasswordHashException : DomainException
{
    public EmptyPasswordHashException() : base("Password hash cannot be empty!")
    {
    }
}