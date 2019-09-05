using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemandAPI.Dtos;
using VideoOnDemandAPI.Helpers;
using VideoOnDemandAPI.Models;
using VideoOnDemandAPI.Services;

namespace VideoOnDemandAPI.Api
{
  [Authorize]
  public class UsersController : BaseApiController
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;


    public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
    {
      _userService = userService;
      _mapper = mapper;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]UserLogOnDto logOnDto)
    {
      var user = _userService.Authenticate(logOnDto.Username, logOnDto.Password);

      if (user == null)
        return BadRequest(new { message = "Invalid Username or Password" });

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info (without password) 
      return Ok(new
      {
        Id = user.Id,
        Username = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Role = user.Role,
        Token = tokenString
      });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserDto userDto)
    {
      // map dto to entity
      var user = _mapper.Map<User>(userDto);

      try
      {
        // save 
        _userService.Create(user, userDto.Password);
        return Ok();
      }
      catch (CustomException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [Authorize(Roles = Role.Admin)]
    [HttpPost("registeradmin")]
    public IActionResult RegisterAdmin([FromBody]UserLogOnDto userDto)
    {
      // map dto to entity
      var user = _mapper.Map<User>(userDto);

      try
      {
        // save 
        user.Role = Role.Admin;
        _userService.Create(user, userDto.Password);
        return Ok();
      }
      catch (CustomException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    //[Authorize(Roles = Role.Admin)]
    [HttpGet]
    public IActionResult GetAll()
    {
      var users = _userService.GetAll();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _userService.GetById(id);

      if (user == null)
      {
        return NotFound();
      }

      // only allow admins to access other user records
      var currentUserId = int.Parse(User.Identity.Name);
      if (id != currentUserId && !User.IsInRole(Role.Admin))
      {
        return Forbid();
      }

      return Ok(user);
    }
  }
}
