using BulletML.Nodes;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This action sets sprite index of a bullet
    /// </summary>
    public class SpriteTask : BulletMLTask
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public SpriteTask(SpriteNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        #endregion Methods
    }
}