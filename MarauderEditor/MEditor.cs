using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;

namespace MarauderEditor
{
    public class MEditor : MonoGameControl
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();
            
            Editor.spriteBatch.Begin();


            Editor.spriteBatch.End();

        }
    }
}
