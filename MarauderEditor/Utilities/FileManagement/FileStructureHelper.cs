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

        #region File Paths

        private const string ContentFolderPath = "\\Content";
        private const string ScriptsFolderPath = "\\Scripts";


        private static List<string> FileStructureFolders = new List<string>()
        {
            "ProjectRootPlaceholder",
            ContentFolderPath,
            ScriptsFolderPath
        };
        #endregion


        static void HandleProjectCreated(ProjectManager.ProjectEventArgs e)
        {
            CreateProjectFileStructure(e.Project);
        }



        internal static void CreateProjectFileStructure(ProjectInfo projectInfo)
        {
            FileStructureFolders[0] = projectInfo.ProjectRoot;
            for (var i = 1; i < FileStructureFolders.Count; i++)
            {
                FileStructureFolders[i] = projectInfo.ProjectRoot + FileStructureFolders[i];
            }
            GenerateFileStructure();
        }

        /// <summary>
        /// Generate any missing folders in file structure (Ex: Saves, Scenarios).
        /// </summary>
        public static void GenerateFileStructure()
        {
            foreach (var folder in FileStructureFolders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

        }

        public static void CreateProjectFile()
        {

        }

        /// <summary>
        /// Check for missing folders in file structure, and generate any missing entries. Run on project load.
        /// </summary>
        internal static void CheckFileStructure()
        {
            foreach (var folder in FileStructureFolders)
            {
                if (!Directory.Exists(folder))
                {
                    GenerateFileStructure();
                    break;
                }
            }
        }

    }
}
