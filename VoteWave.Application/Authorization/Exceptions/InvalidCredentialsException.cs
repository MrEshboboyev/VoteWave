using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Application.Authorization.Exceptions;

public class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException() : base("Invalid credentials!")
    {
    }
}
