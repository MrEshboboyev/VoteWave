using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

internal class OptionNotFoundException(Guid optionId) : 
    DomainException($"Option with Id {optionId} is not found!")
{
    public Guid Id { get; } = optionId;
}
