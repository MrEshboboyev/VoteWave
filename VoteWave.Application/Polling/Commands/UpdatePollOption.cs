using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record UpdatePollOption(Guid PollId, Guid OptionId, string NewText) : ICommand;
