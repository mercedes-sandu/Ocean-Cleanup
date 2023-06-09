using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class UpgradeButton : MonoBehaviour
    {
        /// <summary>
        /// The sprite to use when the upgrade is unlocked.
        /// </summary>
        [SerializeField] private Sprite unlockedSprite;

        /// <summary>
        /// The sprite to use when the upgrade is locked.
        /// </summary>
        [SerializeField] private Sprite lockedSprite;
        
        /// <summary>
        /// The sprite to use when the upgrade is available to purchase.
        /// </summary>
        [SerializeField] private Sprite availableSprite;

        /// <summary>
        /// The image component.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The button component.
        /// </summary>
        private Button _button;

        /// <summary>
        /// Initializes components.
        /// </summary>
        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }
        
        /// <summary>
        /// Updates the appearance and intractability of the buttons.
        /// </summary>
        /// <param name="unlocked">True if the upgrade is unlocked, false otherwise.</param>
        /// <param name="canPurchase">True if the upgrade can be purchased, false otherwise.</param>
        public void UpdateButton(bool unlocked, bool canPurchase)
        {
            _image.sprite = canPurchase ? availableSprite : unlocked ? unlockedSprite : lockedSprite;
            _button.interactable = canPurchase;
        }
    }
}