using Microsoft.EntityFrameworkCore;
using VoteWave.Application.Polling.DTOs;
using VoteWave.Application.Polling.Queries;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Infrastructure.EF.Models;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Infrastructure.EF.Queries.Handlers;

public class GetAllPollsHandler(ReadDbContext context)
    : IQueryHandler<GetAllPolls, IEnumerable<PollDto>>
{
    private readonly DbSet<PollReadModel> _polls = context.Polls;

    public async Task<IEnumerable<PollDto>> HandleAsync(GetAllPolls query)
        => await _polls
            .AsNoTracking()
            .Select(p => p.AsDto())
            .ToListAsync();
}
