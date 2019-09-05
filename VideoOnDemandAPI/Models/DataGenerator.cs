using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemandAPI.Models
{
  public class DataGenerator
  {

    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new VideoOnDemandDBContext(
          serviceProvider.GetRequiredService<DbContextOptions<VideoOnDemandDBContext>>()))
      {
        // Look for any existing users.
        if (context.Users.Any())
        {
          return;   // Database has been seeded
        }

        context.Users.AddRange(
         new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
         new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User });

        context.VideoCategory.AddRange(
          new VideoCategory { Id = 1, CategoryName = "Action"},
          new VideoCategory { Id = 2, CategoryName = "Comedy" });


        context.Video.AddRange(
          new Video { Id = 1, VideoCategoryId = 1, Description = "Video Description", Title = "Titanic" },
          new Video { Id = 2, VideoCategoryId = 2, Description = "Video Description2", Title = "Avatar" });

        context.SaveChanges();
      }
    }
  }
}
