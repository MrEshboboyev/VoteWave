using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public interface IPollFactory
{
    Poll Create(PollTitle pollTitle);
    Poll CreateWithDefaultOptions(PollTitle pollTitle, List<Option> options);
}