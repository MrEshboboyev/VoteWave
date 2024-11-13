using VoteWave.Domain.Events;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class Role : AggregateRoot<Guid>
{
    private RoleName _name;
    private readonly List<User> _users = [];

    public RoleName Name => _name;
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Role() { }


    internal Role(RoleName name)
    {
        _name = name;

        AddEvent(new RoleCreated(this));
    }

    public void UpdateName(RoleName name)
    {
        _name = name;

        AddEvent(new RoleNameUpdated(this)); 
    }

    public void AddUser(User user)
    {
        if (!_users.Contains(user))
        {
            _users.Add(user);
        }
    }
}