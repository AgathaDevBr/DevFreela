using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Exceptions
{
    public class ProjectAlreadyStarttedException : Exception
    {
        public ProjectAlreadyStarttedException() : base("Project already started and cannot be started again.") { }
    }
}
