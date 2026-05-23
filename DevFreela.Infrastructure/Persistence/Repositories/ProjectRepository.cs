using DevFreela.Core.Entities;
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

        public Task AddCommentAsync(ProjectComment comment)
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Project?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
