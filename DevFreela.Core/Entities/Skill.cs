using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class Skill
    {
        public Skill(string description, int id)
        {
            Description = description;
            CreatedAt = DateTime.Now;
            Id = id;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
}
