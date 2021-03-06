﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework;

namespace MarauderEngine.Components
{
    public class TagComponent: Component<TagCData>
    {

        /// <summary>
        /// The tag associated with the Entity
        /// </summary>
        public string TagName
        {
            get => _data.TagName;
            set => _data.TagName = value;
        }

        public override Type type => GetType();
        public TagComponent() { }
        public TagComponent(Entity.Entity entity, string tagName)
        {
            RegisterComponent(entity, "TagComponent");

            TagName = tagName;
            EntityTagSystem.Instance.RegisterTagComponent(this);
        }

        public override bool FireEvent(Event eEvent)
        {

            return false; 
        }

        public override void UpdateComponent(GameTime gameTime)
        {

        }

        public override void Destroy()
        {
            EntityTagSystem.Instance.RemoveTag(this);
        }
    }
}
