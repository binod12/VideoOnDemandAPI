using System.Collections.Generic;
using VideoOnDemandAPI.Entities;
using VideoOnDemandAPI.Dtos;

namespace VideoOnDemandAPI.Services
{
  public interface IUserService
  { 
    User Authenticate(string username, string password);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User Create(User user, UserDto userDto);
    void Update(User user, string password = null);
  }
}
