using BulletML.Enums;
using BulletML.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This task changes the direction a little bit every frame
    /// </summary>
    public class ChangeDirectionTask : BulletMLTask
    {
        #region Members

        private bool _firstTime = true;
        private float _nextDirection;

        /// <summary>
        /// The amount pulled out of the node
        /// </summary>
        private float _nodeDirection;

        /// <summary>
        /// the type of direction change, pulled out of the node
        /// </summary>
        private NodeType _changeType;

        /// <summary>
        /// How long to run this task... measured in frames
        /// </summary>
        private float Duration { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ChangeDirectionTask(ChangeDirectionNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        public override void InitTask(Bullet bullet)
        {
            base.InitTask(bullet);
            _firstTime = true;
        }

        /// <summary>
        /// this sets up the task to be run.
        /// </summary>
        /// <param name="bullet">bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            // Set the time length to run this dude
            Duration = Node.GetChildValue(NodeName.term, this, bullet);

            // Check for divide by 0
            if (Math.Abs(Duration) < float.Epsilon)
                Duration = 1.0f;

            // Get the amount to change direction from the nodes
            var directionNode = Node.GetChild(NodeName.direction) as DirectionNode;
            if (directionNode != null)
            {
                // Also make sure to convert to radians
                _nodeDirection = MathHelper.ToRadians(directionNode.GetValue(this));

                // How do we want to change direction?
                _changeType = directionNode.NodeType;
            }
        }

        private float GetDirection(Bullet bullet)
        {
            // How do we want to change direction?
            float direction;

            switch (_changeType)
            {
                case NodeType.sequence:
                    {
                        // We are going to add this amount to the direction every frame
                        direction = _nodeDirection;
                    }
                    break;

                case NodeType.absolute:
                    {
                        // We are going to go in the direction we are given, regardless of where we are pointing right now
                        direction = _nodeDirection - bullet.Direction;
                    }
                    break;

                case NodeType.relative:
                    {
                        // The direction change will be relative to our current direction
                        direction = _nodeDirection;
                    }
                    break;

                default:
                    {
                        // The direction change is to aim at the enemy
                        direction = (bullet.GetAimDirection() + _nodeDirection) - bullet.Direction;
                    }
                    break;
            }

            // The sequence type of change direction is unaffected by the duration
            if (_changeType == NodeType.absolute || _changeType != NodeType.sequence)
            {
                // Divide by the duration so we ease into the direction change
                direction /= Duration;
            }

            // Keep the direction between -180 and 180
            direction = MathHelper.WrapAngle(direction);

            return direction;
        }

        public override TaskRunStatus Run(Bullet bullet)
        {
            // Change the direction of the bullet by the correct amount
            if (_firstTime)
            {
                _firstTime = false;
                _nextDirection = GetDirection(bullet);
            }

            bullet.Direction += _nextDirection;
            Duration--;

            // Decrement the amount if time left to run and return End when this task is finished
            if (Duration > 0) return TaskRunStatus.Continue;

            // Since this task isn't finished, run it again next time
            Finished = true;
            return TaskRunStatus.End;
        }

        #endregion Methods
    }
}