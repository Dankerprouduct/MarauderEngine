using System.Collections.Generic;

namespace MarauderEngine.Entity.Items
{
    public static class ItemDictionary
    {

        public static List<Item> itemDictinary = new List<Item>(); 


        public static void LoadItemDatabase()
        {
            // TODO: add method for adding items
            // items must be synced with itemDictionary id
            //itemDictinary.Add(new Weapons.LaserRifle());
            //itemDictinary.Add(new Weapons.FusionRifle());
            //itemDictinary.Add(new Weapons.GrenadeLauncher());
        }

        public static void AddItem(Item item)
        {
            itemDictinary.Add(item);
        }
    }
}
