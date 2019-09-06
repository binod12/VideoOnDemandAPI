using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoOnDemandAPI.DataContext
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

        //Get the Hashed Password and salt for admin user
        byte[] passwordHash, passwordSalt;
        Helpers.Utility.CreatePasswordHash("admin", out passwordHash, out passwordSalt);

        //Add the admin/superadmin users
        context.Users.AddRange(
         new User { Id = 0, FirstName = "Super Admin", LastName = "User", Username = "admin", Password = "admin", PasswordHash=passwordHash, PasswordSalt=passwordSalt, Role = Role.Admin });

        //context.VideoCategory.AddRange(
        //  new VideoCategory { Id = 1, CategoryName = "Action"},
        //  new VideoCategory { Id = 2, CategoryName = "Comedy" });


        //context.Video.AddRange(
        //  new Video { Id = 1, VideoCategoryId = 1, Description = "Video Description", Title = "Titanic" },
        //  new Video { Id = 2, VideoCategoryId = 2, Description = "Video Description2", Title = "Avatar" });

        context.SaveChanges();
      }
    }
  }
}
