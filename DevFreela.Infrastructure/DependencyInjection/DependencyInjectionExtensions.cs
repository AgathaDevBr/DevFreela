using DevFreela.Core.Messaging;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Messaging;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DevFreelaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DevFreelaCs")));

            services.AddRepositories();
            services.AddRabbitMq(configuration);

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions
            {
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                UserName = configuration["RabbitMQ:UserName"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest",
                QueueName = configuration["RabbitMQ:QueueName"] ?? "project-created"
            };

            services.AddSingleton(options);
            services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

            return services;
        }
    }
}
