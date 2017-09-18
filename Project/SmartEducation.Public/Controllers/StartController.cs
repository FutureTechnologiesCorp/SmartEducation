using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SmartEducation.Public.Controllers
{
  [Route("api/[controller]")]
  public class StartController
  {
    [HttpGet]
    public IEnumerable<string> Index()
    {
      return new string[] { "A", "N", "G", "U", "L", "A", "R", " 2" };
    }
  }
}
