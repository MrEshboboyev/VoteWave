using VoteWave.Application.Polling.DTOs;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Application.Polling.Queries;

public class GetPollById : IQuery<PollDto>
{
    public Guid PollId { get; set; }
}
