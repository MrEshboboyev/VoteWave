using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Application.Authorization.Exceptions;

internal class UserAlreadyExistsException(string username) :
    DomainException($"This username {username} already exists in our system!")
{
    public string Username { get; } = username;
}
