using UnityEngine;

namespace Code.Scripts
{
    public class ProgressionSaver : MonoBehaviour
    {
        /// <summary>
        /// Public instance of this singleton available by all classes.
        /// </summary>
        public static ProgressionSaver Instance = null;

        /// <summary>
        /// The player's saved number of plastic points.
        /// </summary>
        public int playerPlasticPoints = 0;

        /// <summary>
        /// The player's saved number of plastics collected.
        /// </summary>
        public int playerPlasticsCollected = 0;
        
        /// <summary>
        /// Creates a singleton instance of this class and prevents this object from being destroyed on scene load.
        /// </summary>
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this);
        }
        
        /// <summary>
        /// Called by the Player class whenever plastic is collected to update saved information.
        /// </summary>
        /// <param name="plasticPoints">The saved number of plastic points.</param>
        /// <param name="plasticsCollected">The saved number of plastics collected.</param>
        public void UpdatePlasticValues(int plasticPoints, int plasticsCollected)
        {
            playerPlasticPoints = plasticPoints;
            playerPlasticsCollected = plasticsCollected;
        }
    }
}