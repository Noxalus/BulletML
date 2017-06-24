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
        private ColorNode _colorNode;

        public Color Color
        {
            get { return _colorNode.Color; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ColorTask(BulletMLNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);

            var colorNode = node as ColorNode;

            Debug.Assert(null != colorNode);

            _colorNode = colorNode;
        }
    }
}
