using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record PasswordHash
{
    public string Value { get; set; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPasswordHashException();
        }

        Value = value;
    }

    public static implicit operator string(PasswordHash passwordHash)
        => passwordHash.Value;
    
    public static implicit operator PasswordHash(string passwordHash)
        => new(passwordHash);
}
