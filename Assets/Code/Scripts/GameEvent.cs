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
        /// Detects when the player has collected a piece of plastic.
        /// </summary>
        public static event PlasticCollectionHandler OnPlasticCollect;
        
        /// <summary>
        /// Detects when the player has completed a level.
        /// </summary>
        public static event LevelCompleteHandler OnLevelComplete;
        
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
    }
}