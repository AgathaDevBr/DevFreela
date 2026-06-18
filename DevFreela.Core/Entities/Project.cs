using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        private Project()
        {
            Tittle = string.Empty;
            Description = string.Empty;
            Comments = new List<ProjectComment>();
        }

        public Project(string tittle, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            Tittle = tittle;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;

            CreatedAt = DateTime.UtcNow;
            Status = ProjectStatusEnum.Created;
            Comments = new List<ProjectComment> ();

        }

        public string Tittle { get; private set; }
        public string Description { get; private  set; }
        public int IdClient { get; private  set; }
        public int IdFreelancer {get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
        public void Cancel()
        {
            if (Status == ProjectStatusEnum.Created ||
                Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Cancelled;
            }
        }

        public void Finish()
        {
            if(Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Finished;
                FinishedAt = DateTime.UtcNow;
            }
        }

        public void Start()
        {
           if(Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.UtcNow;
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Tittle = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}
