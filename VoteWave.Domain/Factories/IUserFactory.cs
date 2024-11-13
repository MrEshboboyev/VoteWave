using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public interface IUserFactory
{
    User Create(Username username, Email email, PasswordHash passwordHash);
}