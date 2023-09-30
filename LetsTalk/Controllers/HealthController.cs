using Microsoft.AspNetCore.Mvc;

namespace LetsTalk.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        public string GetHealth(){
            return "Your Service is Reachable";
        }

    }
}
