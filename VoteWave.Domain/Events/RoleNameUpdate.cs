using VoteWave.Domain.Entities;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Events;

public record RoleNameUpdated(Role Role) : IDomainEvent;

