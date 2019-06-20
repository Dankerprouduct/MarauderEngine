using System;

namespace MarauderEngine.Utilities
{
    [Obsolete("I dont even know what this does or was supposed to do therefore it is sin. use something else")]
    public static class GameManager
    {
        private  static GameData<MarauderEngine.Entity.Entity> entityData = new GameData<MarauderEngine.Entity.Entity>();

        public static void Update()
        {

        }

        public static void SaveGame()
        {
            //entityData.SaveData(Game1.world.cellSpacePartition.GetDynamicEntities(), "save2");
        }

        public static void LoadGame()
        {
            MarauderEngine.Entity.Entity[] ents = entityData.LoadData("save2");

            for (int i = 0; i < ents.Length; i++)
            {
                //Game1.world.AddDynamicEntity(ents[i]);
            }
        }
    }
}
