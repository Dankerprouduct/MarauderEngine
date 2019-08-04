using System;
using System.Collections.Generic;
using System.IO;
using MarauderEngine.Systems;
using MarauderEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Graphics
{
    public class TextureManager : SystemManager<TextureManager>
    {

        public static string FullFolderPath = "Content";
        public static List<SpriteFont> Fonts = new List<SpriteFont>();
        public static List<Texture2D> Gui = new List<Texture2D>();
        public static Dictionary<string, object> ContentDictionary = new Dictionary<string, object>();

        public static void LoadContent(ContentManager content)
        {

        }

        #region Loading Methods

        /// <summary>
        /// loads folder into _content dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folder"></param>
        /// <param name="content"></param>
        public static void LoadFolder<T>(string folder, ContentManager content)
        {
            DirectoryInfo dir = new DirectoryInfo("C:/");
            if (FullFolderPath != "Content")
            {
                dir = new DirectoryInfo(FullFolderPath + @"\" +  content.RootDirectory + @"\" + folder);
            }
            else
            {
                dir = new DirectoryInfo(content.RootDirectory + "/" + folder);
            }
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException();
                //dir = new DirectoryInfo(folder);
            }

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (var file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);   
                
                if(FullFolderPath == "Content")
                    ContentDictionary.Add(key, content.Load<T>(folder + "/" + key));
                if (FullFolderPath != "Content")
                    ContentDictionary.Add(key, content.Load<T>(file.Directory + "/" + key));
            }
        }

        public static void LoadContent<T>(string folderPath, string name, ContentManager content)
        {
            ContentDictionary.Add(name, content.Load<T>(folderPath + "/" + name));
        }

        /// <summary>
        /// returns content given the key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetContent<T>(string name)
        {
            return (T)ContentDictionary[name];
        }

        /// <summary>
        /// returns whether or not content by the given key exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ContainsContent(string name)
        {
            return ContentDictionary.ContainsKey(name);
        }
        
        /// <summary>
        /// Adds Font
        /// Make sure to add a folder called "Fonts" before calling this method
        /// </summary>
        /// <param name="name">name of the font </param>
        /// <param name="content"></param>
        public static void AddFont(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Fonts.Add(content.Load<SpriteFont>("Fonts/" + name));
            }
            catch (Exception ex)
            {
                throw  new Exception("could not find font " + name);
            }
        }

        public static void AddGui(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Gui.Add(content.Load<Texture2D>("GUI/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("could not find gui element " + name);
            }
        }

        #endregion


        public static Vector4 NormalizeColors(Color color)
        {
            return color.ToVector4(); 
        }
        public static Color[] GetTextureData(Texture2D texture)
        {
            Color[] tcolor = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(tcolor);
            return tcolor; 
        }

        public static Texture2D EditTextureColor(Texture2D texture, Color desiredColor)
        {
           Color[] textureColor = GetTextureData(texture);

            Color[] newTexture = new Color[textureColor.Length];
            Vector4 color = desiredColor.ToVector4();

            for (int i = 0; i < textureColor.Length; i++)
            {
                newTexture[i] = new Color(textureColor[i].ToVector4() * color);
            }

           

            texture.SetData<Color>(newTexture);
            return texture; 

        }

        public override void Initialize()
        {
            
        }
    }
}
