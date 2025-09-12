using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistencia
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET CORE 1", "Minha descricao de Projeto 1",1, 1, 10000),
                new Project("Meu projeto ASPNET CORE 2", "Minha descricao de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET CORE 3", "Minha descricao de Projeto 3", 1, 1, 30000),
            };
            Users = new List<User>
            {
                new User("João Augusto", "joao@gmail.com", new DateTime(01/03/1994)),
                new User("Julia Souza","julia@gmail.com", new DateTime(05/07/1994) )
            };
            Skills = new List<Skill>
            { 
                new Skill("C#"),
                new Skill("React")
            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
