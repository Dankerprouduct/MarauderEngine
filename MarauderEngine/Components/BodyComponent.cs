using MarauderEngine.Components.Data;
using MarauderEngine.Entity.Body;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class BodyComponent: IComponent
    {
        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public ComponentData Data { get; set; }
        public bool Active { get; set; }
        
        public Body body;

        private readonly TransformComponent _transformComponent; 

        public BodyComponent(MarauderEngine.Entity.Entity entity)
        {
            RegisterComponent(entity, "BodyComponent");

            _transformComponent = Owner.GetComponent<TransformComponent>();

            body = new Body();

            body.AddBodyPart(new Torso(3, Vector2.Zero)
            {
                lerpSpeed = .2f,
                turnAngle = 25,
                scale = 1
            });

            body.AddBodyPart(new Head(1, new Vector2(-22, 0))
            {
                lerpSpeed = .2f,
                turnAngle = 5,
                scale = 1f
            });
            body.AddBodyPart(new Hand(2, new Vector2(30, -44))
            {
                scale = 1.3f,
                // try 25 for lols
                lerpSpeed = .1f,
                turnAngle = 10
            });
            body.AddBodyPart(new Hand(2, new Vector2(30, 44))
            {
                scale = 1.3f,
                lerpSpeed = .1f,
                turnAngle = 10
            });
        }

        public void RegisterComponent(MarauderEngine.Entity.Entity entity, string componentName)
        {
            Owner = entity;
            Name = componentName;
            Active = true;
        }

        public bool FireEvent(Event eEvent)
        {

            return false;
        }

        public void UpdateComponent()
        {
            body.Update(_transformComponent.Position, _transformComponent.Rotation);
        }

        public void Destroy()
        {

        }
    }
}
