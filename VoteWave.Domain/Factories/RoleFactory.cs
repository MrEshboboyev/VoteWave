using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Domain.Factories;

public class RoleFactory : IRoleFactory
{
    public Role Create(RoleName name)
        => new(name);
}
