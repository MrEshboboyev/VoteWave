using Microsoft.EntityFrameworkCore;
using VoteWave.Application.Polling.DTOs;
using VoteWave.Application.Polling.Queries;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Infrastructure.EF.Models;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Infrastructure.EF.Queries.Handlers;

public class GetUserVoteHandler(ReadDbContext context)
    : IQueryHandler<GetUserVote, VoteDto>
{
    private readonly DbSet<VoteReadModel> _votes = context.Votes;

    public async Task<VoteDto> HandleAsync(GetUserVote query)
        => await _votes
            .Where(v => v.Id == query.VoteId)
            .AsNoTracking()
            .Select(v => v.AsDto())
            .SingleOrDefaultAsync();
}
