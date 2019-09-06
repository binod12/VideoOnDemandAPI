using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandAPI.Dtos;
using VideoOnDemandAPI.DataContext;

namespace VideoOnDemandAPI.Helpers
{
  public class AutomapperConfig: Profile
  {
    public AutomapperConfig()
    {
      CreateMap<User, UserDto>();
      CreateMap<UserDto, User>();
    }
  }
}
