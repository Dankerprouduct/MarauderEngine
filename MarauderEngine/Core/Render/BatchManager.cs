using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.Core.Render
{
    /// <summary>
    /// The Render Pipeline that determines which order sprites are drawn to the screen 
    /// </summary>
    public class BatchManager: SystemManager<BatchManager>
    {
        private List<SpriteComponent> _spriteComponents = new List<SpriteComponent>();

        public enum BatchingMethod
        {
            FrontToBack,
            BackToFront,
            Deferred
        }

        private BatchingMethod _batchingMethod; 

        /// <summary>
        /// The initializer
        /// Provide the Batching Method you'd like to use  
        /// </summary>
        /// <param name="batchingMethod"></param>
        public BatchManager(BatchingMethod batchingMethod)
        {
            _batchingMethod = batchingMethod; 
        }

        /// <summary>
        /// This gets called on init 
        /// </summary>
        public override void Initialize()
        {
            
        }

        /// <summary>
        /// Adds a sprite that will be used for batchng
        /// </summary>
        /// <param name="spriteComponent"></param>
        public void AddSpriteComponent(SpriteComponent spriteComponent)
        {
            _spriteComponents.Add(spriteComponent);

            RefreshBatch(); 
        }

        /// <summary>
        /// removes the sprite component 
        /// </summary>
        /// <param name="spriteComponent"></param>
        public void RemoveSpriteComponent(SpriteComponent spriteComponent)
        {
            _spriteComponents.Remove(spriteComponent);

        }

        /// <summary>
        /// Refreshes the draw order
        /// I wouldn't call this too often because it could get expensive
        /// </summary>
        public void RefreshBatch()
        {
            switch (_batchingMethod)
            {
                    case BatchingMethod.BackToFront:
                    {
                        _spriteComponents = (from s in _spriteComponents orderby s.Layer select s).ToList();
                        break;
                    }

                    case BatchingMethod.FrontToBack:
                    {
                        _spriteComponents = (from s in _spriteComponents orderby s.Layer descending select s).ToList();
                        break;
                    }

                    case BatchingMethod.Deferred:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Draws all components added 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _spriteComponents.Count; i++)
            {
                _spriteComponents[i].Draw(spriteBatch);
            }
        }

    }
}
