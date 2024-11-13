using System.ComponentModel.DataAnnotations;

namespace VoteWave.Presentation.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
