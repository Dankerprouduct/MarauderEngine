using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using MarauderEngine.Components;
using MarauderEngine.Core;
using MarauderEngine.Utilities;
using MarauderEngine.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IComponent = MarauderEngine.Components.IComponent;
using MathHelper = MarauderEngine.Utilities.MathHelper;

namespace MarauderEngine.Entity
{
    public class Entity
    {

        public static int nextAvailibleID = 0;
        
        public int id;
        //public Vector2 position;
        //public Vector2 oldPosition;
        public int cellX;
        public int cellY;
        public int cellIndex; // holds current cell
        public int oldCellIndex;

        public Entity Parent { get; set; }
        public EntityData EntityData = new EntityData();
        public List<Entity> Children = new List<Entity>();

        /// <summary>
        /// whether or not this entity is "in play"
        /// set to false to destroy
        /// </summary>
        public bool active = true;

        public string EntityName
        {
            get => EntityData.EntityName;
            set => EntityData.EntityName = value; 
        }

        public TypeDictionary<IComponent> Components = new TypeDictionary<IComponent>();


        public Rectangle collisionRectanlge;
        int physicsIndex; 
        public Entity()
        {
            id = nextAvailibleID;
            EntityData.EntityID = id;
            nextAvailibleID++;
            active = true;
            EntityData.EntityName = EntityName;
            EntityData.EntityType = this.GetType().UnderlyingSystemType;
            
            MarauderComponent.AddEntityType(this);

        }

        public Entity(Entity parent) : this()
        {
            Parent = parent;
            Parent.Children.Add(this);
            Parent.EntityData.Children.Add(EntityData);

        }



        /// <summary>
        /// Sets a parent
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(Entity parent)
        {
            if (Parent != null)
            {
                Parent.Children.Remove(this);
            }

            Parent = parent; 
            Parent.Children.Add(this);
            Parent.EntityData.Children.Add(EntityData);
        }

        /// <summary>
        /// fires an event to the entity
        /// </summary>
        /// <param name="_event"></param>
        /// <returns>returns whether or not the event returned true or false</returns>
        public bool FireEvent(Event _event)
        {
            if (_event.id == "Collider")
            {
                if (Components.Count > 0)
                {
                    if (Components.ContainsKey<PhysicsComponent>())
                    {
                        
                        if (GetComponent<PhysicsComponent>().FireEvent(_event))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                
                foreach (var component in Components.Values)
                {
                    if (component.FireEvent(_event))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// returns a component given a name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetComponent<T>() where T: IComponent
        {
            return (T)Components.Get<T>();
        }

        /// <summary>
        /// returns a component given a name
        /// Please only for scripting
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IComponent GetComponent(string name)
        {
            return Components.First(c => c.Value.Name == name).Value;
        }


        //public T AddComponent<T>(IComponent value) 
        //{
        //    Components.Add<T>(value);
        //    return GetComponent<T>(); 
        //}

        public T AddComponent<T>(T component) where T : IComponent
        {
            Components.Add<T>(component);
            return GetComponent<T>();
        }

        public void ForceAddComponent<T>(T component) where T : IComponent
        {
            Components.ForceAdd<T>(component);
        }

        public bool HasComponent<T>()
        {
            return Components.ContainsKey(typeof(T));
        }

        /// <summary>
        /// returns the vector2 position of entity
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("Stop being lazy, use GetComponent<TransformComponent>().Position")]
        public Vector2 GetEntityPosition()
        {
            return GetComponent<TransformComponent>().Position;
        }
        
        #region Partition Methods
        /// <summary>
        /// Sets the partitions indexes for the entity
        /// </summary>
        /// <param name="cellX">x partition index</param>
        /// <param name="cellY">y partition index</param>
        public void SetPartitionCell(int cellX, int cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY;
            
        }

        /// <summary>
        /// Sets the index of the partition 
        /// </summary>
        /// <param name="index"></param>
        public void SetCellIndex(int index)
        {
            //Console.WriteLine("set cell index " + index); 
            cellIndex = index;
            oldCellIndex = cellIndex; 
        }

        /// <summary>
        /// Returns the current Partition
        /// </summary>
        /// <returns></returns>
        public int GetCurrentDynamicPartition()
        {
            return SceneManagement.CurrentScene.CellSpacePartition.PositionToIndex(this); 
        }

        /// <summary>
        /// Returns the center partition
        /// </summary>
        /// <returns>Center partition</returns>
        public int GetCenterPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            return center; 
        }

        /// <summary>
        /// Returns the partition right of the entity 
        /// </summary>
        /// <returns>partition right of the entity</returns>
        public int GetRightPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int right = center + 150;
            return right; 
        }

        /// <summary>
        /// returns the partition left of the entity
        /// </summary>
        /// <returns>partition left of the entity</returns>
        public int GetLeftPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int left = center - 150;
            return left; 
        }

        /// <summary>
        /// returns the partition towards the bottom of the entity
        /// </summary>
        /// <returns>partition south of entity</returns>
        public int GetBottomPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int left = center - 1;
            return left; 
        }

        /// <summary>
        /// returns the partition towards the top of the entity
        /// </summary>
        /// <returns>partition towards the top of the entity</returns>
        public int GetTopPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int top = cellIndex + 1;
            return top; 
        }

        /// <summary>
        /// returns the southeast partition
        /// </summary>
        /// <returns>souith east partition</returns>
        public int GetBottomRightPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int bottomRight = center + 1 + 150;
            return bottomRight; 
        }

        /// <summary>
        /// returns the bottom left partition 
        /// </summary>
        /// <returns>bottom left partition</returns>
        public int GetBottomLeftPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int bottomLeft = center + 1 - 150;
            return bottomLeft; 
        }

        /// <summary>
        /// returns the top right partition 
        /// </summary>
        /// <returns>top right partition</returns>
        public int GetTopRightPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int topRight = center + 150 - 1;
            return topRight; 
        }

        /// <summary>
        /// returns the top left partition
        /// </summary>
        /// <returns>top left partition</returns>
        public int GetTopLeftPartition()
        {
            cellIndex = GetCurrentDynamicPartition();
            int center = cellIndex;
            int topLeft = center - 150 - 1;
            return topLeft; 
        }
        #endregion

        /// <summary>
        /// Update function that is called by the Cell Space Partitions
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

            UpdateCellIndex(); 
            UpdateComponents(gameTime);
        }

        public void UpdateCellIndex()
        {
            cellIndex = GetCenterPartition();
            if (cellIndex != oldCellIndex)
            {
                SceneManagement.CurrentScene.CellSpacePartition.ChangeCell(this);
                cellIndex = GetCenterPartition();
                SetCellIndex(cellIndex);
                oldCellIndex = cellIndex;
            }
        }
        
        /// <summary>
        /// A helper update function. use as needed as long as the entity calling it is being updated
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="entity"></param>
        public virtual void Update(GameTime gameTime, Entity entity)
        {

        }

        /// <summary>
        /// updates all of the Components in componetns
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateComponents(GameTime gameTime)
        {
            foreach (var component in Components.Values)
            {
                component.UpdateComponent(gameTime);
            }
        }

        /// <summary>
        /// destroys all components and data
        /// </summary>
        public void DestroyEntity()
        {

            Destroy();
            foreach (var component in Components.Values)
            {
                component.Destroy();
                
            }
            Components.Clear();
        }

        /// <summary>
        /// Draws the entity
        /// base draws the body
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        /// <summary>
        /// sets active state of the entity to 0
        /// & marks it to be deleted
        /// </summary>
        public virtual void Destroy()
        {
            //Console.WriteLine("Destroying entity " + id);
            active = false; 
            
        }

        /// <summary>
        /// A helper funtion for draw
        /// </summary>
        [System.Obsolete()]
        public virtual void Draw()
        {

        }

    }
}
