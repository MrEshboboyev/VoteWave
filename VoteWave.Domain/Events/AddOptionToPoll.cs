using VoteWave.Domain.Entities;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Events;

public record AddOptionToPoll(Poll Poll, Option Option) : IDomainEvent;