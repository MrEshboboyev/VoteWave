﻿using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record RemoveOptionFromPoll(Guid PollId, Guid OptionId) : ICommand;
