namespace Code.Scripts
{
    public static class GameEvent
    {
        /// <summary>
        /// Handles the player's collection of plastic.
        /// </summary>
        public delegate void PlasticCollectionHandler(int plasticPoints);

        /// <summary>
        /// Handles the player's completion of levels.
        /// </summary>
        public delegate void LevelCompleteHandler(string nextLevel);

        /// <summary>
        /// Handles the player's speed multiplier upgrades.
        /// </summary>
        public delegate void SpeedUpgradeHandler(float speedMultiplier);
        
        /// <summary>
        /// Handles the player's collision radius upgrades.
        /// </summary>
        public delegate void CollisionRadiusUpgradeHandler(float collisionRadius);

        /// <summary>
        /// Detects when the player has collected a piece of plastic.
        /// </summary>
        public static event PlasticCollectionHandler OnPlasticCollect;
        
        /// <summary>
        /// Detects when the player has completed a level.
        /// </summary>
        public static event LevelCompleteHandler OnLevelComplete;
        
        /// <summary>
        /// Detects when the player has upgraded their speed.
        /// </summary>
        public static event SpeedUpgradeHandler OnSpeedUpgrade;
        
        /// <summary>
        /// Detects when the player has upgraded their collision radius.
        /// </summary>
        public static event CollisionRadiusUpgradeHandler OnCollisionRadiusUpgrade;

        /// <summary>
        /// Collects the piece of plastic and updates appropriate variables.
        /// </summary>
        /// <param name="plasticPoints">The number of plastic points to add to the player's score.</param>
        public static void CollectPlastic(int plasticPoints) => OnPlasticCollect?.Invoke(plasticPoints);
        
        /// <summary>
        /// Completes this level and loads the next one.
        /// </summary>
        /// <param name="nextLevel">The string of the scene of the next level.</param>
        public static void LevelComplete(string nextLevel) => OnLevelComplete?.Invoke(nextLevel);
        
        /// <summary>
        /// Updates the speed multiplier of the player.
        /// </summary>
        /// <param name="speedMultiplier">The new speed multiplier.</param>
        public static void UpgradeSpeed(float speedMultiplier) => OnSpeedUpgrade?.Invoke(speedMultiplier);

        /// <summary>
        /// Updates the collision radius of the player.
        /// </summary>
        /// <param name="collisionRadius">The new collision radius.</param>
        public static void UpgradeCollisionRadius(float collisionRadius) =>
            OnCollisionRadiusUpgrade?.Invoke(collisionRadius);
    }
}