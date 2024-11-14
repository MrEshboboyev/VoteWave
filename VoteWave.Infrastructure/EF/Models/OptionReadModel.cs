namespace VoteWave.Infrastructure.EF.Models;

public class OptionReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public Guid PollId { get; set; }
    public string Text { get; set; }
    public int VoteCount { get; set; }
    public ICollection<VoteReadModel> Votes { get; set; } = [];
}
