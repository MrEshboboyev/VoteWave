using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public interface IRoleFactory
{
    Role Create(RoleName name);
}