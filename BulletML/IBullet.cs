using BulletML.Nodes;
using BulletML.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BulletML
{
    /// <summary>
    /// If you want a thing to be bullet-like, you can inherit from this interface.
    /// Useful if you have an object that encapsulates a bullet.
    /// </summary>
    public interface IBullet
    {
        /// <summary>
        /// A list of tasks that will define this bullets behavior.
        /// </summary>
        List<BulletMLTask> Tasks { get; }

        /// <summary>
        /// Abstract property to get or set the X location of this bullet.
        /// Measured in pixels from upper left.
        /// </summary>
        /// <value>The horizontal position in pixels.</value>
        float X { get; set; }

        /// <summary>
        /// Abstract property to get or set the Y location of this bullet.
        /// Measured in pixels from upper left.
        /// </summary>
        /// <value>The vertical position in pixels.</value>
        float Y { get; set; }

        /// <summary>
        /// Gets or sets the bullet's speed.
        /// </summary>
        /// <value>The speed, in pixels/frame.</value>
        float Speed { get; set; }

        /// <summary>
        /// Gets or sets the bullet's direction.
        /// </summary>
        /// <value>The direction in radians.</value>
        float Direction { get; set; }

        /// <summary>
        /// Gets or sets the bullet's color.
        /// </summary>
        /// <value>The RGBA color.</value>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the bullet's scale.
        /// </summary>
        /// <value>The scale.</value>
        float Scale { get; set; }

        /// <summary>
        /// Initialize this bullet with a top level node.
        /// </summary>
        /// <param name="rootNode">This is a top level node. Find the first "top" node and use it to define this bullet.</param>
        void InitTopNode(BulletMLNode rootNode);

        /// <summary>
        /// This bullet is fired from another bullet, initialize it from the node that fired it.
        /// </summary>
        /// <param name="subNode">Sub node that defines this bullet.</param>
        void InitNode(BulletMLNode subNode);
    }
}