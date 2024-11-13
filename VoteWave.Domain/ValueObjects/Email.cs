using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyEmailException();
        }

        if (value.Split('@').Length != 2)
        {
            throw new InvalidEmailException();
        }

        Value = value;
    }

    public static implicit operator string(Email email)
        => email.Value;

    public static implicit operator Email(string email)
        => new(email);
}