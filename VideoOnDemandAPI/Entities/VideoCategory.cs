using System.ComponentModel.DataAnnotations;

namespace VideoOnDemandAPI.DataContext
{
  public class VideoCategory: EntityBase
  {
    [Required]
    public string CategoryName { get; set; }

  }
}
