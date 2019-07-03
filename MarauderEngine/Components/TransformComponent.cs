using System;
using System.Runtime.CompilerServices;
using MarauderEngine.Components.Data;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class TransformComponent: Component<TransformCData>
    {

        // Implement if abstract GetType does not return child type.
        //public override Type type => GetType();

        public override Type type => GetType();
        public Vector2 Position
        {
            get => _data.Position;
            set => _data.Position = value;
        }

        private float _rotation
        {
            get => _data._rotation;
            set => _data._rotation = value;
        }

        /// <summary>
        /// Rotation in Radians
        /// </summary>
        public  float Rotation
        {
            get => _rotation;
            set
            {
                if (!float.IsNaN(value))
                {
                    _rotation = value;

                }
            }
        }

        public TransformComponent() { }
        public TransformComponent(MarauderEngine.Entity.Entity owner)
        {
            RegisterComponent(owner, "TransformComponent");

        } 

        public TransformComponent(MarauderEngine.Entity.Entity owner, Vector2 position)
        {
            RegisterComponent(owner, "TransformComponent");
            Position = position; 
        }



        /// <summary>
        /// Fires Event to Physics Component
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        public override bool FireEvent(Event eEvent)
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
        public override void UpdateComponent()
        {
            //var physicsComponent = Owner.GetComponent<PhysicsComponent>(); //(PhysicsComponent) Owner.GetComponent("PhysicsComponent");
            
            //if (physicsComponent != null)
            //{
            //    //Position = physicsComponent.GetPosition();
            //}
        }

        public override void Destroy()
        {

        }
    }
}
