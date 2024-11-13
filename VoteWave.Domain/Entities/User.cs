using VoteWave.Domain.Events;
using VoteWave.Domain.ValueObjects;
using VoteWave.Shared.Abstractions.Domain;

namespace VoteWave.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    private Username _username;
    private Email _email;
    private PasswordHash _passwordHash;

    private readonly List<Role> _roles = [];

    // public getters
    public Username Username => _username;
    public Email Email => _email;
    public PasswordHash PasswordHash => _passwordHash;
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    // private constructor for ORM support
    private User() { }

    internal User(Username username, Email email, PasswordHash passwordHash)
    {
        _username = username;
        _email = email;
        _passwordHash = passwordHash;

        AddEvent(new UserRegistered(this));
    }

    // Update user's password
    public void UpdatePassword(PasswordHash newPasswordHash)
    {
        _passwordHash = newPasswordHash;

        AddEvent(new UserPasswordChanged(this));
    }

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
        }
    }
}