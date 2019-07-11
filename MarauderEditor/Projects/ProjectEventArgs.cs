using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEditor.Projects
{
    internal class ProjectEventArgs : EventArgs
    {
        public ProjectInfo Project;

        public ProjectEventArgs(ProjectInfo project)
        {
            Project = project;
        }

    }
}
