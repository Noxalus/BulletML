using BulletML.Enums;
using BulletML.Nodes;
using System;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This task changes the speed a little bit every frame.
    /// </summary>
    public class ChangeSpeedTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The speed amount pulled out from the node.
        /// </summary>
        private float _nodeSpeed;

        /// <summary>
        /// The type of speed change, pulled out from the node.
        /// </summary>
        private NodeType _changeType;

        private float _initialDuration;

        /// <summary>
        /// How long to run this task, measured in frames.
        /// </summary>
        private float Duration { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeSpeedTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ChangeSpeedTask(ChangeSpeedNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        /// <summary>
        /// This sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            // Set the length of time to run this task
            Duration = Node.GetChildValue(NodeName.term, this, bullet);

            // Check for divide by 0
            if (Math.Abs(Duration) < float.Epsilon)
                Duration = 1.0f;

            _initialDuration = Duration;

            _nodeSpeed = Node.GetChildValue(NodeName.speed, this, bullet);
            _changeType = Node.GetChild(NodeName.speed).NodeType;
        }

        private float GetSpeed(Bullet bullet)
        {
            switch (_changeType)
            {
                case NodeType.sequence:
                    {
                        return _nodeSpeed;
                    }

                case NodeType.relative:
                    {
                        return _nodeSpeed / _initialDuration;
                    }

                default:
                    {
                        // Absolute
                        return (_nodeSpeed - bullet.Speed) / Duration;
                    }
            }
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet.
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running.</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override TaskRunStatus Run(Bullet bullet)
        {
            bullet.Speed += GetSpeed(bullet);

            Duration--;

            if (Duration > 0) return TaskRunStatus.Continue;

            Finished = true;
            return TaskRunStatus.End;

            // Since this task isn't finished, run it again next time
        }

        #endregion Methods
    }
}