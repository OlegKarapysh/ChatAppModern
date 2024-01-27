namespace ChatAppModern.Domain.DTOs.Authentication;

public class RegistrationDto : LoginDto
{
    [Required(ErrorMessage = "Username is required!")]
    [StringLength(30, MinimumLength = 3,
         ErrorMessage = "Username must be between 3 and 30 characters long!")]
    public string UserName { get; set; } = string.Empty;
    [Compare(nameof(Password), ErrorMessage = "Passwords must match!")]
    public string RepeatPassword { get; set; } = string.Empty;
}