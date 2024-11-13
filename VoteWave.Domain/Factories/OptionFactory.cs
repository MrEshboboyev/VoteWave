using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public class OptionFactory : IOptionFactory
{
    public Option Create(Guid pollId, OptionText optionText)
        => new(pollId, optionText);
}
