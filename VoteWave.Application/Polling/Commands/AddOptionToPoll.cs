using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record AddOptionToPoll(Guid PollId, string Text) : ICommand;
