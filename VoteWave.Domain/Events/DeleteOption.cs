﻿using VoteWave.Domain.Entities;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Events;

public record DeleteOption(Option Option) : IDomainEvent;