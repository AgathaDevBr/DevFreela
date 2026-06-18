using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Events;
using DevFreela.Core.Messaging;
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private const string ProjectCreatedQueue = "project-created";

        private readonly IProjectRepository _projectRepository;
        private readonly IMessageBus _messageBus;

        public ProjectService(IProjectRepository projectRepository, IMessageBus messageBus)
        {
            _projectRepository = projectRepository;
            _messageBus = messageBus;
        }

        public async Task<int> CreateAsync(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.ClientId, inputModel.FreelancerId, inputModel.TotalCost);
            var projectId = await _projectRepository.CreateAsync(project);

            await _messageBus.PublishAsync(
                ProjectCreatedQueue,
                new ProjectCreatedIntegrationEvent(
                    projectId,
                    inputModel.Title,
                    inputModel.ClientId,
                    inputModel.FreelancerId,
                    inputModel.TotalCost));

            return projectId;
        }

        public async Task CreateCommentAsync(NewCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            await _projectRepository.AddCommentAsync(comment);
        }

        public async Task DeleteAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UpdateProjectInputModel inputModel)
        {
            var project = await _projectRepository.GetByIdAsync(inputModel.Id);

            if (project is null)
            {
                return;
            }

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            await _projectRepository.UpdateAsync(project);
        }

        public async Task<List<ProjectViewModel>> GetAllAsync(string? query)
        {
            var projects = await _projectRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects
                    .Where(p => p.Tittle.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return projects
                .Select(p => new ProjectViewModel(p.Id, p.Tittle, p.CreatedAt))
                .ToList();
        }

        public async Task<ProjecDetailViewModel?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project is null)
            {
                return null;
            }

            return new ProjecDetailViewModel(
                project.Id,
                project.Tittle,
                project.Description,
                project.TotalCost,
                project.CreatedAt,
                project.StartedAt,
                project.FinishedAt);
        }

        public async Task FinishAsync(int id)
        {
            await _projectRepository.CompleteAsync(id);
        }

        public async Task StartAsync(int id)
        {
            await _projectRepository.StartAsync(id);
        }
    }
}
