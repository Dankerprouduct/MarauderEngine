using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class TransformComponent: IComponent
    {
        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public Vector2 Position { get; set; }

        private float _rotation = 0;
        /// <summary>
        /// Rotation in Radians
        /// </summary>
        public  float Rotation
        {
            get { return _rotation; }
            set
            {
                if (!float.IsNaN(value))
                {
                    _rotation = value;

                }
            }
        }

        public TransformComponent(MarauderEngine.Entity.Entity owner)
        {
            RegisterComponent(owner, "TransformComponent");
        }

        public TransformComponent(MarauderEngine.Entity.Entity owner, Vector2 position)
        {
            RegisterComponent(owner, "TransformComponent");
            Position = position; 
        }

        public void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName;
            Active = true; 
        }

        /// <summary>
        /// Fires Event to Physics Component
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        public bool FireEvent(Event eEvent)
        {

            if (eEvent.id == "AddRotation")
            {
                Rotation += (float)eEvent.parameters["rotation"];
            }

            if (eEvent.id == "SetRotation")
            {
                Rotation = (float)eEvent.parameters["rotation"];
            }
            return false; 
        }

        /// <summary>
        /// Updates Component
        /// </summary>
        public void UpdateComponent()
        {
            //var physicsComponent = Owner.GetComponent<PhysicsComponent>(); //(PhysicsComponent) Owner.GetComponent("PhysicsComponent");
            
            //if (physicsComponent != null)
            //{
            //    //Position = physicsComponent.GetPosition();
            //}
        }

        public void Destroy()
        {

        }
    }
}
