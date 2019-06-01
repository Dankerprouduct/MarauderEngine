using System;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.Entity.Items
{
    [System.Obsolete()]
    public abstract class Item: MarauderEngine.Entity.Entity
    {
        public Item(): base()
        {

        }
        

        public abstract void Use(MarauderEngine.Entity.Entity entity);
        public bool inUse; 
        public int worldItemTextureID;
        public int guiItemID;
        public int itemID;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState currentMouseState;
        public MouseState previousMouseState; 

    }

    public class EmptyItem: Item
    {
        public EmptyItem()
        {
            itemID = -1; 
        }
        
        public override void Use(MarauderEngine.Entity.Entity entity)
        {
            Console.WriteLine("using Empty Item"); 
        }
    }
}
