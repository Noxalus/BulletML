using BulletML.Enums;
using BulletML.Nodes;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This task pauses for a specified amount of time before resuming.
    /// </summary>
    public class WaitTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// How long to run this task measured in frames.
        /// This task will pause until the duration runs out, then resume running tasks.
        /// </summary>
        private int Duration { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public WaitTask(WaitNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(Node != null);
            Debug.Assert(Owner != null);
        }

        /// <summary>
        /// This sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            Duration = (int)(Node.GetValue(this));
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet.
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running.</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override TaskRunStatus Run(Bullet bullet)
        {
            Duration--;

            if (Duration >= 0.0f)
                return TaskRunStatus.Stop;

            Finished = true;
            return TaskRunStatus.End;
        }

        #endregion Methods
    }
}