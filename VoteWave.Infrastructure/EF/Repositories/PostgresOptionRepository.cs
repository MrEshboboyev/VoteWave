using Microsoft.EntityFrameworkCore;
using VoteWave.Domain.Entities;
using VoteWave.Domain.Repositories;
using VoteWave.Infrastructure.EF.Contexts;

namespace VoteWave.Infrastructure.EF.Repositories;

internal class PostgresOptionRepository(WriteDbContext writeDbContext) : IOptionRepository
{
    private readonly DbSet<Option> _options = writeDbContext.Options;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public Task<Option> GetByIdAsync(Guid id)
        => _options.SingleOrDefaultAsync(t => t.Id == id);


    public async Task AddAsync(Option option)
    {
        await _options.AddAsync(option);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Option option)
    {
        _options.Update(option);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Option option)
    {
        _options.Remove(option);
        await _writeDbContext.SaveChangesAsync();
    }
}
