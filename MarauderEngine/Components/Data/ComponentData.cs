namespace MarauderEngine.Components.Data
{
    public abstract class ComponentData
    {

        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }


        public bool Active { get; set; }

    }
}
