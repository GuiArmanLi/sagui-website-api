using System.ComponentModel.DataAnnotations;

public class UserSignUpRequest
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Invalid field {0}")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "Field {0} must have {2} - {1} characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    [Compare(nameof(Password), ErrorMessage = "Password must be identical")]
    public required string ConfirmationPassword { get; set; }
}