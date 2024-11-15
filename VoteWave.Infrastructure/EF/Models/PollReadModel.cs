namespace VoteWave.Infrastructure.EF.Models;

public class PollReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Title { get; set; }
    public ICollection<OptionReadModel> Options { get; set; } = [];
}
