using VoteWave.Domain.Factories;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class CreatePollHandler(IPollFactory pollFactory,
    IPollRepository pollRepository) : ICommandHandler<CreatePoll>
{
    private readonly IPollFactory _pollFactory = pollFactory;
    private readonly IPollRepository _pollRepository = pollRepository;

    public async Task HandleAsync(CreatePoll command)
    {
        var poll = _pollFactory.Create(command.Title);

        await _pollRepository.AddAsync(poll);
    }
}
