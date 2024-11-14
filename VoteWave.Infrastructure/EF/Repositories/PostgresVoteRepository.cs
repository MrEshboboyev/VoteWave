using Microsoft.EntityFrameworkCore;
using VoteWave.Domain.Entities;
using VoteWave.Domain.Repositories;
using VoteWave.Infrastructure.EF.Contexts;

namespace VoteWave.Infrastructure.EF.Repositories;

internal class PostgresVoteRepository(WriteDbContext writeDbContext) : IVoteRepository
{
    private readonly DbSet<Vote> _votes = writeDbContext.Votes;
    private readonly WriteDbContext _writeDbContext = writeDbContext;

    public Task<Vote> GetByIdAsync(Guid id)
        => _votes.SingleOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Vote vote)
    {
        await _votes.AddAsync(vote);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vote vote)
    {
        _votes.Update(vote);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Vote vote)
    {
        _votes.Remove(vote);
        await _writeDbContext.SaveChangesAsync();
    }
}
