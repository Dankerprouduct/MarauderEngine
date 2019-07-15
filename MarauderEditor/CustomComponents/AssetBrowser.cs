using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MarauderEngine.Entity;
using MarauderEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEditor.CustomComponents
{
    public class AssetBrowser : ListView
    {
        public static AssetBrowser Instance;

        public struct Asset
        {
            public string Key;
            public Bitmap Bitmap;
        }
        public AssetBrowser(Control parent)
        {
            Instance = this;
            Parent = parent;
            BackColor = ColorScheme.ComponentColor;
            ForeColor = Color.White;
            BorderStyle = BorderStyle.None;
            Font = new Font("Calibri", 12, FontStyle.Regular);
            Height = 250;
            
            //Anchor = AnchorStyles.Bottom;
            
            View = View.Tile;

            //var label = new Label();
            //label.Text = "AssetBrowser";
            //label.Parent = this;
            //label.Location = new Point(10, -10);
            //label.Size = new Size(100, 24);
            //label.ForeColor = Color.White;
            //label.BackColor = Color.Transparent;


            this.ItemSelectionChanged += (sender, args) =>
            {
                EntityProperties.Instance.LoadEntity((args.Item as EntityViewItem).Entity.EntityData );
                if (TransformMovement.Instance != null)
                {
                    TransformMovement.Instance.SetBrush((args.Item as EntityViewItem));
                }
            };

        }

        public void LoadEntities(Assembly gameAssembly)
        {
            int i = 0;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            View = View.LargeIcon;
            LargeImageList = imageList;

            var prefabImage = Bitmap.FromFile("Images/MarauderLogo.png");

            foreach (Type type in gameAssembly.GetTypes())
            {

                if (type.IsSubclassOf(typeof(Entity)) && !type.IsAbstract)
                {
                    Console.WriteLine(type.DeclaringType);
                    EntityViewItem lvi = new EntityViewItem(type);
                    lvi.ImageIndex = i;

                    imageList.Images.Add(type.Name, prefabImage);
                    Items.Add(lvi);

                    i++;
                
                }
            }
        }

        public void LoadContent()
        {
            Console.WriteLine("loading content");
            List<Asset> bitmaps = new List<Asset>();
            foreach (KeyValuePair<string, object> key in TextureManager.ContentDictionary)
            {
                if (key.Value is Texture2D)
                {
                    bitmaps.Add(new Asset()
                    {
                        Key = key.Key,
                        Bitmap = ConvertToBitmap(key.Key)
                    });
                }
            }
            AddContent(bitmaps);
        }
        
        public void AddContent(string key)
        {
            Items.Add(key);
        }

        public void AddContent(List<Asset> bitmaps)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64,64);


            View = View.LargeIcon;
            LargeImageList = imageList;

            var prefabImage = Bitmap.FromFile("Images/MarauderLogo.png");
            int i = 0;
            foreach (var image in bitmaps)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.ToolTipText = "Test";
                
                lvi.Text = image.Key;
                Items.Add(lvi);

                imageList.Images.Add(image.Key, prefabImage);


                Console.WriteLine("adding imgae");
                i++;
            }

            Refresh();
        }

        public Bitmap ConvertToBitmap(string key)
        {
            MemoryStream memoryStream = new MemoryStream();
            TextureManager.GetContent<Texture2D>(key).SaveAsPng(memoryStream,
                TextureManager.GetContent<Texture2D>(key).Width,
                TextureManager.GetContent<Texture2D>(key).Height);
            return new Bitmap(memoryStream);
        }
    }
}
