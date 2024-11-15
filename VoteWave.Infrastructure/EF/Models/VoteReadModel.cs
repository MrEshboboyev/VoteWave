namespace VoteWave.Infrastructure.EF.Models;

public class VoteReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserId { get; set; }
    public DateTime VotedAt { get; set; }
}
