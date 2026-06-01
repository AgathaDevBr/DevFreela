using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _dbContext;

        public ProjectService(IProjectRepository dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {

            var project = new Project(inputModel.Title, inputModel.Description, inputModel.ClientId, inputModel.FreelancerId, inputModel.TotalCost);
            _dbContext.CreateAsync(project);
            return project.Id;
        }

        public void CreateComment(NewCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.AddCommentAsync(comment);
        }

        public void Delete(int id)
        {
           _dbContext.DeleteAsync(id);
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            _dbContext.UpdateAsync(new Project(inputModel.Tittle, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost));

        }
        public List<ProjectViewModel> GetAll(string query)
        {
            var project = _dbContext.GetAllAsync().Result;

            var projectsViewModel = project
                .Select(p => new ProjectViewModel(p.Id, p.Tittle, p.CreatedAt)).ToList();

            return projectsViewModel;
        }

        public ProjecDetailViewModel GetById(int id)
        {
            var project = _dbContext.GetByIdAsync(id).Result;
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
            _dbContext.CompleteAsync(id);
        }
        public void Start(int id)
        {
            _dbContext.StartAsync(id);
        }

    }
}
