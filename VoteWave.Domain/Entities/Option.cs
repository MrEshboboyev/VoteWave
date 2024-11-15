using VoteWave.Domain.Events;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class Option : AggregateRoot<Guid>
{
    private Guid _pollId;
    private OptionText _text;
    private int _voteCount;
    private readonly List<Vote> _votes = [];

    public Guid PollId => _pollId;
    public OptionText Text => _text;
    public int VoteCount => _voteCount;
    public IReadOnlyCollection<Vote> Votes => _votes.AsReadOnly();

    internal Option() { }

    internal Option(Guid pollId, OptionText text)
    {
        _pollId = pollId;
        _text = text;
        _voteCount = 0; // Ensure initialized
        AddEvent(new OptionCreated(this));
    }

    public void AddVote(Vote vote)
    {
        if (_votes.Any(v => v.UserId == vote.UserId))
            throw new InvalidOperationException("User has already voted on this option.");

        _votes.Add(vote);
        _voteCount++;

        AddEvent(new VoteAdded(vote));
    }

    public void IncrementVoteCount()
    {
        _voteCount++; // Useful for direct updates (e.g., bulk voting scenarios)
        AddEvent(new VoteCountIncremented(this));
    }
}