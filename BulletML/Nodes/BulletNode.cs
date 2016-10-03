using BulletML.Enums;

namespace BulletML.Nodes
{
    public class BulletNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulletNode"/> class.
        /// </summary>
        public BulletNode() : this(NodeName.bullet)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletNode"/> class.
        /// this is the constructor used by sub classes
        /// </summary>
        /// <param name="nodeName">the node type.</param>
        protected BulletNode(NodeName nodeName) : base(nodeName)
        {
        }
    }
}