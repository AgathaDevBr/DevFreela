using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            return Ok();
        }
    }
}
