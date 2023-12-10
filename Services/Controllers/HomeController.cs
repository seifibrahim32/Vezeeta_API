using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization; 

namespace Services.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly IStringLocalizer<HomeController> _StringLocalizer; 

        public HomeController(IStringLocalizer<HomeController> _stringLocalizer)
        {
            _StringLocalizer = _stringLocalizer; 
        }

        [HttpGet , Route("Welcome")]
        public IActionResult Hello()
        {
            var article = _StringLocalizer["Welcome"];
            return Ok(new { Greeting = article.Value });
        }
    }
}
