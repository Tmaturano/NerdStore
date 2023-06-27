using NS.Core.Communication;
using NS.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Models;

public class UserRegister
{
    [Required]
    [DisplayName("Complete Name")]
    public string Name { get; set; }

    [Required]
    [DisplayName("CPF")]
    [Cpf]
    public string Cpf { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The field {0} needs to be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password")]
    [Display(Name = "Confirm your Password")]
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

public class UserLoginResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
    public ResponseResult ResponseResult { get; set; }
}

public class UserToken
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaim> Claims { get; set; }
}

public class UserClaim
{
    public string Value { get; set; }
    public string Type { get; set; }
}
