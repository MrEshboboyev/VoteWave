using VoteWave.Domain.Entities;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Events;

public record VoteCasted(Poll Poll, Guid OptionId, Guid UserId) : IDomainEvent;