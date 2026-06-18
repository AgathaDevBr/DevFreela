using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var projects = await _projectService.GetAllAsync(query);
            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Create(NewProjectInputModel inputModel)
        {
            var id = await _projectService.CreateAsync(inputModel);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Update(int id, UpdateProjectInputModel inputModel)
        {
            inputModel.Id = id;
            await _projectService.UpdateAsync(inputModel);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id:int}/comments")]
        [Authorize]
        public async Task<IActionResult> CreateComment(int id, NewCommentInputModel inputModel)
        {
            inputModel.IdProject = id;
            await _projectService.CreateCommentAsync(inputModel);

            return NoContent();
        }

        [HttpPut("{id:int}/start")]
        [Authorize(Roles = "Admin,Freelancer")]
        public async Task<IActionResult> Start(int id)
        {
            await _projectService.StartAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}/finish")]
        [Authorize(Roles = "Admin,Freelancer")]
        public async Task<IActionResult> Finish(int id)
        {
            await _projectService.FinishAsync(id);
            return NoContent();
        }
    }
}
