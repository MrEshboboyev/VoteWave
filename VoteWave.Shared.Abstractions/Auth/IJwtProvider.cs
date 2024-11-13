namespace VoteWave.Shared.Abstractions.Auth;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, IEnumerable<string> roles);
}