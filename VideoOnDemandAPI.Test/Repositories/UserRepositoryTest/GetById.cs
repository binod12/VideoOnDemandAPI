using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VideoOnDemandAPI.Entities;
using Xunit;
using Xunit.Abstractions;

namespace VideoOnDemandAPI.Test.Repositories.UserRepositoryTest
{
  public class GetById
  {
    private readonly VideoOnDemandDBContext context;
    private readonly ITestOutputHelper _output;

    public GetById(ITestOutputHelper output)
    {
      _output = output;
      var dbOptions = new DbContextOptionsBuilder<VideoOnDemandDBContext>()
                      .UseInMemoryDatabase(databaseName: "TestUsers")
                      .Options;
      context = new VideoOnDemandDBContext(dbOptions);
    }

    [Fact]
    public async Task GetExistingUser()
    {
      context.Users.Add(new User
      {
        Username = "admin",
        Id=1,
        EmailId="binod12@gmail.com",
        Password="Test",
        LastName="admin"
      });

      context.SaveChanges();

      int userId = 1;
      var user = await context.Users.FindAsync(userId);
      Assert.Equal(userId, user.Id);
    }

  }
}
