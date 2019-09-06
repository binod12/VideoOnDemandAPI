using System;
using System.ComponentModel.DataAnnotations;

namespace VideoOnDemandAPI.DataContext
{
  public class Video: EntityBase
  {
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public int VideoCategoryId { get; set; }

    public double Price { get; set; }
  }
}
