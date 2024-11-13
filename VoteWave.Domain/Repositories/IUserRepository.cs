using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}