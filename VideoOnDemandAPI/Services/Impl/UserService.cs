using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemandAPI.Helpers;
using VideoOnDemandAPI.Models;

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
      var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

      // return null if user not found
      if (user == null)
        return null;

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      // remove password before returning
      user.Password = null;

      return user;
    }

    public IEnumerable<User> GetAll()
    {
      // return users without passwords
      //Todo: Make password as null
      return _context.Users.AsEnumerable().Select(x => { x.Password = null; return x; });
    }

    public User GetById(int id)
    {
      var user = _context.Users.FirstOrDefault(x => x.Id == id);

      // return user without password
      if (user != null)
        user.Password = null;

      return user;
    }
  }
}