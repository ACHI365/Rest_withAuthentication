using System.ComponentModel.DataAnnotations;

namespace Registration.Modules;

public class User
{
    [Key]
    [Required(ErrorMessage="Email Required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }

}