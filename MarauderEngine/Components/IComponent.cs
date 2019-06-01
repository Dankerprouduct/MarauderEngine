namespace MarauderEngine.Components
{
    public  interface IComponent
    {
        MarauderEngine.Entity.Entity Owner { get; set; }
        string Name { get; set; }
        bool Active { get; set; }

        /// <summary>
        /// registers Components owner
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="componentName"></param>
        void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName);

        /// <summary>
        /// Receives Event
        /// </summary>
        /// <param name="eEvent"></param>
        bool FireEvent(Event eEvent);

        void UpdateComponent();

        void Destroy(); 

    }
}
