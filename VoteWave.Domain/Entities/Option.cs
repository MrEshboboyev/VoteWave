using VoteWave.Domain.Events;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class Option : AggregateRoot<Guid>
{
    private Guid _pollId;
    private OptionText _text;
    private int _voteCount;

    // public getters
    public Guid PollId => _pollId;
    public OptionText Text => _text;
    public int VoteCount => _voteCount;

    internal Option() { }

    internal Option(Guid pollId, OptionText text)
    {
        _pollId = pollId;
        _text = text;

        AddEvent(new OptionCreated(this));
    }

    public void IncrementVoteCount()
    {
        _voteCount++;

        AddEvent(new VoteCountIncremented(this));
    }

    public bool HasUserVoted(Guid userId)
    {
        // This method would require tracking individual votes if needed
        // Stubbed out for current scope, might use in-memory or persistent tracking in the future
        return false; // Assumes no tracking for now; returns true if user vote is tracked
    }
}
