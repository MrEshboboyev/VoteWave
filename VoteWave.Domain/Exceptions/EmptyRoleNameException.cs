using VoteWave.Shared.Abstractions.Exceptions;

namespace VoteWave.Domain.Exceptions;

public class EmptyRoleNameException : DomainException
{
    public EmptyRoleNameException() : base("Role name cannot be empty.")
    {
    }
}