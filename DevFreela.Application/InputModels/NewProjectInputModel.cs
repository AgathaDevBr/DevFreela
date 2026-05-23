using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class NewProjectInputModel
    {
        public string Title { get; set; }
        public int FreelancerId { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}
