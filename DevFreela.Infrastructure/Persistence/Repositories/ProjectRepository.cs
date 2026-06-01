using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
             _dbContext.Comments.Add(comment);
             await _dbContext.SaveChangesAsync();
        }

        public async Task CompleteAsync(int id)
        {
            var project =  _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project != null)
            {

                project.Finish();
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<int> CreateAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return Task.FromResult(project.Id);
        }

        public Task DeleteAsync(int id)
        {
            if(id != null) throw new Exception("O id do projeto não pode ser nulo");
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            _dbContext.Projects.Remove(project);
            return Task.CompletedTask;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var listProjects = _dbContext.Projects.ToList();
            return listProjects;
        }

        public Task<Project?> GetByIdAsync(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            return Task.FromResult(project);
        }

        public Task StartAsync(int id)
        {
            var projetActive = _dbContext.Projects.Where(p => p.Status == ProjectStatusEnum.InProgress && p.Id == id).FirstOrDefault().Start;
            _dbContext.Update(projetActive);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Project project)
        {
            var updateProjetct = _dbContext.Projects.Where(p => p.Id == project.Id).FirstOrDefault();
            updateProjetct.Update(project.Tittle, project.Description, project.TotalCost);
            _dbContext.Update(updateProjetct);
            return Task.CompletedTask;
        }
    }
}
