using Microsoft.AspNetCore.Mvc;

namespace VideoOnDemandAPI.Api
{

  /// <summary>
  /// Base API Controller: Common routing, Api Filters/Attributes can be applied here
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class BaseApiController : ControllerBase
  {
  }
}