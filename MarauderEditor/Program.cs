using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEditor.Utilities.FileManagement;

namespace MarauderEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PreEditorInit();
            Application.Run(new EditorWindow());
        }

        private static void PreEditorInit()
        {
            FileStructureHelper.Initialize();

        }
    }
}
