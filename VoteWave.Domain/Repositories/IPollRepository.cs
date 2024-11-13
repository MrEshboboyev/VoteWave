using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Repositories;

public interface IPollRepository
{
    Task<Poll> GetByIdAsync(Guid id);
    Task AddAsync(Poll poll);
    Task UpdateAsync(Poll poll);
}