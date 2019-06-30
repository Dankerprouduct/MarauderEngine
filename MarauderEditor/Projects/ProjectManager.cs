using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEditor.Utilities.FileManagement;

namespace MarauderEditor.Projects
{
    internal static class ProjectManager
    {

        /// <summary>
        /// Currently loaded project.
        /// </summary>
        public static ProjectInfo CurrentProject;

        /// <summary>
        /// Attemps to create a new project with the given info.
        /// </summary>
        /// <param name="newProjectInfo"></param>
        internal static void CreateNewProject(ProjectInfo newProjectInfo)
        {
            CurrentProject = newProjectInfo;
            FileStructureHelper.CreateProjectFileStructure(newProjectInfo);

            OnProjectCreated(new ProjectEventArgs(newProjectInfo));

        }


        #region  Events

        internal class ProjectEventArgs : EventArgs
        {
            public ProjectInfo Project;

            public ProjectEventArgs(ProjectInfo project)
            {
                Project = project;
            }

        }

        internal static void OnProjectCreated(ProjectEventArgs e)
        {
            ProjectCreatedEvent?.Invoke(e);
        }

        internal delegate void ProjectEventHandler(ProjectEventArgs e);

        /// <summary>
        /// Raised when a new project is created.
        /// </summary>
        public static event ProjectEventHandler ProjectCreatedEvent;

#endregion

    }
}
