using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarauderEngine.Components.Data;
using MarauderEngine.World;
using Microsoft.Xna.Framework;
using MathHelper = MarauderEngine.Utilities.MathHelper;

namespace MarauderEngine.Components
{
    public class AStarComponent : Component<AStarCData>
    {

        private Vector2 _currentPathingTarget;
        private List<Node> _pathingNode;
        private Pathfinding _pathfinding;
        private Point _pathingGoal;
        private Thread _pathingThread;
        private bool _isPathing = false;

        public override Type type => GetType();

        /// <summary>
        /// the distance at which the entity will stop pathfinding and move straight towards the target
        /// </summary>
        public float IgnoreDistance
        {
            get => _data.IgnoreDistance;
            set => _data.IgnoreDistance = value;
        }

        public AStarComponent() { }

        /// <summary>
        /// Used for entity pathfining
        /// Be sure to properly set the Node_Mesh in the Pathfinding class before using
        /// </summary>
        /// <param name="owner"></param>
        public AStarComponent(Entity.Entity owner)
        {
            RegisterComponent(owner, "AStarComponent");
            _pathfinding = new Pathfinding();
        }



        public override bool FireEvent(Event eEvent)
        {

            if (eEvent.id == "FindPath")
            {
                FindPathTo((Vector2)eEvent.parameters["target"]);
            }

            return false;
        }

        public override void UpdateComponent(GameTime gameTime)
        {

        }

        /// <summary>
        /// clears the current pathing nodes
        /// this will stop pathfinding
        /// </summary>
        public void ClearPaths()
        {
            if (_pathingNode != null)
            {
                _pathingNode.Clear();
            }
        }

        /// <summary>
        /// What inherited objects interface with
        /// if target is close enough it moves straight towards it 
        /// </summary>
        /// <param name="target"></param>
        public void FindPathTo(Vector2 target)
        {

            if (!_isPathing)
            {
                if (!(Vector2.Distance(target, Owner.GetComponent<TransformComponent>().Position) <= 128 * 2))
                {
                    _pathingNode?.Clear();
                    Pathfind(target);
                    _isPathing = true;
                }
                else
                {
                    if (Vector2.Distance(target, Owner.GetComponent<TransformComponent>().Position) >= IgnoreDistance)
                    {
                        MoveTo(target);
                        Owner.GetComponent<TransformComponent>().Rotation = MathHelper.RotationFromVector2(target, Owner.GetComponent<TransformComponent>().Position);
                    }

                    if (Vector2.Distance(target, Owner.GetComponent<TransformComponent>().Position) <= 128 * 5)
                    {
                        Owner.GetComponent<TransformComponent>().Rotation = MathHelper.RotationFromVector2(target, Owner.GetComponent<TransformComponent>().Position);
                    }

                }
            }

            if (_isPathing)
            {
                Pathfind(target);
                //Console.WriteLine("3");
                //StartPathfindingThread(target);
            }
        }

        /// <summary>
        /// moves toward a vector2 point
        /// </summary>
        /// <param name="target"></param>
        private void MoveTo(Vector2 target)
        {
            _currentPathingTarget = target;
            Vector2 direction = _currentPathingTarget - Owner.GetComponent<TransformComponent>().Position;

            if (direction.Length() != 0)
            {
                direction.Normalize();
            }

            Owner.GetComponent<TransformComponent>().Rotation =
                MathHelper.RotationFromVector2(target, Owner.GetComponent<TransformComponent>().Position);


            Event velocityEvent = new Event();
            velocityEvent.id = "AddVelocity"; 

            velocityEvent.parameters.Add("Velocity", direction * 1);
            Owner.FireEvent(velocityEvent);
            
        }

        /// <summary>
        /// Is a Courotine for the pathfinding algorithm,
        /// moves to the next node in the array then removes it once its too close
        /// 
        /// </summary>
        /// <param name="target"></param>
        private void Pathfind(Vector2 target)
        {
            //Console.WriteLine(pathingNode + " " + "THIS IS STUPID");
            if (_pathingNode != null && _pathingNode.Count > 0)
            {
                Vector2 nodePosition = new Vector2((_pathingNode[0].arrayPosition.X * 128) + 64, (_pathingNode[0].arrayPosition.Y * 128) + 64);
                MoveTo(nodePosition);
                //Console.WriteLine(nodePosition);
                //Console.WriteLine("trying to move");

                if (Vector2.Distance(Owner.GetComponent<TransformComponent>().Position, new Vector2((_pathingNode[0].arrayPosition.X * 128) + 64, (_pathingNode[0].arrayPosition.Y * 128) + 64)) < 64)
                {
                    _pathingNode.RemoveAt(0);


                }
            }
            else
            {
                StartPathThread(target);

                //_pathingThread = new Thread(() =>);
                //if (!_pathingThread.IsAlive)
                //{
                //    _pathingThread.Start();
                //}
                //_pathingThread.Join();
            }

        }

        /// <summary>
        ///  a new thread for A*
        /// </summary>
        /// <param name="target"></param>
        private void StartPathThread(Vector2 target)
        {
            Task pathTask = new Task(() =>
            {
                _isPathing = false;
                // Start Node
                Node startNode = new Node(new Point((int) Owner.GetComponent<TransformComponent>().Position.X / 128,
                    (int) Owner.GetComponent<TransformComponent>().Position.Y / 128));
                startNode.arrayPosition = new Point((int) Owner.GetComponent<TransformComponent>().Position.X / 128,
                    (int) Owner.GetComponent<TransformComponent>().Position.Y / 128);


                // Goal Node
                _pathingGoal = new Point((int) target.X / 128, (int) target.Y / 128);
                Node goalNode = new Node(new Point((int) _pathingGoal.X, (int) _pathingGoal.Y));
                goalNode.arrayPosition = _pathingGoal;


                _pathingNode = _pathfinding.FindPath(startNode, goalNode);
            });

            pathTask.Start();
        }

        /// <summary>
        /// a debug utility used to draw the nodes currently being used for pathing
        /// </summary>
        public void DrawPathingNodes()
        {
            if (_pathingNode != null)
            {
                if (_pathingNode.Count > 0 && _pathingNode != null)
                {
                    GUI.GUI.DrawLine(Owner.GetComponent<TransformComponent>().Position, new Vector2(_pathingGoal.X * 128, _pathingGoal.Y * 128), 10, Color.Blue);
                    GUI.GUI.DrawLine(Owner.GetComponent<TransformComponent>().Position, new Vector2(_pathingNode[0].arrayPosition.X * 128, _pathingNode[0].arrayPosition.Y * 128), 10, Color.Yellow);
                }
            }
            _pathfinding.DrawSets();
        }

        public override void Destroy()
        {

        }
    }
}
