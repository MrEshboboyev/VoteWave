using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoteWave.Domain.Entities;
using VoteWave.Domain.Factories;
using VoteWave.Domain.ValueObjects;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Shared.Abstractions.Auth;

namespace VoteWave.Infrastructure.Seeding;

public class DatabaseSeeder(WriteDbContext context, 
    IPasswordHasher passwordHasher, 
    ILogger<DatabaseSeeder> logger,
    IRoleFactory roleFactory,
    IUserFactory userFactory)
{
    private readonly DbSet<User> _users = context.Users;
    private readonly DbSet<Role> _roles = context.Roles;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ILogger<DatabaseSeeder> _logger = logger;
    private readonly IRoleFactory _roleFactory = roleFactory;
    private readonly IUserFactory _userFactory = userFactory;

    public async Task SeedAsync()
    {
        // Ensure roles exist
        await EnsureRoleExists("Admin");
        await EnsureRoleExists("User");

        // Ensure admin user exists
        await EnsureAdminUserExists("admin@example.com", "Admin");
    }

    private async Task EnsureRoleExists(string roleName)
    {
        if (!_roles.ToList().Any(r => r.Name.Value == roleName))
        {
            var role = _roleFactory.Create(roleName);
            await _roles.AddAsync(role);
            await context.SaveChangesAsync();
            _logger.LogInformation($"Role '{roleName}' created.");
        }
    }

    private async Task EnsureAdminUserExists(string email, string roleName)
    {
        if (!_users.ToList().Any(u => u.Email.Value == email))
        {
            var username = new Username("admin");
            var emailObj = new Email(email);
            var passwordHash = new PasswordHash(_passwordHasher.Hash("AdminPassword123!")); // Use a secure password

            var adminUser = _userFactory.Create(username, emailObj, passwordHash);

            // Assign the "Admin" role to the user
            var role = _roles.ToList().FirstOrDefault(r => r.Name.Value == roleName);
            if (role != null)
            {
                adminUser.AddRole(role);
            }

            await _users.AddAsync(adminUser);
            await context.SaveChangesAsync();
            _logger.LogInformation($"Admin user '{email}' created with role '{roleName}'.");
        }
    }
}