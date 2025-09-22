using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            try
            {
                if (createProject.Title.Length > 50)
                {
                    return BadRequest("O título deve ter no máximo 50 caracteres.");
                }

                return CreatedAtAction(nameof(GetById), new { id = 1 }, null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult Get(string query)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }
    }
}
