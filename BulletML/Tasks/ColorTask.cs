using BulletML.Enums;
using BulletML.Nodes;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This action sets the color of a bullet
    /// </summary>
    public class ColorTask : BulletMLTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ColorTask(BulletMLNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        /// <summary>
        /// This sets up the task to be run.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        public override void InitTask(Bullet bullet)
        {
            // Set the length of time to run this task
            float red = Node.GetChildValue(NodeName.red, this, bullet);
            float green = Node.GetChildValue(NodeName.green, this, bullet);
            float blue = Node.GetChildValue(NodeName.blue, this, bullet);
            float alpha = Node.GetChildValue(NodeName.alpha, this, bullet);
            float opacity = Node.GetChildValue(NodeName.opacity, this, bullet);

            bullet.Color = new Color(red, green, blue, alpha) * opacity;
        }
    }
}
