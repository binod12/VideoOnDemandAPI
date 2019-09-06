using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemandAPI.DataContext
{
  public class User : EntityBase
  {
    [Required]
    public string FirstName { get; set; }

    [Required]

    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string EmailId { get; set; }

    public string Password { get; set; }

    [Required]
    public string Role { get; set; }

    public string Token { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
  }
}
