using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public class UserFactory : IUserFactory
{
    public User Create(Username username, Email email, PasswordHash passwordHash)
        => new(username, email, passwordHash);
}
