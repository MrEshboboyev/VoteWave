using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyUsernameException();
        }
        Value = value;
    }

    public static implicit operator string(Username name)
        => name.Value;

    public static implicit operator Username(string name)
        => new(name);
}