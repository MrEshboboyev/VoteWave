using VoteWave.Application.Polling.DTOs;
using VoteWave.Shared.Abstractions.Queries;

namespace VoteWave.Application.Polling.Queries;

public class GetAllPolls : IQuery<IEnumerable<PollDto>>
{
}
