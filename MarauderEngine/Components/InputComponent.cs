using System;
using MarauderEngine.Components.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.Components
{
    public class InputComponent: IComponent
    {
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        MouseState mouseState;
        MouseState oldMouseState;

        public MarauderEngine.Entity.Entity Owner { get; set; }
        public string Name { get; set; }
        public ComponentData Data { get; set; }
        public bool Active { get; set; }

        public InputComponent(MarauderEngine.Entity.Entity entity)
        {
            RegisterComponent(entity, "InputComponent");
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
            keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.W))
            {

                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(0, -1));
                Owner.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(0, 1));
                Owner.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(-1, 0));
                Owner.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(1, 0));
                Owner.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.Tab) && oldKeyboardState.IsKeyUp(Keys.Tab))
            {
                Event openInventoryEvent = new Event();
                openInventoryEvent.id = "OpenInventory";
                Console.WriteLine("opening inventory");
                Owner.FireEvent(openInventoryEvent); 
            }


            oldKeyboardState = keyboardState;
        }

        public void Destroy()
        {
            
        }
    }
}
