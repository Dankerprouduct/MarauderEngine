using System.Collections.Generic;

namespace MarauderEngine.Entity
{
    public class Room
    {
        public int[,] tileMap;
        
        public List<Decoration> Decorations
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                if (tileMap != null) return tileMap.GetLength(0);
                else
                {
                    return 0;
                }
            }
        }

        public int Height
        {
            get
            {
                if (tileMap != null) return tileMap.GetLength(1);
                else
                {
                    return 0;
                }
            }
        }
    }
}
