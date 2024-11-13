using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

internal class EmptyEmailException : DomainException
{
    public EmptyEmailException() : base("Email cannot be empty.")
    {
    }
}
