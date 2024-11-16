using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record UpdatePollTitle(Guid PollId, string NewTitle) : ICommand;
