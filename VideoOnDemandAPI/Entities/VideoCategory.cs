using System.ComponentModel.DataAnnotations;

namespace VideoOnDemandAPI.Entities
{
  public class VideoCategory: EntityBase
  {
    [Required]
    public string CategoryName { get; set; }

  }
}
