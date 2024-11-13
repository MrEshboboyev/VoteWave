using Microsoft.EntityFrameworkCore;
using VoteWave.Domain.Entities;
using VoteWave.Domain.Repositories;
using VoteWave.Infrastructure.EF.Contexts;

namespace VoteWave.Infrastructure.EF.Repositories;

internal class PostgresUserRepository(WriteDbContext writeDbContext) : IUserRepository
{
    private readonly DbSet<User> _users = writeDbContext.Users;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public Task<User> GetByIdAsync(Guid id)
        => _users.SingleOrDefaultAsync(t => t.Id == id);


    public Task<User> GetByUsernameAsync(string username)
        => _users.Include(u => u.Roles).SingleOrDefaultAsync(t => t.Username == username);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _users.Remove(user);
        await _writeDbContext.SaveChangesAsync();
    }
}
