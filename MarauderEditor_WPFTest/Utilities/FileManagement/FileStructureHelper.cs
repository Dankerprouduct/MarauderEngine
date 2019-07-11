using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEditor.Projects;

namespace MarauderEditor.Utilities.FileManagement
{

    internal static class FileStructureHelper
    {

        #region Editor File Paths

        public static readonly string EditorDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Marauder Engine";
        public static readonly string EditorProjectsPath = EditorDocumentsPath + $"\\Projects";

        private static readonly List<string> EditorFileStructureFolders = new List<string>()
        {
            EditorDocumentsPath,
            EditorProjectsPath
        };

        #endregion

        #region Project File Paths

        public const string ContentFolderPath = "\\Content";
        public const string ScriptsFolderPath = "\\Scripts";


        private static readonly List<string> ProjectFileStructureFolders = new List<string>()
        {
            "ProjectRootPlaceholder",
            ContentFolderPath,
            ScriptsFolderPath
        };
        #endregion

        internal static void Initialize()
        {
            ProjectManager.ProjectCreatedEvent += HandleProjectCreated;
        }

        private static void HandleProjectCreated(ProjectEventArgs e)
        {
            CreateProjectFileStructure(e.Project);
            CreateProjectFile(e.Project);
        }

        /// <summary>
        /// Creates editor file structure if it does not already exist. Must be called when editor is opened.
        /// </summary>
        internal static void CreateEditorFileStructure()
        {
            GenerateFileStructure(EditorFileStructureFolders);
        }

        /// <summary>
        /// Creates project file structure if it does not already exist.
        /// </summary>
        /// <param name="projectInfo"></param>
        private static void CreateProjectFileStructure(ProjectInfo projectInfo)
        {
            ProjectFileStructureFolders[0] = projectInfo.ProjectRoot;
            for (var i = 1; i < ProjectFileStructureFolders.Count; i++)
            {
                ProjectFileStructureFolders[i] = projectInfo.ProjectRoot + ProjectFileStructureFolders[i];
            }
            GenerateFileStructure(ProjectFileStructureFolders);
        }

        /// <summary>
        /// Generate any missing folders in file structure (Ex: Saves, Scenarios).
        /// </summary>
        private static void GenerateFileStructure(List<string> foldersList)
        {
            foreach (var folder in foldersList)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

        }

        private static void CreateProjectFile(ProjectInfo projectInfo)
        {
            IOHelper.SaveFile(projectInfo.ToJson(), projectInfo.ProjectRoot, projectInfo.ProjectFileName);
        }

        /// <summary>
        /// Check for missing folders in file structure, and generate any missing entries. Run on project load.
        /// </summary>
        private static void CheckFileStructure(List<string> foldersList)
        {
            foreach (var folder in foldersList)
            {
                if (!Directory.Exists(folder))
                {
                    GenerateFileStructure(foldersList);
                    break;
                }
            }
        }

    }
}
