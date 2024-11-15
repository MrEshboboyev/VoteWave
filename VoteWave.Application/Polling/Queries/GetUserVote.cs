using VoteWave.Application.Polling.DTOs;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Application.Polling.Queries;

public class GetUserVote : IQuery<VoteDto>
{
    public Guid UserId { get; set; }
    public Guid VoteId { get; set; }
}
