using VoteWave.Application.Polling.DTOs;
using VoteWave.Infrastructure.EF.Models;

namespace VoteWave.Infrastructure.EF.Queries;

public static class Extensions
{
    public static PollDto AsDto(this PollReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            Title = readModel.Title
        };

    public static PollResultsDto AsResultsDto(this PollReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            Title = readModel.Title,
            Options = readModel.Options.Select(o => new OptionDto
            {
                Id = o.Id,
                Text = o.Text,
                VoteCount = o.VoteCount,
                Votes = o.Votes.Select(v => v.AsDto())
            })
        };

    public static OptionDto AsDto(this OptionReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            Text = readModel.Text,
            VoteCount = readModel.VoteCount,
            Votes = readModel.Votes.Select(v => v.AsDto())
        };

    public static VoteDto AsDto(this VoteReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            OptionId = readModel.OptionId,
            UserId = readModel.UserId,
            VotedAt = readModel.VotedAt
        };
}

