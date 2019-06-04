using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarauderEngine.GUI
{
    public class UIScrollWindow: UIWindow
    {
        public bool Scroll { get; set; }
        public bool HorizontalScroll = false;
        public int ScrollSpeed = 10; 
        public UIScrollWindow(Point origin, Point size, bool enabled) : base(origin, size, enabled)
        {
            Scroll = true;
        }

        public UIScrollWindow(IUIElement parent, Point origin, Point size, bool enabled) : base(parent, origin, size, enabled)
        {
            Scroll = true;
        }

        public UIScrollWindow(IUIElement parent, Point origin, Point size, Color color, bool enabled) :
            base(parent, origin, size, color, enabled)
        {
            Scroll = true; 
        }

        public override void Update()
        {
            if (Enabled)
            {
                if (Scroll)
                {


                    if (UIManager.Instance.CurrentMouseState.ScrollWheelValue >
                        UIManager.Instance.PrevopisMouseState.ScrollWheelValue)
                    {
                        for (int i = 0; i < GetElements().Count; i++)
                        {
                            if (!HorizontalScroll)
                            {
                                Elements[i].AddPosition(new Point(0, -ScrollSpeed));
                            }
                            else
                            {
                                Elements[i].AddPosition(new Point(-ScrollSpeed, 0));
                            }
                        }

                    }

                    if (UIManager.Instance.CurrentMouseState.ScrollWheelValue <
                        UIManager.Instance.PrevopisMouseState.ScrollWheelValue)
                    {
                        for (int i = 0; i < GetElements().Count; i++)
                        {
                            if (!HorizontalScroll)
                            {
                                Elements[i].AddPosition(new Point(0, ScrollSpeed));
                            }
                            else
                            {
                                Elements[i].AddPosition(new Point(ScrollSpeed, 0));
                            }
                        }

                    }
                }
            }

            base.Update();
        }

        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, _rasterizerState);
            Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = rectangle;
            base.Draw(spriteBatch);

            spriteBatch.End();


            spriteBatch.Begin();

        }
    }
}
