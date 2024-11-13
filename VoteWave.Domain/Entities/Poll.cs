using VoteWave.Domain.Events;
using VoteWave.Domain.Exceptions;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class Poll : AggregateRoot<Guid> 
{
    private PollTitle _title;
    private readonly List<Option> _options = [];

    // public getters
    public PollTitle Title => _title;
    public IReadOnlyCollection<Option> Options => _options.AsReadOnly();

    internal Poll() { }

    internal Poll(PollTitle title)
    {
        _title = title;

        AddEvent(new PollCreated(this));
    }

    internal Poll(PollTitle title, List<Option> options)
    {
        _title = title;
        _options = options;

        AddEvent(new PollCreated(this));
    }

    public void AddOption(Option option)
    {
        _options.Add(option);

        AddEvent(new AddOptionToPoll(this, option));
    }

    public void RemoveOption(Option option)
    {
        _options.Remove(option);

        AddEvent(new DeleteOption(option));
    }

    public void CastVote(Guid optionId, Guid userId)
    {
        // Check if the user has already voted on this poll
        if (_options.Any(option => option.HasUserVoted(userId)))
        {
            throw new UserAlreadyVotedException(userId);
        }

        // Increment vote count for the option
        var option = GetOption(optionId);
        option.IncrementVoteCount();

        // Add a vote event
        AddEvent(new VoteCasted(this, optionId, userId));
    }

    private Option GetOption(Guid optionId)
    {
        var option = _options.SingleOrDefault(o => o.Id == optionId)
            ?? throw new OptionNotFoundException(optionId);

        return option;
    }
}