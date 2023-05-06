using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
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
        /// Subscribes to game events.
        /// </summary>
        private void Awake()
        {
            GameEvent.OnPlasticCollect += CollectPlastic;
        }

        /// <summary>
        /// When the player collects a piece of plastic, add to the plastic points and increment the number of plastics
        /// collected.
        /// </summary>
        /// <param name="plasticPoints">The number of plastic points to add.</param>
        private void CollectPlastic(int plasticPoints)
        {
            _plasticPoints += plasticPoints;
            _plasticsCollected++;
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDestroy()
        {
            GameEvent.OnPlasticCollect -= CollectPlastic;
        }
    }
}