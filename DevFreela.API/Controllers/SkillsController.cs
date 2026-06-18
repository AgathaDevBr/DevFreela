using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills);
        }
    }
}
