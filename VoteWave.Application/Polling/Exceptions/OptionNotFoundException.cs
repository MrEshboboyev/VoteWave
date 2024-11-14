using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Application.Polling.Exceptions;

public class OptionNotFoundException(Guid optionId) : 
    DomainException($"Option with Id {optionId} not found!")
{
    public Guid OptionId { get; } = optionId;
}
