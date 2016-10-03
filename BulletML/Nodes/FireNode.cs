using BulletML.Enums;
using System.Diagnostics;

namespace BulletML.Nodes
{
    public class FireNode : BulletMLNode
    {
        #region Members

        /// <summary>
        /// A bullet node this task will use to set any bullets shot from this task
        /// </summary>
        /// <value>The bullet node.</value>
        public BulletNode BulletDescriptionNode { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="FireNode"/> class.
        /// </summary>
        public FireNode() : this(NodeName.fire)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireNode"/> class.
        /// this is the constructor used by sub classes
        /// </summary>
        /// <param name="nodeName">the node type.</param>
        public FireNode(NodeName nodeName) : base(nodeName)
        {
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation
        /// </summary>
        public override void ValidateNode()
        {
            base.ValidateNode();

            // Check for a bullet node
            BulletDescriptionNode = GetChild(NodeName.bullet) as BulletNode;

            // If it didn't find one, check for the bulletRef node
            if (null == BulletDescriptionNode)
            {
                // Make sure that dude knows what he's doing
                var refNode = GetChild(NodeName.bulletRef) as BulletRefNode;

                if (refNode != null)
                {
                    refNode.FindMyBulletNode();
                    BulletDescriptionNode = refNode.ReferencedBulletNode;
                }
            }

            Debug.Assert(null != BulletDescriptionNode, "A <fire> node must contain a <bullet> or a <bulletRef> node.");
        }

        #endregion Methods
    }
}