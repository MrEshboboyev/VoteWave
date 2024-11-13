using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Authorization.Commands;

public record AuthenticateUser(string Username, string Password) : ICommand<string>;