using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemandAPI.Models
{
  public class Video: EntityBase
  {
    public string Title { get; set; }

    public string Description { get; set; }

    public VideoCategory Category { get; set; }

    public double Price { get; set; }
  }
}
