using BulletML.Enums;
using BulletML.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This task changes the speed a little bit every frame.
    /// </summary>
    public class ChangeColorTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The color pulled out from the node.
        /// </summary>
        private Color _nodeColor;

        private Color _initialColor;
        private float _initialDuration;

        /// <summary>
        /// How long to run this task, measured in frames.
        /// </summary>
        private float Duration { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeColorTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ChangeColorTask(ChangeColorNode node, BulletMLTask owner) : base(node, owner)
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

            var colorNode = Node.GetChild(NodeName.color);

            float red = (colorNode.GetChild(NodeName.red) != null) ? colorNode.GetChildValue(NodeName.red, this, bullet) : (bullet.Color.R / 255f);
            float green = (colorNode.GetChild(NodeName.green) != null) ? colorNode.GetChildValue(NodeName.green, this, bullet) : (bullet.Color.G / 255f);
            float blue = (colorNode.GetChild(NodeName.blue) != null) ? colorNode.GetChildValue(NodeName.blue, this, bullet) : (bullet.Color.B / 255f);
            float alpha = (colorNode.GetChild(NodeName.alpha) != null) ? colorNode.GetChildValue(NodeName.alpha, this, bullet) : (bullet.Color.A / 255f);
            float opacity = (colorNode.GetChild(NodeName.opacity) != null) ? colorNode.GetChildValue(NodeName.opacity, this, bullet) : 1f;

            _nodeColor = new Color(red, green, blue, alpha) * opacity;
        }

        private Color GetColor(Bullet bullet)
        {
            return Color.Lerp(_initialColor, _nodeColor, 1f - (Duration / _initialDuration));
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet.
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>TaskRunStatus: whether this task is done, paused, or still running.</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override TaskRunStatus Run(Bullet bullet)
        {
            if (Duration == _initialDuration)
                _initialColor = bullet.Color;

            bullet.Color = GetColor(bullet);

            Duration--;

            if (Duration >= 0) return TaskRunStatus.Continue;

            Finished = true;
            return TaskRunStatus.End;

            // Since this task isn't finished, run it again next time
        }

        #endregion Methods
    }
}