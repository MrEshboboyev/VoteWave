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

    public void UpdateTitle(PollTitle title)
    {
        _title = title;

        AddEvent(new PollTitleUpdated(this));
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

    public void CastVote(Vote vote)
    {
        var option = _options.FirstOrDefault(o => o.Id == vote.OptionId)
            ?? throw new InvalidOperationException("Option not found in poll.");

        option.AddVote(vote);

        // Track state change or raise domain events if required
        AddEvent(new VoteCasted(this, vote.OptionId, vote.UserId));
    }

    private Option GetOption(Guid optionId)
    {
        var option = _options.SingleOrDefault(o => o.Id == optionId)
            ?? throw new OptionNotFoundException(optionId);

        return option;
    }
}