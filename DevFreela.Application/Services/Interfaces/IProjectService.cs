using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectViewModel>> GetAllAsync(string? query);
        Task<ProjecDetailViewModel?> GetByIdAsync(int id);
        Task<int> CreateAsync(NewProjectInputModel inputModel);
        Task UpdateAsync(UpdateProjectInputModel inputModel);
        Task DeleteAsync(int id);
        Task CreateCommentAsync(NewCommentInputModel inputModel);
        Task StartAsync(int id);
        Task FinishAsync(int id);
    }
}
