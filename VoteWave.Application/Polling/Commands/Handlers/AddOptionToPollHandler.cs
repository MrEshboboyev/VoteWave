using VoteWave.Domain.Factories;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands.Handlers;

public class AddOptionToPollHandler(IOptionFactory optionFactory,
    IOptionRepository optionRepository) : ICommandHandler<AddOptionToPoll>
{
    private readonly IOptionFactory _optionFactory = optionFactory;
    private readonly IOptionRepository _optionRepository = optionRepository;

    public async Task HandleAsync(AddOptionToPoll command)
    {
        var (pollId, optionText) = command;

        var option = _optionFactory.Create(pollId, optionText);

        await _optionRepository.AddAsync(option);
    }
}
