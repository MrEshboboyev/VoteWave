using VoteWave.Domain.Events;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class Vote : AggregateRoot<Guid>
{
    private Guid _pollId;
    private Guid _optionId;
    private Guid _userId;
    private DateTime _votedAt;

    // public getters
    public Guid PollId => _pollId;
    public Guid OptionId => _optionId;
    public Guid UserId => _userId;
    public DateTime VotedAt => _votedAt;

    internal Vote() { }

    internal Vote(Guid pollId, Guid optionId, Guid userId)
    {
        _pollId = pollId;
        _optionId = optionId;
        _userId = userId;
        _votedAt = DateTime.UtcNow;

        AddEvent(new VoteCreated(this));
    }

    public bool IsDuplicateVote(Guid userId, Guid pollId)
    {
        return _pollId == pollId && _userId == userId;
    }
}
