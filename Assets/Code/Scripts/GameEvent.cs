namespace Code.Scripts
{
    public static class GameEvent
    {
        /// <summary>
        /// Handles the player's collection of plastic.
        /// </summary>
        public delegate void PlasticCollectionHandler(int plasticPoints);

        /// <summary>
        /// Detects when the player has collected a piece of plastic.
        /// </summary>
        public static event PlasticCollectionHandler OnPlasticCollect;
        
        /// <summary>
        /// Collects the piece of plastic and updates appropriate variables.
        /// </summary>
        /// <param name="plasticPoints">The number of plastic points to add to the player's score.</param>
        public static void CollectPlastic(int plasticPoints) => OnPlasticCollect?.Invoke(plasticPoints);
    }
}