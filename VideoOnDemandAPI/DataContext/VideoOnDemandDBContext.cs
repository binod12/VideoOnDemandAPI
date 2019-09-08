using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandAPI.Entities;

namespace VideoOnDemandAPI
{
  public class VideoOnDemandDBContext : DbContext
  {
    public VideoOnDemandDBContext(DbContextOptions<VideoOnDemandDBContext> options) 
      : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public DbSet<Video> Video { get; set; }

    public DbSet<VideoCategory> VideoCategory { get; set; }

  }
}
