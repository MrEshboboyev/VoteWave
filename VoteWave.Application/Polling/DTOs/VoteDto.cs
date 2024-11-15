namespace VoteWave.Application.Polling.DTOs;

public class VoteDto
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserId { get; set; }
    public DateTime VotedAt { get; set; }
}

