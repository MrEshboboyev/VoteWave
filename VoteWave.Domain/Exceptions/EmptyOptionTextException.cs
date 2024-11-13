using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

internal class EmptyOptionTextException : DomainException
{
    public EmptyOptionTextException() : base("Option text cannot be empty.")
    {
    }
}
