using System;
using System.Collections.Generic;
using MarauderEngine.Core;
using Microsoft.Xna.Framework;

namespace MarauderEngine.World
{
    public class NodeMesh
    {
        public static NodeMesh Instance;

        Node[,] map;
        List<Node> allAvailiableNodes = new List<Node>(); 
        
        public NodeMesh()
        {
            Instance = this; 


        }

        public void RemoveNode(int x, int y)
        {
            if (map[x, y] != null)
            {
                allAvailiableNodes.Remove(map[x, y]);
                map[x, y] = null; 
            }
        }

        /// <summary>
        /// removes all nodes from node mesh 
        /// </summary>
        public void FlushNodeMesh()
        {
            if (map != null)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        map[x, y] = null;
                    }
                }
            }
        }

        public void MakeMap(int walkableTile, int impassableTile, int[,] map)
        {
            this.map = new Node[map.GetLength(0), map.GetLength(1)];

            for (int y = 0; y < this.map.GetLength(1); y++)
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (map[x, y] == walkableTile)
                    {
                        this.map[x, y] = new Node(new Point((x), (y)));
                        //this.map[x, y].arrayPosition = new Point(x, y); 
                        allAvailiableNodes.Add( this.map[x,y] );
                    }
                    else
                    {
                        this.map[x, y] = null;
                    }
                }
            }

        }

        public void MakeMap( int impassableTile, int[,] map)
        {
            this.map = new Node[map.GetLength(0), map.GetLength(1)];

            for (int y = 0; y < this.map.GetLength(1); y++)
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (map[x, y] != impassableTile)
                    {
                        this.map[x, y] = new Node(new Point((x), (y)));
                        //this.map[x, y].arrayPosition = new Point(x, y); 
                        allAvailiableNodes.Add(this.map[x, y]);
                    }
                    else 
                    {
                        this.map[x, y] = null;
                    }
                }
            }

        }

        public void DrawNodes()
        {
            if (Core.Debug.ShowBoundariesAndMesh)
            {

                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        if (map[x, y] != null)
                        {
                            GUI.GUI.DrawCircle(new Vector2(map[x, y].arrayPosition.X * 128 + 64 - 15, map[x, y].arrayPosition.Y * 128 + 64 - 15), 30, Color.LimeGreen * 1f);
                            //GUI.GUI.DrawString(x.ToString() + " " + y.ToString(), new Vector2(map[x, y].nodePosition.X, map[x, y].nodePosition.Y), 1, 1, Color.White); 
                        }
                    }
                }

            }
        }

        public Node GetNode(int x, int y)
        {
            try
            {
                return map[x, y];
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return FindNodeOnMesh(); 
            }
        }

        public Node FindNodeOnMesh()
        {
            
            int index = Game1.random.Next(0, allAvailiableNodes.Count-1);
            Console.WriteLine(index); 
            return map[allAvailiableNodes[index].arrayPosition.X, allAvailiableNodes[index].arrayPosition.Y];
        }

        public Node[,] GetMap()
        {
            return map; 
        }



    }
}
