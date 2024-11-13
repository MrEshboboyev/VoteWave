using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Authorization.Commands;

public record RegisterUser(string Username, string Email,
    string Password) : ICommand;