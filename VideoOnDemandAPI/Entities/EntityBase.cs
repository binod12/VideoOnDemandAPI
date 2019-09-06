using System.ComponentModel.DataAnnotations;

namespace VideoOnDemandAPI.DataContext
{
  public class EntityBase
  {
    [Key]
    public int Id { get; set; }

  }
}
