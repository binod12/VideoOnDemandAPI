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
        // Look for any board games already in database.
        if (context.Users.Any())
        {
          return;   // Database has been seeded
        }

        context.Users.AddRange(
         new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
         new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User });

        context.SaveChanges();
      }
    }
  }
}
