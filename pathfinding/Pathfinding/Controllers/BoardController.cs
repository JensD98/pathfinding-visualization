using Microsoft.AspNetCore.Mvc;

namespace Pathfinding.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BoardController : ControllerBase
  {
    [HttpGet]
    public string Get()
    {
      return "GET";
    }

    [HttpPost]
    public string Create()
    {
      return "POST";
    }
  }
}