using BulletML.Nodes;
using System.Diagnostics;

namespace BulletML.Tasks
{
    /// <summary>
    /// This action sets the velocity of a bullet
    /// </summary>
    public class SpeedTask : BulletMLTask
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public SpeedTask(SpeedNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        #endregion Methods
    }
}