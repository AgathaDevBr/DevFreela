using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevFreela.Application.InputModels
{
    public class UpdateProjectInputModel
    {
        public int Id { get; internal set; }
        public string Tittle { get; internal set; }
        public string Description { get; internal set; }
        public decimal TotalCost { get; internal set; }
        public int IdClient {  get; internal set; }
        public int IdFreelancer { get; internal set; }

    }
}
