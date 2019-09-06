using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemandAPI.Dtos
{
  public class UserDto
  {
    [Required]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string EmailId { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string RepeatPassword { get; set; }
  }
}
