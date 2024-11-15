namespace VoteWave.Application.Polling.DTOs;

public class PollResultsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<OptionDto> Options { get; set; }
}
