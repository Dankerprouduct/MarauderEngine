using System;
using Microsoft.Xna.Framework;

namespace MarauderEngine.World
{
    public class Node
    {
        // corresponds to the mapdata array
        public Point arrayPosition; 
        //public Point nodePosition;

        public Node parentNode;

        public float distanceFromStart;

        public float distanceToTarget;

        public float totalPathDistance; 
        
        public Node(Point nodePosition)
        {
            this.arrayPosition = nodePosition; 
            
        }

        public Node(Point nodePosition, Node parentNode)
        {
            this.arrayPosition = nodePosition;
            this.parentNode = parentNode;
            //this.arrayPosition = parentNode.arrayPosition; 
            
        }
        
        public void GetDistanceFromStart(Node startingNode, Node currentNode)
        {
            distanceFromStart = 0;
            Node node1 = currentNode; 

            while(node1.arrayPosition != startingNode.arrayPosition)
            {
                distanceFromStart += 1;
                if(node1.parentNode == null)
                {
                    Console.WriteLine("node parent is null");
                }
                node1 = node1.parentNode; 
            }          
        }

        public void GetDistanceToTarget(Point target)
        {
            distanceFromStart = Math.Abs(arrayPosition.X - target.X);
            distanceFromStart += Math.Abs(arrayPosition.Y - target.Y); 
        }

        public void RecalculateTotalPathDistance()
        {
            totalPathDistance = distanceFromStart + distanceToTarget; 
        }

        public void SetNodeStats(Node startNode, Node currentNode, Point targetPosition)
        {
            currentNode.GetDistanceFromStart(startNode, currentNode);
            currentNode.GetDistanceToTarget(targetPosition);
            currentNode.RecalculateTotalPathDistance();
        }
        
        
        

    }
}
