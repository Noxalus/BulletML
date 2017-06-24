using BulletML.Enums;
using BulletML.Tasks;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BulletML.Nodes
{
    /// <summary>
    /// This is a single node from a BulletML document.
    /// Used as the base node for all the other node types.
    /// </summary>
    public class BulletMLNode
    {
        #region Properties

        /// <summary>
        /// The XML node name of this item.
        /// </summary>
        public NodeName Name { get; private set; }

        /// <summary>
        /// Gets or sets the type of the node.
        /// This is virtual so sub-classes can override it and validate their own shit.
        /// </summary>
        /// <value>The type of the node.</value>
        public virtual NodeType NodeType { get; protected set; } = NodeType.unknown;

        /// <summary>
        /// The label of this node
        /// This can be used by other nodes to reference this node
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// An equation used to get a value of this node.
        /// </summary>
        /// <value>The node value.</value>
        private readonly BulletMLEquation _nodeEquation = new BulletMLEquation();

        /// <summary>
        /// A list of all the child nodes for this node.
        /// </summary>
        public List<BulletMLNode> ChildNodes { get; private set; }

        /// <summary>
        /// pointer to the parent node of this dude
        /// </summary>
        protected BulletMLNode Parent { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLNode"/> class.
        /// </summary>
        public BulletMLNode(NodeName nodeName)
        {
            ChildNodes = new List<BulletMLNode>();
            Name = nodeName;
        }

        /// <summary>
        /// Convert a string to its NodeType enum equivalent.
        /// </summary>
        /// <returns>NodeType: the enum value of that string.</returns>
        /// <param name="enumString">The string to convert to an enum.</param>
        private static NodeType StringToType(string enumString)
        {
            // Make sure there is something there
            if (string.IsNullOrEmpty(enumString))
                return NodeType.unknown;

            return (NodeType)Enum.Parse(typeof(NodeType), enumString);
        }

        /// <summary>
        /// Convert a string to its NodeName enum equivalent.
        /// </summary>
        /// <returns>NodeName: the enum value of that string.</returns>
        /// <param name="enumString">The string to convert to an enum.</param>
        private static NodeName StringToName(string enumString)
        {
            return (NodeName)Enum.Parse(typeof(NodeName), enumString);
        }

        /// <summary>
        /// Gets the root node.
        /// </summary>
        /// <returns>The root node.</returns>
        public BulletMLNode GetRootNode()
        {
            // Recurse up until we get to the root node
            if (null != Parent)
                return Parent.GetRootNode();

            // If it gets here, this is the root.
            return this;
        }

        /// <summary>
        /// Find a node of a specific type and label.
        /// Recurse into the XML tree until we find it!
        /// </summary>
        /// <returns>The label node.</returns>
        /// <param name="label">Label of the node we are looking for</param>
        /// <param name="name">name of the node we are looking for</param>
        public BulletMLNode FindLabelNode(string label, NodeName name)
        {
            // This uses breadth first search, since labelled nodes are usually top level

            // Check if any of our child nodes match the request
            foreach (var node in ChildNodes)
            {
                if ((name == node.Name) && (label == node.Label))
                    return node;
            }

            // Recurse into the child nodes and see if we find any matches
            foreach (var node in ChildNodes)
            {
                var foundNode = node.FindLabelNode(label, name);

                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        /// <summary>
        /// Find a parent node of the specified node type.
        /// </summary>
        /// <returns>The first parent node of that type, null if unknown found.</returns>
        /// <param name="nodeType">Node type to find.</param>
        public BulletMLNode FindParentNode(NodeName nodeType)
        {
            if (Parent == null)
                return null;

            if (nodeType == Parent.Name)
                return Parent;

            // Recurse into parent nodes to check grandparents, etc...
            return Parent.FindParentNode(nodeType);
        }

        /// <summary>
        /// Gets the value of a specific type of child node for a task.
        /// </summary>
        /// <returns>The child value. return 0.0 if no node found</returns>
        /// <param name="name">type of child node we want.</param>
        /// <param name="task">Task to get a value for.</param>
        /// <param name="bullet">Bullet to get a value for.</param>
        public float GetChildValue(NodeName name, BulletMLTask task, Bullet bullet)
        {
            foreach (var tree in ChildNodes)
            {
                if (name == tree.Name)
                    return tree.GetValue(task);
            }

            return 0f;
        }

        /// <summary>
        /// Get a direct child node of a specific type.
        /// Does not recurse!
        /// </summary>
        /// <returns>The child.</returns>
        /// <param name="name">Type of node we want. Null if not found.</param>
        public BulletMLNode GetChild(NodeName name)
        {
            foreach (var node in ChildNodes)
            {
                if (name == node.Name)
                    return node;
            }

            return null;
        }

        /// <summary>
        /// Gets the value of this node for a specific instance of a task.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="task">Task.</param>
        public float GetValue(BulletMLTask task)
        {
            // Send to the equation for an answer
            return (float)_nodeEquation.Solve(task.GetParamValue);
        }

        #region XML Methods

        /// <summary>
        /// Parse the specified bulletNodeElement.
        /// Read all the data from the XML node into this node.
        /// </summary>
        /// <param name="bulletNodeElement">Bullet node element.</param>
        /// <param name="parentNode">Parent node element.</param>
        public virtual void Parse(XmlNode bulletNodeElement, BulletMLNode parentNode)
        {
            // Handle null argument.
            if (bulletNodeElement == null)
                throw new ArgumentNullException(nameof(bulletNodeElement));

            // Grab the parent node
            Parent = parentNode;

            // Parse all attributes except for the root node
            if (Parent != null)
            {
                XmlNamedNodeMap mapAttributes = bulletNodeElement.Attributes;

                if (mapAttributes != null)
                {
                    for (var i = 0; i < mapAttributes.Count; i++)
                    {
                        var strName = mapAttributes.Item(i).Name;
                        var strValue = mapAttributes.Item(i).Value;

                        // Get the bullet node type
                        if (strName == AttributeName.type.ToString())
                            NodeType = StringToType(strValue);
                        // Label is just a text value
                        else if (strName == AttributeName.label.ToString())
                            Label = strValue;
                    }
                }
            }

            // Parse all the child nodes
            if (bulletNodeElement.HasChildNodes)
            {
                for (var childNode = bulletNodeElement.FirstChild;
                     childNode != null;
                     childNode = childNode.NextSibling)
                {
                    // Skip any comments in the bulletml script
                    if (childNode.NodeType == XmlNodeType.Comment)
                        continue;

                    // If the child node is a text node, parse it into this bullet
                    if (childNode.NodeType == XmlNodeType.Text)
                    {
                        // Get the text of the child xml node, but store it in THIS bullet node
                        _nodeEquation.Parse(childNode.Value);
                        continue;
                    }

                    // Create a new node
                    var childBulletNode = NodeFactory.CreateNode(StringToName(childNode.Name));

                    // Read in the node and store it
                    childBulletNode.Parse(childNode, this);
                    ChildNodes.Add(childBulletNode);
                }
            }
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation.
        /// </summary>
        public virtual void ValidateNode()
        {
            // Validate all the child nodes
            foreach (var childnode in ChildNodes)
                childnode.ValidateNode();
        }

        #endregion XML Methods

        #endregion Methods
    }
}