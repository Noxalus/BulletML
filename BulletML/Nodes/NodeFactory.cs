using BulletML.Enums;
using System;

namespace BulletML.Nodes
{
    /// <summary>
    /// This is a simple class used to create different types of nodes.
    /// </summary>
    public static class NodeFactory
    {
        /// <summary>
        /// Given a node type, create the correct node.
        /// </summary>
        /// <returns>An instance of the correct node type</returns>
        /// <param name="nodeType">Node type that we want.</param>
        public static BulletMLNode CreateNode(NodeName nodeType)
        {
            switch (nodeType)
            {
                case NodeName.bullet:
                    {
                        return new BulletNode();
                    }
                case NodeName.action:
                    {
                        return new ActionNode();
                    }
                case NodeName.fire:
                    {
                        return new FireNode();
                    }
                case NodeName.changeDirection:
                    {
                        return new ChangeDirectionNode();
                    }
                case NodeName.changeSpeed:
                    {
                        return new ChangeSpeedNode();
                    }
                case NodeName.accel:
                    {
                        return new AccelNode();
                    }
                case NodeName.wait:
                    {
                        return new WaitNode();
                    }
                case NodeName.repeat:
                    {
                        return new RepeatNode();
                    }
                case NodeName.bulletRef:
                    {
                        return new BulletRefNode();
                    }
                case NodeName.actionRef:
                    {
                        return new ActionRefNode();
                    }
                case NodeName.fireRef:
                    {
                        return new FireRefNode();
                    }
                case NodeName.vanish:
                    {
                        return new VanishNode();
                    }
                case NodeName.horizontal:
                    {
                        return new HorizontalNode();
                    }
                case NodeName.vertical:
                    {
                        return new VerticalNode();
                    }
                case NodeName.term:
                    {
                        return new TermNode();
                    }
                case NodeName.times:
                    {
                        return new TimesNode();
                    }
                case NodeName.direction:
                    {
                        return new DirectionNode();
                    }
                case NodeName.speed:
                    {
                        return new SpeedNode();
                    }
                case NodeName.color:
                    {
                        return new ColorNode();
                    }
                case NodeName.red:
                    {
                        return new RedNode();
                    }
                case NodeName.green:
                    {
                        return new GreenNode();
                    }
                case NodeName.blue:
                    {
                        return new BlueNode();
                    }
                case NodeName.alpha:
                    {
                        return new AlphaNode();
                    }
                case NodeName.opacity:
                    {
                        return new OpacityNode();
                    }
                case NodeName.changeColor:
                    {
                        return new ChangeColorNode();
                    }
                case NodeName.scale:
                    {
                        return new ScaleNode();
                    }
                case NodeName.changeScale:
                    {
                        return new ChangeScaleNode();
                    }
                case NodeName.sprite:
                    {
                        return new SpriteNode();
                    }
                case NodeName.param:
                    {
                        return new ParamNode();
                    }
                case NodeName.bulletml:
                    {
                        return new BulletMLNode(NodeName.bulletml);
                    }
                default:
                    {
                        throw new Exception("Unhandled type of NodeName: \"" + nodeType.ToString() + "\"");
                    }
            }
        }
    }
}