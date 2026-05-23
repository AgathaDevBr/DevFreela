using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class UpdateProjectInputModel
    {
        public int Id { get; internal set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public decimal TotalCost { get; internal set; }
    }
}
