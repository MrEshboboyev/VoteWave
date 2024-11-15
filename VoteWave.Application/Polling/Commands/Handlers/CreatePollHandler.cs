using VoteWave.Domain.Entities;
using VoteWave.Domain.Factories;
using VoteWave.Domain.Repositories;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class CreatePollHandler(IPollFactory pollFactory,
    IPollRepository pollRepository,
    IOptionFactory optionFactory) : ICommandHandler<CreatePoll>
{
    private readonly IPollFactory _pollFactory = pollFactory;
    private readonly IPollRepository _pollRepository = pollRepository;
    private readonly IOptionFactory _optionFactory = optionFactory;

    public async Task HandleAsync(CreatePoll command)
    {
        var poll = _pollFactory.Create(command.Title);
        // Convert option titles to Option entities
        var options = command.OptionTexts.Select(text => 
            _optionFactory.Create(poll.Id, new OptionText(text)));

        foreach (var option in options)
        {
            poll.AddOption(option);
        }

        await _pollRepository.AddAsync(poll);
    }
}
