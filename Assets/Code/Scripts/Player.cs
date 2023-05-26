using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// The instance of the player available to all classes.
        /// </summary>
        public static Player Instance = null;
        
        /// <summary>
        /// The tiers of plastic the player is able to collect. By default starts with Tier 1.
        /// </summary>
        public List<Plastic.PlasticTier> allowedPlasticTiers = new() { Plastic.PlasticTier.Tier1 };
        
        /// <summary>
        /// The number of plastics the player has collected.
        /// </summary>
        private int _plasticsCollected = 0;
        
        /// <summary>
        /// The plastic points the player has accumulated.
        /// </summary>
        private int _plasticPoints = 0;
        
        /// <summary>
        /// The player's movement component.
        /// </summary>
        private SubmarineMovement _movement;
        
        /// <summary>
        /// The player's collider component.
        /// </summary>
        private CapsuleCollider _collider;

        /// <summary>
        /// Creates a singleton instance of this class and subscribes to game events.
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
            
            _movement = GetComponent<SubmarineMovement>();
            _collider = GetComponent<CapsuleCollider>();
            
            GameEvent.OnPlasticCollect += CollectPlastic;
        }

        /// <summary>
        /// Called by the level manager at the start of a scene to load the player's saved plastic values.
        /// </summary>
        /// <param name="plasticPoints">The saved plastic points.</param>
        /// <param name="plasticsCollected">The saved number of plastics collected.</param>
        public void SetPlasticValues(int plasticPoints, int plasticsCollected)
        {
            _plasticPoints = plasticPoints;
            _plasticsCollected = plasticsCollected;
        }

        /// <summary>
        /// Called by the level manager at the start of a scene to load the player's saved upgrades.
        /// </summary>
        /// <param name="speedMultiplier"></param>
        /// <param name="collisionRadius"></param>
        public void SetUpgrades(float speedMultiplier, float collisionRadius)
        {
            _movement.UpdateSpeed(speedMultiplier);
            _collider.radius = collisionRadius;
        }

        /// <summary>
        /// When the player collects a piece of plastic, add to the plastic points and increment the number of plastics
        /// collected. Saves the player's plastic points and plastics collected to the ProgressionSaver.
        /// </summary>
        /// <param name="plasticPoints">The number of plastic points to add.</param>
        private void CollectPlastic(int plasticPoints)
        {
            _plasticPoints += plasticPoints;
            _plasticsCollected++;
            ProgressionSaver.Instance.UpdatePlasticValues(_plasticPoints, _plasticsCollected);
        }

        /// <summary>
        /// Returns the player's current plastic points total.
        /// </summary>
        /// <returns>How many plastic points the player currently has.</returns>
        public int GetPlasticPoints() => _plasticPoints;

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDestroy()
        {
            GameEvent.OnPlasticCollect -= CollectPlastic;
        }
    }
}