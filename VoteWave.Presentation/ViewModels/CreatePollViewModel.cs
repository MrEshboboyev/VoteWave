namespace VoteWave.Presentation.ViewModels;

public class CreatePollViewModel
{
    public string Title { get; set; }

    // A list of option titles input by the user
    public List<string> OptionTexts { get; set; } = [];
}