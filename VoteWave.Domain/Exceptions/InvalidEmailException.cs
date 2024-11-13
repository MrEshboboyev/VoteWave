using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

public class InvalidEmailException : DomainException
{
    public InvalidEmailException() : base("Email is Invalid format.")
    {
    }
}
