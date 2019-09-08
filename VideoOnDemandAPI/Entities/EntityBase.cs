using System.ComponentModel.DataAnnotations;

namespace VideoOnDemandAPI.Entities
{
  public class EntityBase
  {
    [Key]
    public int Id { get; set; }

  }
}
