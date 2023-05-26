using UnityEngine;

namespace Code.Scripts
{
    public class Plastic : MonoBehaviour
    {
        /// <summary>
        /// The potential tiers corresponding to pieces of plastic.
        /// </summary>
        public enum PlasticTier
        {
            Tier1, Tier2, Tier3, Tier4, Tier5
        }

        /// <summary>
        /// The tier corresponding to this piece of plastic.
        /// </summary>
        [SerializeField] private PlasticTier tier;
        
        /// <summary>
        /// The amount of plastic points this piece of plastic is worth.
        /// </summary>
        [SerializeField] private int plasticPoints = 15;

        /// <summary>
        /// Detects collisions with the player and adds the plastic points to the player's score if the player can
        /// collect plastics of this tier.
        /// </summary>
        /// <param name="other">The collision object.</param>
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("colliding");
            // get the player component
            Player player = other.collider.GetComponent<Player>();
            
            // if the player is null or the player cannot collect plastics of this tier, return
            if (!player || !player.allowedPlasticTiers.Contains(tier)) return;
            
            // janky way of doing it but that's fine
            plasticPoints = 15;
            
            // call the plastic collect event
            GameEvent.CollectPlastic(plasticPoints);
            gameObject.SetActive(false);
        }
    }
}