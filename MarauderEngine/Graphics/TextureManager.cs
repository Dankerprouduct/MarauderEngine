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
        public static Dictionary<string, List<Texture2D>> TextureLists = new Dictionary<string, List<Texture2D>>();
        public static List<Texture2D> Tiles = new List<Texture2D>();
        public static List<SpriteFont> Fonts = new List<SpriteFont>();
        public static List<Texture2D> Gui = new List<Texture2D>();
        public static List<Texture2D> Sprites = new List<Texture2D>();
        public static List<Texture2D> Torsos = new List<Texture2D>();
        public static List<Texture2D> BodyParts = new List<Texture2D>();
        public static List<Texture2D> Particles = new List<Texture2D>();
        public static List<Texture2D> WorldItems = new List<Texture2D>();
        public static List<Texture2D> GuiItemTextures = new List<Texture2D>();
        public static List<Texture2D> GraphicsTextures = new List<Texture2D>();
        public static List<Texture2D> Decorations = new List<Texture2D>();
        public static string FullFolderPath = "Content";
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, object> ContentDictionary = new Dictionary<string, object>();

        public struct TileSet
        {
            public string Name;
            public List<Texture2D> Type1;
            public List<Texture2D> Type2;
            public List<Texture2D> Type3;
            public List<Texture2D> Type4;
            public List<Texture2D> Type5;
            public List<Texture2D> Type6;
            public List<Texture2D> Type7;
            public List<Texture2D> Type8;
            public List<Texture2D> Type9;
            public List<Texture2D> Type10;
            public List<Texture2D> Elbow;

        }
        public static Dictionary<string, TileSet> TileSets = new Dictionary<string, TileSet>();

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

        [System.Obsolete()]
        public static void AddTexture(string folder, string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                _textures.Add(name, content.Load<Texture2D>(folder + "/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("could not find texture of name " + name);
            }
        }

        [System.Obsolete()]
        public static Texture2D GetTexture(string name)
        {
            return _textures[name]; 
        }

        public static void AddFont(string folder, string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                _fonts.Add(name, content.Load<SpriteFont>(folder + "/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("could not find font of name " + name);
            }
        }

        public static void AddTile(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Tiles.Add(content.Load<Texture2D>("Tiles/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("could not find tile of name " + name);
            }
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

        /// <summary>
        /// Loads texture from Sprites/ folder
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public static void AddSprite(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Sprites.Add(content.Load<Texture2D>("Sprites/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find sprite " + name);
            }
        }

        public static void AddBodyPart(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                BodyParts.Add(content.Load<Texture2D>("Sprites/BodyParts/" + name));
            }
            catch
            {
                throw  new Exception("could not find body part " + name);
            }
        }

        
        public static void AddParticle(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Particles.Add(content.Load<Texture2D>("Particles/" + name));
            }
            catch (Exception ex)
            {
                throw  new Exception("could not find particle texture " + name);
            }
        }

        public static void AddWorldItemTexture(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                WorldItems.Add(content.Load<Texture2D>("Sprites/Items/" + name));
            }
            catch (Exception ex)
            {
                throw  new Exception("could not find world item texture" + name);
            }
        }

        public static void AddGuiItem(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                GuiItemTextures.Add(content.Load<Texture2D>("GUI/Items/" + name));
            }
            catch (Exception ex)
            {
                throw  new Exception("could not find gui item " + name);
            }
        }

        public static void AddGraphicTexture(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                GraphicsTextures.Add(content.Load<Texture2D>("Graphics/" + name));
            }
            catch (Exception ex)
            {
                throw new Exception("could not find graphic texture " + name);
            }
        }

        public static void AddDecoration(string name, ContentManager content)
        {
            try
            {
                Console.WriteLine("Loading " + name);
                Decorations.Add(content.Load<Texture2D>("Decorations/" + name));
            }
            catch (Exception ex)
            {
                throw  new Exception("could not find decoration "+ name);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="desiredColor">in a float range between 0 and 1</param>
        /// <returns></returns>
        public static Texture2D EditTextureColor(Texture2D texture, Vector4 desiredColor)
        {
            Color[] textureColor = GetTextureData(texture);

            Color[] newTexture = new Color[textureColor.Length];
            //color.Normalize();

            for (int i = 0; i < textureColor.Length; i++)
            {
                newTexture[i] = new Color(textureColor[i].ToVector4() * desiredColor);
            }

            texture.SetData<Color>(newTexture);
            return texture;

        }

        public static Texture2D EditTextureColor(Texture2D texture, int basePower, Vector4 desiredColor)
        {
            Color[] textureColor = GetTextureData(texture);

            Color[] newTexture = new Color[textureColor.Length];
            //color.Normalize();

            for (int i = 0; i < textureColor.Length; i++)
            {
                Vector4 vector = textureColor[i].ToVector4().Power(basePower); 
                               
                newTexture[i] = new Color(vector * desiredColor);

            }



            texture.SetData<Color>(newTexture);
            return texture;

        }

        public override void Initialize()
        {
            
        }
    }
}
