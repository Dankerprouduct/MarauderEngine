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
            //FileStructureHelper.CreateProjectFileStructure(newProjectInfo);

            OnProjectCreated(new ProjectEventArgs(newProjectInfo));

        }

        internal static void LoadProject(ProjectInfo projectInfo)
        {
            CurrentProject = projectInfo;
            //FileStructureHelper.CreateProjectFileStructure(projectInfo);

            OnProjectLoaded(new ProjectEventArgs(projectInfo));

        }


        #region  Events


        internal static void OnProjectCreated(ProjectEventArgs e)
        {
            ProjectCreatedEvent?.Invoke(e);
        }

        internal static void OnProjectLoaded(ProjectEventArgs e)
        {
            ProjectLoadedEvent?.Invoke(e);
        }

        internal delegate void ProjectEventHandler(ProjectEventArgs e);

        /// <summary>
        /// Raised when a new project is created.
        /// </summary>
        public static event ProjectEventHandler ProjectCreatedEvent;

        public static event ProjectEventHandler ProjectLoadedEvent;

        #endregion




    }


}
