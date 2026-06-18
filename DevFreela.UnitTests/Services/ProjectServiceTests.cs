using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Implementations;
using DevFreela.Core.Entities;
using DevFreela.Core.Events;
using DevFreela.Core.Messaging;
using DevFreela.Core.Repositories;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Services
{
    public class ProjectServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateProjectAndPublishIntegrationEvent()
        {
            var repository = new Mock<IProjectRepository>();
            var messageBus = new Mock<IMessageBus>();
            Project? createdProject = null;

            repository
                .Setup(r => r.CreateAsync(It.IsAny<Project>()))
                .Callback<Project>(project => createdProject = project)
                .ReturnsAsync(10);

            var service = new ProjectService(repository.Object, messageBus.Object);
            var inputModel = new NewProjectInputModel
            {
                Title = "API ASP.NET Core",
                Description = "Criar API do DevFreela",
                ClientId = 1,
                FreelancerId = 2,
                TotalCost = 1500m
            };

            var projectId = await service.CreateAsync(inputModel);

            Assert.Equal(10, projectId);
            Assert.NotNull(createdProject);
            Assert.Equal(inputModel.Title, createdProject!.Tittle);
            repository.Verify(r => r.CreateAsync(It.IsAny<Project>()), Times.Once);
            messageBus.Verify(
                b => b.PublishAsync(
                    "project-created",
                    It.Is<ProjectCreatedIntegrationEvent>(e => e.ProjectId == 10 && e.Title == inputModel.Title),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProjectDoesNotExist()
        {
            var repository = new Mock<IProjectRepository>();
            var messageBus = new Mock<IMessageBus>();

            repository
                .Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Project?)null);

            var service = new ProjectService(repository.Object, messageBus.Object);

            var project = await service.GetByIdAsync(99);

            Assert.Null(project);
        }
    }
}
