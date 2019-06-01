namespace MarauderEngine.Utilities
{
    public static class GameManager
    {
        private  static GameData<MarauderEngine.Entity.Entity> entityData = new GameData<MarauderEngine.Entity.Entity>();

        public static void Update()
        {

        }

        public static void SaveGame()
        {
            entityData.SaveData(Game1.world.cellSpacePartition.GetDynamicEntities(), "save2");
        }

        public static void LoadGame()
        {
            MarauderEngine.Entity.Entity[] ents = entityData.LoadData("save2");

            for (int i = 0; i < ents.Length; i++)
            {
                Game1.world.AddDynamicEntity(ents[i]);
            }
        }
    }
}
