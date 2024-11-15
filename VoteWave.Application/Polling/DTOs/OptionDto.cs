namespace VoteWave.Application.Polling.DTOs;

public class OptionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public int VoteCount { get; set; }
    public IEnumerable<VoteDto> Votes { get; set; }
}
