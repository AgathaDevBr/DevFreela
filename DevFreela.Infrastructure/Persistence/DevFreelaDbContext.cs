using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options)
           : base(options)
        {
            Projects = new List<Project> {
                new Project("Meu projeto ASPNET CORE 1", "Minha descrição de projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET CORE 2", "Minha descrição de projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET CORE 3", "Minha descrição de projeto 3", 1, 1, 30000)
            };
            Users = new List<User> {
                new User("Ágatha Almeida", "agathasantos@gmail.com", new DateTime(2001,3,3)),
                 new User("Ester Mota", "esterMotta@gmail.com", new DateTime(2000,23,11)),
                  new User("Justin Bieber", "justinbieber@gmail.com", new DateTime(1999,1,3))
            };
            Skills = new List<Skill>
            {
                new Skill(".NET CORE", 1),
                new Skill("ANGULAR", 2),
                new Skill("SQL", 3),
            };
        }
        public List<ProjectComment> Comments { get; set; }
        public List<Project> Projects {  get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get;  set; }
    }
}
