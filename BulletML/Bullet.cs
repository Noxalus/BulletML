using BulletML.Enums;
using BulletML.Nodes;
using BulletML.Tasks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulletML
{
    /// <summary>
    /// This is the bullet class that outside assemblies will interact with.
    /// Just inherit from this class and override the abstract functions!
    /// </summary>
    public abstract class Bullet : IBullet
    {
        #region Members

        /// <summary>
        /// The direction this bullet is travelling.
        /// Measured as an angle in radians.
        /// </summary>
        private float _direction;

        /// <summary>
        /// The bullet manager that manages this bullet.
        /// </summary>
        /// <value>The bullet manager.</value>
        private readonly IBulletManager _bulletManager;

        #endregion Members

        #region Properties

        /// <summary>
        /// Abstract property to get the X location of this bullet.
        /// measured in pixels from upper left.
        /// </summary>
        /// <value>The horizontal position.</value>
        public abstract float X { get; set; }

        /// <summary>
        /// Gets or sets the y parameter of the location
        /// measured in pixels from upper left.
        /// </summary>
        /// <value>The vertical position.</value>
        public abstract float Y { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction in radians.</value>
        public virtual float Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
            }
        }

        /// <summary>
        /// The bullet's acceleration.
        /// </summary>
        /// <value>The acceleration, in pixels/frame^2.</value>
        public Vector2 Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the speed
        /// </summary>
        /// <value>The speed, in pixels/frame.</value>
        public virtual float Speed { get; set; }

        /// <summary>
        /// A list of tasks that will define this bullet behavior.
        /// </summary>
        public List<BulletMLTask> Tasks { get; private set; }

        /// <summary>
        /// The tree node that describes this bullet.
        /// These are shared between multiple bullets.
        /// </summary>
        private BulletMLNode BulletNode { get; set; }

        /// <summary>
        /// Get the bullet manager.
        /// </summary>
        /// <value>The bullet manager.</value>
        public IBulletManager BulletManager => _bulletManager;

        /// <summary>
        /// Convenience property to get the bullet's label.
        /// </summary>
        /// <value>The label.</value>
        public string Label => BulletNode.Label;

        /// <summary>
        /// This is the initial velocity of the bullet when it is fired.
        /// For example, if the enemy is moving forward and fires bullets, they will clump
        /// together because they don't retain the enemy's velocity.
        /// Set this property if you have fast moving enemies or guns and want the bullet
        /// pattern to inherit the velocity of the object that fired them.
        /// </summary>
        public Vector2 InitialVelocity { get; set; } = Vector2.Zero;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletML.Bullet"/> class.
        /// </summary>
        /// <param name="bulletManager">A bullet manager.</param>
        protected Bullet(IBulletManager bulletManager)
        {
            Debug.Assert(null != bulletManager);

            _bulletManager = bulletManager;
            Acceleration = Vector2.Zero;
            Tasks = new List<BulletMLTask>();
        }

        /// <summary>
        /// Initialize this bullet with a top level node.
        /// </summary>
        /// <param name="rootNode">
        /// This is a top level node.
        /// Find the first "top" node and use it to define this bullet.
        /// </param>
        public void InitTopNode(BulletMLNode rootNode)
        {
            Debug.Assert(rootNode != null);

            // Find the item labelled "top", it's our entry point
            var validBullet = false;
            var topNode = rootNode.FindLabelNode("top", NodeName.action);

            if (topNode != null)
            {
                // Initialize with the top node we found!
                InitNode(topNode);
                validBullet = true;
            }
            else
            {
                // There is no "top" node, so that means we have a list of "top#" nodes
                for (int i = 1; i < 10; i++)
                {
                    topNode = rootNode.FindLabelNode("top" + i, NodeName.action);

                    if (topNode != null)
                    {
                        if (!validBullet)
                        {
                            // Use this bullet!
                            InitNode(topNode);
                            validBullet = true;
                        }
                        else
                        {
                            // Create a new bullet
                            var bullet = _bulletManager.CreateBullet(true);

                            // Set the position to this dude's position
                            bullet.X = this.X;
                            bullet.Y = this.Y;

                            // Initialize with the node we found
                            bullet.InitNode(topNode);
                        }
                    }
                }
            }

            if (!validBullet)
            {
                // We didn't find a "top" node, we remove this bullet from the game.
                _bulletManager.RemoveBullet(this);
            }
        }

        /// <summary>
        /// This bullet is fired from another bullet, initialize it from the node that fired it.
        /// </summary>
        /// <param name="node">Sub node that defines this bullet.</param>
        public void InitNode(BulletMLNode node)
        {
            Debug.Assert(null != node);

            // Clear every tasks
            Tasks.Clear();

            // Grab that top level node
            BulletNode = node;

            // Found a top num node, add a task for it
            var task = new BulletMLTask(node, null);

            // Parse the nodes into the task list
            task.ParseTasks(this);

            // Initialize all the tasks
            task.InitTask(this);

            Tasks.Add(task);
        }

        /// <summary>
        /// Update this bullet.
        /// Called once every 1/60th of a second during runtime.
        /// </summary>
        public virtual void Update()
        {
            // Flag to tell whether or not this bullet has finished all its tasks
            foreach (var task in Tasks)
                task.Run(this);

            // Only do this if the bullet isn't done, sin/cos really are expensive
            var direction = new Vector2((float)(Math.Sin(Direction)), (float)(-Math.Cos(Direction)));
            var velocity = Acceleration + (direction * Speed);

            X += velocity.X;
            Y += velocity.Y;
        }

        /// <summary>
        /// Get the direction to aim.
        /// </summary>
        /// <returns>Angle to target the aimed position.</returns>
        public virtual float GetAimDirection()
        {
            Debug.Assert(null != BulletManager);

            var playerPosition = BulletManager.PlayerPosition(this);

            return (float)Math.Atan2((playerPosition.X - X), -(playerPosition.Y - Y));
        }

        /// <summary>
        /// Finds the task by label.
        /// This recurses into child tasks to find the taks with the correct label.
        /// Used only for unit testing!
        /// </summary>
        /// <returns>The task by label.</returns>
        /// <param name="strLabel">String label.</param>
        public BulletMLTask FindTaskByLabel(string strLabel)
        {
            // Check if any of the child tasks have a task with that label
            foreach (var childTask in Tasks)
            {
                var foundTask = childTask.FindTaskByLabel(strLabel);

                if (foundTask != null)
                    return foundTask;
            }

            return null;
        }

        /// <summary>
        /// Given a label and name, find the task that matches.
        /// </summary>
        /// <returns>The task by label and name.</returns>
        /// <param name="strLabel">String label of the task.</param>
        /// <param name="name">The name of the node the task should be attached to.</param>
        public BulletMLTask FindTaskByLabelAndName(string strLabel, NodeName name)
        {
            // Check if any of the child tasks have a task with that label
            foreach (var childTask in Tasks)
            {
                var foundTask = childTask.FindTaskByLabelAndName(strLabel, name);

                if (foundTask != null)
                    return foundTask;
            }

            return null;
        }

        /// <summary>
        /// Check if this bullet has finished running all its tasks.
        /// </summary>
        /// <returns></returns>
        public bool TasksFinished()
        {
            foreach (var task in Tasks)
            {
                if (!task.Finished)
                    return false;
            }

            // All the tasks and their child tasks are done running
            return true;
        }

        #endregion Methods
    }
}