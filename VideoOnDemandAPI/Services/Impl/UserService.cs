using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using VideoOnDemandAPI.Helpers;
using VideoOnDemandAPI.DataContext;
using VideoOnDemandAPI.Dtos;

namespace VideoOnDemandAPI.Services.Impl
{
  public class UserService: IUserService
  {
    private readonly VideoOnDemandDBContext _context;

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings, VideoOnDemandDBContext context)
    {
      _appSettings = appSettings.Value;
      _context = context;
    }

    public User Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = _context.Users.SingleOrDefault(x => x.Username == username);

      // check if username exists
      if (user == null)
        return null;

      // check if password is correct
      if (!Helpers.Utility.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful
      return user;
    }

    public IEnumerable<User> GetAll()
    {
      // return users without passwords
      //Make password related properties  as null
      return _context.Users.AsEnumerable().Select(user => { user.Password = null; user.PasswordHash = null; user.PasswordSalt = null; return user; });
    }

    public User GetById(int id)
    {
      var user = _context.Users.FirstOrDefault(x => x.Id == id);

      // return user without password related properties
      if (user != null)
      {
        user.Password = null;
        user.PasswordHash = null;
        user.PasswordSalt = null;
      }

      return user;
    }

    public User Create(User user, UserDto userDto)
    {
      // validation
      if (string.IsNullOrWhiteSpace(userDto.Password))
        throw new CustomException("Password is required");

      if (userDto.Password != userDto.RepeatPassword)
        throw new CustomException("User Password and Repeat password does not match");

      if (_context.Users.Any(x => x.Username == user.Username))
        throw new CustomException("Username \"" + user.Username + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      Helpers.Utility.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public void Update(User userParam, string password = null)
    {
      var user = _context.Users.Find(userParam.Id);

      if (user == null)
        throw new CustomException("User does not exist");

      if (userParam.Username != user.Username)
      {
        // username has changed so check if the new username is already taken
        if (_context.Users.Any(x => x.Username == userParam.Username))
          throw new CustomException("Username " + userParam.Username + " is already taken");
      }

      // update user properties
      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.Username = userParam.Username;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        Helpers.Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _context.Users.Update(user);
      _context.SaveChanges();
    }
  }
}