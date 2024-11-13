using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record RoleName
{
    public string Value { get; }

    public RoleName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyRoleNameException();
        }
        Value = value;
    }

    public static implicit operator string(RoleName title)
        => title.Value;

    public static implicit operator RoleName(string title)
        => new(title);
}