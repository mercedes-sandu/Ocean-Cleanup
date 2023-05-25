using UnityEngine;

namespace Code.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// The name of the scene of the next level.
        /// </summary>
        [SerializeField] private string nextLevelName;
        
        /// <summary>
        /// The number of plastics required to complete this level.
        /// </summary>
        private int _numPlastics;

        /// <summary>
        /// The current number of collected plastics.
        /// </summary>
        private int _plasticsCollected = 0;

        /// <summary>
        /// Subscribes to game events.
        /// </summary>
        private void Awake()
        {
            GameEvent.OnPlasticCollect += PlasticCollected;
        }
        
        /// <summary>
        /// Calculates the number of plastics required to collect to complete the level. Load's the players saved data.
        /// </summary>
        private void Start()
        {
            _numPlastics = FindObjectsOfType<Plastic>().Length;
            
            Player.Instance.SetPlasticValues(ProgressionSaver.Instance.playerPlasticPoints,
                ProgressionSaver.Instance.playerPlasticsCollected);
            Player.Instance.SetUpgrades(ProgressionSaver.Instance.playerSpeedMultiplier,
                ProgressionSaver.Instance.playerCollisionRadius);
        }

        /// <summary>
        /// Checks if the player has collected the required number of plastics to complete the level.
        /// </summary>
        /// <param name="plasticPoints"></param>
        private void PlasticCollected(int plasticPoints)
        {
            _plasticsCollected++;
            if (_plasticsCollected != _numPlastics) return;
            GameEvent.LevelComplete(nextLevelName);
        }
    }
}