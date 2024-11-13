using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public interface IOptionFactory
{
    Option Create(Guid pollId, OptionText optionText);
}