using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Repositories;

public interface IVoteRepository
{
    Task<Vote> GetByIdAsync(Guid id);
    Task AddAsync(Vote vote);
    Task UpdateAsync(Vote vote);
}