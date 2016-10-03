namespace BulletML
{
    /// <summary>
    /// This thing manages a few gameplay variables that used by the BulletML library
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Callback method to get the game difficulty.
        /// You need to set this at the start of the game.
        /// </summary>
        public static FloatDelegate GameDifficulty;
    }
}