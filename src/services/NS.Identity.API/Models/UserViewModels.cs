using System.ComponentModel.DataAnnotations;

namespace NS.Identity.API.Models;

public class UserRegister
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The field {0} needs to be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}

public class UserLogin
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The field {0} needs to be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}
