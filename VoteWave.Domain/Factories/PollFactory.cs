using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public class PollFactory : IPollFactory
{
    public Poll Create(PollTitle pollTitle)
        => new(pollTitle);

    public Poll CreateWithDefaultOptions(PollTitle pollTitle, List<Option> options)
        => new(pollTitle, options);
}
