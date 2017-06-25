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
            float red = (Node.GetChild(NodeName.red) != null) ? Node.GetChildValue(NodeName.red, this, bullet) : bullet.Color.R;
            float green = (Node.GetChild(NodeName.green) != null) ? Node.GetChildValue(NodeName.green, this, bullet) : bullet.Color.G;
            float blue = (Node.GetChild(NodeName.blue) != null) ? Node.GetChildValue(NodeName.blue, this, bullet) : bullet.Color.B;
            float alpha = (Node.GetChild(NodeName.alpha) != null) ? Node.GetChildValue(NodeName.alpha, this, bullet) : bullet.Color.A;
            float opacity = (Node.GetChild(NodeName.opacity) != null) ? Node.GetChildValue(NodeName.opacity, this, bullet) : 1f;

            bullet.Color = new Color(red, green, blue, alpha) * opacity;
        }
    }
}
