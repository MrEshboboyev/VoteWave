using VoteWave.Domain.Entities;

namespace VoteWave.Domain.Repositories;

public interface IOptionRepository
{
    Task<Option> GetByIdAsync(Guid id);
    Task AddAsync(Option option);
    Task UpdateAsync(Option option);
}