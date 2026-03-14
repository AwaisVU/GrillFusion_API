using GrillFusion_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrillFusion_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> GetSomething()
        {
            return "You are authorized user";
        }

        [HttpGet("{someValue:int}")]
        [Authorize(Roles =SD.Role_Admin)]
        public ActionResult<string> GetSomething(int someValue)
        {
            return "You are authorized user, with role of Admin";
        }
    }
}
