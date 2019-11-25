using Microsoft.AspNetCore.Mvc;

namespace TeamJob.Services.Profile.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get() => Ok("TeamJob Profile Service");
    }
}
