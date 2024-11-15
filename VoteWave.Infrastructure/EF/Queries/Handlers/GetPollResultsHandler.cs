using Microsoft.EntityFrameworkCore;
using VoteWave.Application.Polling.DTOs;
using VoteWave.Application.Polling.Queries;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Infrastructure.EF.Models;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Infrastructure.EF.Queries.Handlers;

public class GetPollResultsHandler(ReadDbContext context)
    : IQueryHandler<GetPollResults, PollResultsDto>
{
    private readonly DbSet<PollReadModel> _polls = context.Polls;

    public async Task<PollResultsDto> HandleAsync(GetPollResults query)
        => await _polls
            .Include(p => p.Options)
            .ThenInclude(o => o.Votes)
            .Where(p => p.Id == query.PollId)
            .AsNoTracking()
            .Select(p => p.AsResultsDto())
            .SingleOrDefaultAsync();
}