using VoteWave.Application.Authorization.Exceptions;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Auth;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Authorization.Commands.Handlers;

public class AuthenticateUserHandler(IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher) : ICommandHandler<AuthenticateUser, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<string> HandleAsync(AuthenticateUser command)
    {
        var user = await _userRepository.GetByUsernameAsync(command.Username);
        if (user == null || !_passwordHasher.Verify(command.Password, user.PasswordHash.Value))
        {
            throw new InvalidCredentialsException();
        }

        List<string> roles = user.Roles.Select(x => x.Name.Value).ToList();

        return _jwtProvider.GenerateToken(user.Id, roles);
    }
}