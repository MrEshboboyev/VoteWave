using Microsoft.EntityFrameworkCore;
using VoteWave.Application.Polling.DTOs;
using VoteWave.Application.Polling.Queries;
using VoteWave.Infrastructure.EF.Contexts;
using VoteWave.Infrastructure.EF.Models;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Infrastructure.EF.Queries.Handlers;

public class GetPollByIdHandler(ReadDbContext context)
    : IQueryHandler<GetPollById, PollDto>
{
    private readonly DbSet<PollReadModel> _polls = context.Polls;

    public async Task<PollDto> HandleAsync(GetPollById query)
        => await _polls
            .Where(p => p.Id == query.PollId)
            .AsNoTracking()
            .Select(p => p.AsDto())
            .SingleOrDefaultAsync();
}