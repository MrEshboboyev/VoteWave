using Microsoft.EntityFrameworkCore;
using VoteWave.Domain.Entities;
using VoteWave.Domain.Repositories;
using VoteWave.Infrastructure.EF.Contexts;

namespace VoteWave.Infrastructure.EF.Repositories;

internal class PostgresPollRepository(WriteDbContext writeDbContext) : IPollRepository
{
    private readonly DbSet<Poll> _polls = writeDbContext.Polls;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public Task<Poll> GetByIdAsync(Guid id)
        => _polls
            .Include(p => p.Options)
                .ThenInclude(o => o.Votes)
            .SingleOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Poll poll)
    {
        await _polls.AddAsync(poll);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Poll poll)
    {
        _polls.Update(poll);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Poll poll)
    {
        _polls.Remove(poll);
        await _writeDbContext.SaveChangesAsync();
    }
}
