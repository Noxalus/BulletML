using BulletML.Enums;
using BulletML.Tasks;
using System;

namespace BulletML.Nodes
{
    /// <summary>
    /// action node... also the base class for actionref nodes
    /// </summary>
    public class ActionNode : BulletMLNode
    {
        #region Members

        /// <summary>
        /// Gets or sets the parent repeat node.
        /// This is the node immediately above this one that says how many times to repeat this action.
        /// </summary>
        /// <value>The parent repeat node.</value>
        public RepeatNode ParentRepeatNode { get; private set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        public ActionNode() : this(NodeName.action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// this is the constructor used by sub classes
        /// </summary>
        /// <param name="nodeName">the node type.</param>
        public ActionNode(NodeName nodeName) : base(nodeName)
        {
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation
        /// </summary>
        public override void ValidateNode()
        {
            //Get our parent repeat node if we have one
            ParentRepeatNode = FindParentRepeatNode();

            //do any base class validation
            base.ValidateNode();
        }

        /// <summary>
        /// Finds the parent repeat node.
        /// This method is not recursive, since action and actionref nodes can be nested.
        /// </summary>
        /// <returns>The parent repeat node.</returns>
        private RepeatNode FindParentRepeatNode()
        {
            //Parent node should never ever be empty on an action node
            if (null == Parent)
            {
                throw new NullReferenceException("Parent node cannot be empty on an action or actionRef node");
            }

            //If the parent is a repeat node, check how many times to repeat this action
            if (Parent.Name == NodeName.repeat)
            {
                return Parent as RepeatNode;
            }

            //This dude is not under a repeat node
            return null;
        }

        /// <summary>
        /// Get the number of times this action should be repeated.
        /// </summary>
        /// <param name="myTask">The task to get the number of repeat times for.</param>
        /// <param name="bullet">The associated bullet.</param>
        /// <returns>The number of times to repeat this node, as specified by a parent repeat node.</returns>
        public int RepeatNum(ActionTask myTask, Bullet bullet)
        {
            if (null != ParentRepeatNode)
            {
                // Get the equation value of the repeat node
                return (int)ParentRepeatNode.GetChildValue(NodeName.times, myTask, bullet);
            }
            else
            {
                // No repeat nodes, just repeat it once
                return 1;
            }
        }

        #endregion Methods
    }
}