﻿using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Polling.Commands;

public record CreatePoll(string Title, List<string> OptionTexts) : ICommand;