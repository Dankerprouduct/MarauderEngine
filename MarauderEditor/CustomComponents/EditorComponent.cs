using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarauderEngine.Core;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D11;
using Point = System.Drawing.Point;

namespace MarauderEditor.CustomComponents
{
    public class EditorComponent: Form
    {
        public static EditorComponent Instance;
        public static Assembly GameAssembly;
        public static string AssemblyPath; 
        public static MarauderComponent MarauderComponent;

        private Type _projectType; 
        public EditorComponent()
        {
            Initialize();
            Instance = this;
            BackColor = ColorScheme.FormColor;
            FormBorderStyle = FormBorderStyle.Sizable;

            Icon = new Icon("Images/MarauderLogo.ico");
        }

        public void StartEditor()
        {
            LoadComponents(OpenProject());
        }

        private string OpenProject()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\danke\\source\\repos\\SpaceMarauders\\SpaceMarauders\\bin\\Windows\\x86\\Debug\\Saves";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}

                    var directoryPath = Path.GetDirectoryName(filePath);

                    AssemblyPath = Path.Combine(directoryPath, filePath);
                    return Path.Combine(directoryPath, filePath);
                }
            };

            return null;
        }

        public void LoadComponents(string path)
        {

            Text = "Marauder Editor";

            int padding = 3; 
            var menuStrip = new MarauderMenuStrip(this);

            var sceneTree = new SceneTree(this);
            sceneTree.Height = Height - menuStrip.Height;
            sceneTree.Location = new Point(0, menuStrip.Height);

            var assetBrowser = new AssetBrowser(this);
            assetBrowser.Width = Width - sceneTree.Width - padding;
            assetBrowser.Location = new Point(sceneTree.Width + padding, Height - assetBrowser.Height);
            //assetBrowser.Visible = false;

            

            var game1 = new Game1(MarauderComponent);
            game1.Location = new Point(sceneTree.Width + padding, menuStrip.Height);
            game1.Size = new Size(Width - sceneTree.Width - 250, Height - assetBrowser.Height - 10 );
            game1.Parent = this;

            var propertyGrid = new EntityProperties(this);
            propertyGrid.Location = new Point(game1.Right, game1.Location.Y);
            propertyGrid.Size = new Size(Width - (game1.Width + sceneTree.Width + (padding * 2 )), Height - assetBrowser.Height - 10);
            
            //propertyGrid.SelectedObject = game1;
            
            Console.WriteLine($"property grid size:  { propertyGrid.Location}");

            Resize += (sender, args) =>
            {
                sceneTree.Height = Height - menuStrip.Height;
                sceneTree.Location = new Point(0, menuStrip.Height);

                assetBrowser.Width = Width - sceneTree.Width - padding;
                assetBrowser.Location = new Point(sceneTree.Width + padding, Height - assetBrowser.Height);

                game1.Location = new Point(sceneTree.Width + padding, menuStrip.Height);
                game1.Size = new Size(Width - sceneTree.Width - 250, Height - assetBrowser.Height - 10);
                game1.Parent = this;
                

                propertyGrid.Location = new Point(game1.Right, game1.Location.Y);
                propertyGrid.Size = new Size(Width - (game1.Width + sceneTree.Width + (padding * 2)), Height - assetBrowser.Height - 10);

                game1.ResizeGameWindow(sender, args);
            };


            GameAssembly = Assembly.LoadFrom(path);
            Assembly.Load(GameAssembly.GetName());
            AppDomain.CurrentDomain.Load(GameAssembly.GetName());

            foreach (Type type in GameAssembly.GetTypes())
            {
                Console.WriteLine($"{type.FullName}    [{type.BaseType}]");
                if (type.BaseType == typeof(MarauderComponent))
                {
                    Game game = new MarauderEngine.Game1();
                    object[] args = new[] { game };
                    var component = Activator.CreateInstance(type, args) as MarauderComponent;

                    TextureManager.Instance = component.TextureManager;
                    TextureManager.FullFolderPath = Path.GetDirectoryName(path);
                    component.LoadTheContent(Game1.Instance.Editor.Content);
                    TextureManager.FullFolderPath = "Content"; 

                    MarauderComponent = component;
                    MarauderComponent.InitializeSceneManagement();
                    game1.MarauderComponent = component;
                    Console.WriteLine("Content: " + TextureManager.Gui.Count);
                  
                }
            }

            assetBrowser.LoadEntities(GameAssembly);
        }

        public void Initialize()
        {
            
        }   
    }
}
