using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEngine.Graphics.Animation
{
    public class AnimationDefinition
    {
        public AnimationDefinition(string name, Frame[] frames)
        {
            this.Name = name;
        }

        public string Name { get; }

    }
}
