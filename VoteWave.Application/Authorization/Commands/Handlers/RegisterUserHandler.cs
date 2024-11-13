using VoteWave.Application.Authorization.Exceptions;
using VoteWave.Domain.Factories;
using VoteWave.Domain.Repositories;
using VoteWave.Shared.Abstractions.Auth;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Application.Authorization.Commands.Handlers;

internal class RegisterUserHandler(IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserFactory userFactory,
    IPasswordHasher passwordHasher) : ICommandHandler<RegisterUser>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserFactory _userFactory = userFactory;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task HandleAsync(RegisterUser command)
    {
        var (username, email, password) = command;

        var userFromDb = await _userRepository.GetByUsernameAsync(username);

        if (userFromDb != null)
        {
            throw new UserAlreadyExistsException(username);
        }

        var passwordHash = _passwordHasher.Hash(password);
        var user = _userFactory.Create(username, email, passwordHash);

        // add "User" role for new user 
        var role = await _roleRepository.GetByNameAsync("User");

        user.AddRole(role);

        await _userRepository.AddAsync(user);
    }
}