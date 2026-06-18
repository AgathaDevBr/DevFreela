using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class NewProjectInputModel
    {
        public string Title { get; set; } = string.Empty;
        public int FreelancerId { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
    }
}
