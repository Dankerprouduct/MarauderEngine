using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MarauderEngine.World
{
    public class Pathfinding
    {

        public static NodeMesh Node_Mesh; 
        public List<Node> oSet;
        public List<Node> cSet;
        public static int MaxPathingDistance = 200;


        public static void SetNodeMesh(NodeMesh nodemesh)
        {
            Node_Mesh = nodemesh; 
        }

        public void DrawSets()
        {

            if (oSet != null)
            {
                for (int i = 0; i < oSet.Count; i++)
                {
                    //Console.WriteLine("drawing: " + new Vector2(oSet[i].arrayPosition.X * 128, oSet[i].arrayPosition.Y * 128)); 
                    GUI.GUI.DrawCircle(new Vector2(oSet[i].arrayPosition.X * 128, oSet[i].arrayPosition.Y * 128), 64, Color.Yellow * .5f);
                }
            }
            if (cSet != null)
            {
                for (int i = 0; i < cSet.Count; i++)
                {
                    GUI.GUI.DrawCircle(new Vector2(cSet[i].arrayPosition.X * 128, cSet[i].arrayPosition.Y * 128), 64, Color.Blue * .5f);
                    //Console.WriteLine(cSet[i].arrayPosition + " " + i); 
                }
            }
            
        }

        public void SetSets(List<Node> openSet, List<Node> closedSet)
        {
            cSet = closedSet;
            oSet = openSet; 
        }
        
        public  List<Node> FindPath(Node start, Node goal)
        {

            // the set of nodes already evaluated
            List<Node> closedSet = new List<Node>();

            // the  set of currently discovered nodes
            List<Node> openSet = new List<Node>();


            Node startNode = new Node(start.arrayPosition);
            startNode.arrayPosition = start.arrayPosition; 
            startNode.SetNodeStats(startNode, startNode, goal.arrayPosition);
            startNode.parentNode = startNode;


            //Console.WriteLine("START: "+startNode.arrayPosition);
            
            openSet.Add(startNode);

            Node targetNode = new Node(goal.arrayPosition);
            targetNode.arrayPosition = goal.arrayPosition;

            //Console.WriteLine("GOAL: " + goal.arrayPosition);
            bool lookingForPath = true;
            int counter = 0; 
            while(lookingForPath)
            {

                CompareNodeByDistance compareByPathLength = new CompareNodeByDistance();
                openSet.Sort(compareByPathLength.Compare);
                Node currentNode = openSet[0];
               // Console.WriteLine("node position " + currentNode.arrayPosition); 
                
                
                if (counter > MaxPathingDistance)
                {
                    List<Node> chunkPath = new List<Node>();
                    chunkPath.Add(openSet[1]);

                    for (int i = 0; i < closedSet.Count; i++)
                    {
                        if (chunkPath[i].parentNode != startNode)
                        {
                            Node nextNode = closedSet.Find(node => node.arrayPosition == chunkPath[i].parentNode.arrayPosition);
                            chunkPath.Add(nextNode);
                        }
                        else if (chunkPath[i].parentNode == startNode)
                        {
                            chunkPath.Add(startNode);
                            break;
                        }
                    }

                    chunkPath.Reverse();

                    counter = 0;
                    SetSets(openSet, closedSet);
                    return chunkPath;
                }

                var adjacentNodes = GetWalkableAdjacentSquares(currentNode.arrayPosition.X, currentNode.arrayPosition.Y, Node_Mesh.GetMap());

                for (int i = 0; i < adjacentNodes.Count; i++)
                {
                    if(adjacentNodes[i] != null)
                    {
                        if(adjacentNodes[i].arrayPosition == targetNode.arrayPosition)
                        {
                            targetNode.parentNode = currentNode;
                            targetNode.arrayPosition = currentNode.arrayPosition;
                            lookingForPath = false;

                            break;
                        }
                        else if (!CheckClosedListPosition(closedSet, adjacentNodes[i]) && !CheckClosedListPosition(openSet, adjacentNodes[i]))
                        {
                            Node newNode = new Node(adjacentNodes[i].arrayPosition, currentNode);
                            newNode.SetNodeStats(startNode, newNode, targetNode.arrayPosition);
                            
                            //Console.WriteLine("adding " + newNode.arrayPosition + " to open list");

                            openSet.Add(newNode);
                            //counter++; 
                            SetSets(openSet, closedSet);
                        }
                        
                    }
                }

                closedSet.Add(currentNode);



                if (!lookingForPath)
                {
                    closedSet.Add(targetNode);
                    
                }
                openSet.Remove(currentNode);
                //Console.WriteLine("removed " + currentNode.arrayPosition + " from open list");
                counter++;

                if (openSet.Count == 0 && lookingForPath)
                {
                    lookingForPath = false;
                    break;
                }

                
                
                
                                
            }



            // final path stuff
            List<Node> finalPath = new List<Node>();
            finalPath.Add(targetNode);

            for (int i = 0; i < closedSet.Count; i++)
            {
                if (finalPath[i].parentNode != startNode && finalPath[i].parentNode != null)
                {
                    Node nextNode = closedSet.Find(node => node.arrayPosition == finalPath[i].parentNode.arrayPosition);
                    finalPath.Add(nextNode);
                }
                else if (finalPath[i].parentNode == startNode)
                {
                    finalPath.Add(startNode);
                    break;
                }
            }

            finalPath.Reverse();
            if (finalPath[0] == startNode)
            {
                int finalIndex = finalPath.FindIndex(node => node == targetNode);
            }

            return finalPath;
        }

        static List<Node> GetWalkableAdjacentSquares(int x, int y, Node[,] map)
        {

            var proposedLocations = new List<Node>();
            //Console.WriteLine(x + " " + y); 
            if(Node_Mesh.GetNode(x - 1,y) != null)
            {
                proposedLocations.Add(Node_Mesh.GetNode(x - 1, y));
            }

            if (Node_Mesh.GetNode(x + 1, y) != null)
            {
                proposedLocations.Add(Node_Mesh.GetNode(x + 1, y));
            }

            if (Node_Mesh.GetNode(x, y - 1) != null )
            {
                proposedLocations.Add(Node_Mesh.GetNode(x, y - 1));
            }

            if (Node_Mesh.GetNode(x, y + 1) != null )
            {
                proposedLocations.Add(Node_Mesh.GetNode(x, y + 1));
            }

            if (Node_Mesh.GetNode(x + 1, y + 1 ) != null  )
            {
                proposedLocations.Add(Node_Mesh.GetNode(x + 1, y + 1));
            }

            if (Node_Mesh.GetNode(x - 1, y + 1) != null)
            {
                proposedLocations.Add(Node_Mesh.GetNode(x - 1, y + 1));
            }

            if (Node_Mesh.GetNode(x + 1, y - 1) != null )
            {
                proposedLocations.Add(Node_Mesh.GetNode(x + 1, y - 1));
            }

            if (Node_Mesh.GetNode(x - 1, y - 1) != null )
            {
                proposedLocations.Add(Node_Mesh.GetNode(x - 1, y - 1));
            }

            for(int i = 0; i < proposedLocations.Count; i++)
            {
               // Console.WriteLine("proposedLocations: " + i + " " + proposedLocations[i].arrayPosition);
            }
            return proposedLocations; 
            /*
            return proposedLocations.Where(
                l => map[l.arrayPosition.Y, l.arrayPosition.X] != null).ToList(); 
                */
        }

        public bool CheckClosedListPosition(List<Node> nodes, Node node)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].arrayPosition == node.arrayPosition)
                {
                    return true;
                }
            }

            return false;
        }
    }

    
    

    public class CompareNodeByDistance : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.totalPathDistance > y.totalPathDistance)
            {
                return 1;
            }
            else if (x.totalPathDistance < y.totalPathDistance)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
