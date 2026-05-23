using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {

            var project = new Project(inputModel.Title, inputModel.Description, inputModel.ClientId, inputModel.FreelancerId, inputModel.TotalCost);
            _dbContext.Projects.Add(project);
            return project.Id;
        }

        public void CreateComment(NewCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.Comments.Add(comment);
        }

        public void Delete(int id)
        {
            var projet = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            projet.Cancel();
            _dbContext.Projects.Remove(projet);
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

        }
        public List<ProjectViewModel> GetAll(string query)
        {
            var project = _dbContext.Projects;

            var projectsViewModel = project
                .Select(p => new ProjectViewModel(p.Id, p.Tittle, p.CreatedAt)).ToList();

            return projectsViewModel;
        }

        public ProjecDetailViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            var dto = new ProjecDetailViewModel(
                project.Id,
                project.Tittle,
                project.Description,
                project.TotalCost,
                project.CreatedAt,
                project.FinishedAt);
            return dto;
        }

        public void Finish(int id)
        {
            var projet = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            projet.Finish();
        }
        public void Start(int id)
        {
            var projet = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            projet.Start();
        }

    }
}
