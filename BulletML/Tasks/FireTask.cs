using BulletML.Enums;
using BulletML.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.IO;

namespace BulletML.Tasks
{
    /// <summary>
    /// A task to shoot a bullet
    /// </summary>
    public class FireTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The direction that this task will fire a bullet.
        /// </summary>
        /// <value>The fire direction in degrees.</value>
        public float FireDirection { get; private set; }

        /// <summary>
        /// The speed that this task will fire a bullet.
        /// </summary>
        /// <value>The fire speed.</value>
        public float FireSpeed { get; private set; }

        /// <summary>
        /// The scale that this task will fire a bullet.
        /// </summary>
        /// <value>The fire scale.</value>
        public float FireScale { get; private set; }

        /// <summary>
        /// If this fire node shoots from a bullet/bulletRef node, this will be a task created for it.
        /// This is needed so the params of the bulletRef can be set correctly.
        /// </summary>
        /// <value>The bullet reference task.</value>
        public BulletMLTask BulletTask { get; private set; }

        /// <summary>
        /// The node we are going to use to set the direction of any bullets shot with this task.
        /// </summary>
        /// <value>The direction node.</value>
        public DirectionTask DirectionTask { get; private set; }

        /// <summary>
        /// The node we are going to use to set the speed of any bullets shot with this task.
        /// </summary>
        /// <value>The speed node.</value>
        public SpeedTask SpeedTask { get; private set; }

        /// <summary>
        /// The node we are going to use to set the sprite index of any bullets shot with this task.
        /// </summary>
        /// <value>The sprite node.</value>
        public SpriteTask SpriteTask { get; private set; }

        /// <summary>
        /// The node we are going to use to set the scale of any bullets shot with this task.
        /// </summary>
        /// <value>The scale node.</value>
        public ScaleTask ScaleTask { get; private set; }

        private static float _lastFireDirection;
        private static float _lastFireSpeed;
        private static float _lastFireScale;

        private static float _lastSetupDirection;
        private static float _lastSetupSpeed;
        private static float _lastSetupScale;
        private bool _isRunning;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="FireTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public FireTask(FireNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);

            _lastFireDirection = 0f;
            _lastFireSpeed = 0f;
            _lastFireScale = 0f;
            _lastSetupDirection = 0f;
            _lastSetupSpeed = 0f;
            _lastSetupScale = 0f;
            _isRunning = false;
        }

        /// <summary>
        /// Parse a specified node and bullet into this task.
        /// </summary>
        /// <param name="bullet">The bullet that launched this task.</param>
        public override void ParseTasks(Bullet bullet)
        {
            if (bullet == null)
                throw new NullReferenceException("Bullet argument cannot be null.");

            // Setup the bullet node
            GetBulletNode(this, bullet);

            // Setup all the direction and speed nodes of the current fire node
            GetDirectionTasks(this);
            GetSpeedNodes(this);
            GetScaleNodes(this);
            GetSpriteNodes(this);

            // Setup all the direction and speed nodes of the bullet/bulletRef subnode
            GetDirectionTasks(BulletTask);
            GetSpeedNodes(BulletTask);
            GetScaleNodes(BulletTask);
            GetSpriteNodes(BulletTask);

            _lastSetupDirection = bullet.Direction;
            _lastSetupSpeed = bullet.Speed;
            _lastSetupScale = bullet.Scale;
        }

        /// <summary>
        /// Sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            // Get the direction to shoot the bullet

            // Does this fire node contain a direction node?
            if (DirectionTask != null)
            {
                // Set the fire direction to the "initial" value
                var newBulletDirection = DirectionTask.GetNodeValue(bullet);

                switch (DirectionTask.Node.NodeType)
                {
                    case NodeType.absolute:
                        {
                            // The new bullet points right at a particular direction
                            FireDirection = newBulletDirection;
                        }
                        break;

                    case NodeType.relative:
                        {
                            // The new bullet's direction will be relative to the old bullet's one
                            FireDirection = MathHelper.ToDegrees(bullet.Direction) + newBulletDirection;
                        }
                        break;

                    case NodeType.aim:
                        {
                            // Aim the bullet at the player
                            FireDirection = MathHelper.ToDegrees(bullet.GetAimDirection()) + newBulletDirection;
                        }
                        break;

                    case NodeType.sequence:
                        {
                            // The new bullet's direction will be relative to the last fired bullet's one
                            if (_isRunning)
                            {
                                var lastFireDirection = MathHelper.ToDegrees(_lastFireDirection);
                                FireDirection = lastFireDirection + newBulletDirection;
                            }
                            else
                            {
                                var lastSetupDirection = MathHelper.ToDegrees(_lastSetupDirection);
                                FireDirection = lastSetupDirection + newBulletDirection;
                            }
                        }
                        break;

                    default:
                        {
                            // Aim the bullet at the player
                            FireDirection = MathHelper.ToDegrees(bullet.GetAimDirection()) + newBulletDirection;
                        }
                        break;
                }
            }
            else
            {
                // There isn't direction task, so just aim at the player.
                FireDirection = MathHelper.ToDegrees(bullet.GetAimDirection());
            }

            // Convert from degrees to radians
            FireDirection = MathHelper.ToRadians(FireDirection);

            // Set the speed to shoot the bullet

            // Does this fire node contain a speed node?
            if (SpeedTask != null)
            {
                // Set the shoot speed to the "initial" value.
                var newBulletSpeed = SpeedTask.GetNodeValue(bullet);

                switch (SpeedTask.Node.NodeType)
                {
                    case NodeType.relative:
                        {
                            // The new bullet speed will be relative to the old bullet
                            FireSpeed = bullet.Speed + newBulletSpeed;
                        }
                        break;

                    case NodeType.sequence:
                        {
                            // If there is a sequence node, add the value to the bullet's speed
                            if (_isRunning)
                                FireSpeed = _lastFireSpeed + newBulletSpeed;
                            else
                                FireSpeed = _lastSetupSpeed + newBulletSpeed;
                        }
                        break;

                    default:
                        {
                            // The new bullet shoots at a predeterminde speed
                            FireSpeed = newBulletSpeed;
                        }
                        break;
                }
            }
            else
            {
                // There is no speed node in the fire node
                FireSpeed = Math.Abs(bullet.Speed) > float.Epsilon ? bullet.Speed : 0f;
            }

            if (ScaleTask != null)
            {
                var newBulletScale = ScaleTask.GetNodeValue(bullet);

                switch (ScaleTask.Node.NodeType)
                {
                    case NodeType.relative:
                        {
                            FireScale = bullet.Scale + newBulletScale;
                        }
                        break;

                    case NodeType.sequence:
                        {
                            if (_isRunning)
                                FireScale = _lastFireScale + newBulletScale;
                            else
                                FireScale = _lastSetupScale + newBulletScale;
                        }
                        break;

                    default:
                        {
                            FireScale = newBulletScale;
                        }
                        break;
                }
            }
            else
            {
                FireScale = 1f;
            }

            // Store setup direction/speed for sequence type
            _lastSetupDirection = FireDirection;
            _lastSetupSpeed = FireSpeed;
            _lastSetupScale = FireScale;
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet.
        /// This is called once per frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running.</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override TaskRunStatus Run(Bullet bullet)
        {
            _isRunning = true;

            // Create the new bullet
            var newBullet = bullet.BulletManager.CreateBullet();
            newBullet.X = bullet.X;
            newBullet.Y = bullet.Y;

            // Set the new bullet's direction
            newBullet.Direction = FireDirection;

            // Set the new bullet's speed
            newBullet.Speed = FireSpeed;

            // Set the new bullet's speed
            newBullet.Scale = FireScale;

            // Set the new bullet's sprite index
            if (SpriteTask != null)
                newBullet.SpriteIndex = (short)SpriteTask.GetNodeValue(bullet);

            // Store the new bullet's direction and speed for the sequence type
            _lastFireDirection = FireDirection;
            _lastFireSpeed = FireSpeed;
            _lastFireScale = FireScale;

            // Initialize the bullet with the bullet node stored in the fire node
            var fireNode = Node as FireNode;
            Debug.Assert(null != fireNode);
            newBullet.InitNode(fireNode.BulletDescriptionNode);

            // Set the owner of all the top level tasks for the new bullet to this task
            foreach (var task in newBullet.Tasks)
                task.Owner = this;

            Finished = true;

            return TaskRunStatus.End;
        }

        /// <summary>
        /// Retrieve the bullet or bulletRef nodes from a fire task.
        /// </summary>
        /// <param name="task">Task to check if has a child bullet or bulletRef node.</param>
        /// <param name="bullet">Associated bullet.</param>
        private void GetBulletNode(BulletMLTask task, Bullet bullet)
        {
            if (task == null)
                return;

            // Check if there is a bullet or a bulletRef node
            var bulletNode = task.Node.GetChild(NodeName.bullet) as BulletNode;
            var bulletRefNode = task.Node.GetChild(NodeName.bulletRef) as BulletRefNode;

            if (bulletNode != null)
            {
                // Create a task for the bullet ref
                BulletTask = new BulletMLTask(bulletNode, this);
                BulletTask.ParseTasks(bullet);
                ChildTasks.Add(BulletTask);
            }
            else if (bulletRefNode != null)
            {
                // Create a task for the bullet ref
                BulletTask = new BulletMLTask(bulletRefNode.ReferencedBulletNode, this);

                // Populate the params of the bullet ref
                foreach (var node in bulletRefNode.ChildNodes)
                    BulletTask.Params.Add(node.GetValue(this));

                BulletTask.ParseTasks(bullet);
                ChildTasks.Add(BulletTask);
            }
            else
            {
                throw new InvalidDataException("A <fire> node must contain a <bullet> or a <bulletRef> node.");
            }
        }

        /// <summary>
        /// Given a node, pull the direction nodes out from underneath it and store them if necessary.
        /// </summary>
        /// <param name="taskToCheck">task to check if has a child direction node.</param>
        private void GetDirectionTasks(BulletMLTask taskToCheck)
        {
            // Check if this fire task has a direction node
            var directionNode = taskToCheck?.Node.GetChild(NodeName.direction) as DirectionNode;

            if (directionNode == null) return;

            if (DirectionTask == null)
                DirectionTask = new DirectionTask(directionNode, taskToCheck);
        }

        /// <summary>
        /// Given a node, pull the speed nodes out from underneath it and store them if necessary.
        /// </summary>
        /// <param name="taskToCheck">Node to check.</param>
        private void GetSpeedNodes(BulletMLTask taskToCheck)
        {
            // Check if this fire task has a speed node
            var speedNode = taskToCheck?.Node.GetChild(NodeName.speed) as SpeedNode;

            if (speedNode == null) return;

            if (SpeedTask == null)
                SpeedTask = new SpeedTask(speedNode, taskToCheck);
        }

        /// <summary>
        /// Given a node, pull the scale nodes out from underneath it and store them if necessary.
        /// </summary>
        /// <param name="taskToCheck">Node to check.</param>
        private void GetScaleNodes(BulletMLTask taskToCheck)
        {
            // Check if this fire task has a scale node
            var scaleNode = taskToCheck?.Node.GetChild(NodeName.scale) as ScaleNode;

            if (scaleNode == null) return;

            if (ScaleTask == null)
                ScaleTask = new ScaleTask(scaleNode, taskToCheck);
        }

        /// <summary>
        /// Given a node, pull the sprite nodes out from underneath it and store them if necessary.
        /// </summary>
        /// <param name="taskToCheck">Node to check.</param>
        private void GetSpriteNodes(BulletMLTask taskToCheck)
        {
            // Check if this fire task has a sprite node
            var spriteNode = taskToCheck?.Node.GetChild(NodeName.sprite) as SpriteNode;

            if (spriteNode == null) return;

            if (SpriteTask == null)
                SpriteTask = new SpriteTask(spriteNode, taskToCheck);
        }

        #endregion Methods
    }
}