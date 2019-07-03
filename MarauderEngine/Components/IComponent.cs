using MarauderEngine.Components.Data;

namespace MarauderEngine.Components
{
    public  interface IComponent
    {
        /// <summary>
        /// What Entity this Component is assigned to
        /// </summary>
        MarauderEngine.Entity.Entity Owner { get; set; }
        string Name { get; set; }

        ComponentData Data { get; set; }

        /// <summary>
        /// The data for this
        /// </summary>
        //ComponentData<T> Data { get; }

        /// <summary>
        /// Whether or not this component is being updated
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// registers Components owner
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="componentName"></param>
        void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName);

        /// <summary>
        /// Receives Event
        /// Should return false as default.
        /// </summary>
        /// <param name="eEvent"></param>
        bool FireEvent(Event eEvent);

        /// <summary>
        /// Updates the Component. Updates are called from the Owner's Update method. 
        /// </summary>
        void UpdateComponent();

        /// <summary>
        /// Destroys the Component on Owner's DestroyEntity method.
        /// </summary>
        void Destroy(); 

    }
}
