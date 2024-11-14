using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record CastVote(Guid PollId, Guid OptionId, Guid UserId) : ICommand;