using VoteWave.Domain.Exceptions;

namespace VoteWave.Domain.ValueObjects;

public record OptionText
{
    public string Value { get; }

    public OptionText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyOptionTextException();
        }
        Value = value;
    }

    public static implicit operator string(OptionText text)
        => text.Value;

    public static implicit operator OptionText(string text)
        => new(text);
}