using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEditor.Projects
{
    public class ProjectInfo
    {

        public string ProjectName = "Test Project";

        public string ProjectFileName => ProjectName + ".mproj";

        public string ProjectRoot => System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Marauder Engine\\{ProjectName}";

        public ProjectInfo()
        {

        }

    }
}
