using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record PollTitle
{
    public string Value { get; }

    public PollTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPollTitleException();
        }
        Value = value;
    }

    public static implicit operator string(PollTitle title)
        => title.Value;

    public static implicit operator PollTitle(string title)
        => new(title);
}