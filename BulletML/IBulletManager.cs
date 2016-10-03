using Microsoft.Xna.Framework;

namespace BulletML
{
    /// <summary>
    /// This is the interface that outisde assemblies will use to manage bullets.
    /// Mostly for creating and destroying them.
    /// </summary>
    public interface IBulletManager
    {
        #region Methods

        /// <summary>
        /// A method to get the current player's position.
        /// This is used to target bullets at that position.
        /// </summary>
        /// <returns>The position to aim the bullet at</returns>
        /// <param name="targettedBullet">The bullet we are getting a target for.</param>
        Vector2 PlayerPosition(IBullet targettedBullet);

        /// <summary>
        /// A bullet is done being used, do something to get rid of it.
        /// </summary>
        /// <param name="deadBullet">the Dead bullet.</param>
        void RemoveBullet(IBullet deadBullet);

        /// <summary>
        /// Create a new bullet.
        /// </summary>
        /// <param name="topBullet">
        /// A boolean to specify if it's a top bullet.
        /// These are usually special bullets that dont need to be drawn or kept around after they finish tasks, etc...
        /// </param>
        /// <returns>A shiny new bullet.</returns>
		IBullet CreateBullet(bool topBullet = false);

        #endregion Methods
    }
}