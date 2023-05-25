using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Scripts
{
    public class UpgradeMenu : MonoBehaviour
    {
        /// <summary>
        /// A singleton instance of the upgrade menu available to all classes.
        /// </summary>
        public static UpgradeMenu Instance = null;

        /// <summary>
        /// The list of buttons that upgrade the player's speed multiplier.
        /// </summary>
        [SerializeField] private List<UpgradeButton> speedMultiplierButtons;
        
        /// <summary>
        /// The list of buttons that upgrade the player's collision radius.
        /// </summary>
        [SerializeField] private List<UpgradeButton> collisionRadiusButtons;
        
        /// <summary>
        /// The list of text that displays the cost of each speed multiplier upgrade.
        /// </summary>
        [SerializeField] private List<TextMeshProUGUI> speedMultiplierCostTexts;
        
        /// <summary>
        /// The list of text that displays the cost of each collision radius upgrade.
        /// </summary>
        [SerializeField] private List<TextMeshProUGUI> collisionRadiusCostTexts;

        /// <summary>
        /// The text that displays the player's plastic points.
        /// </summary>
        [SerializeField] private TextMeshProUGUI plasticPointsText;
        
        /// <summary>
        /// The canvas component.
        /// </summary>
        private Canvas _canvas;

        /// <summary>
        /// The player's current speed multiplier index.
        /// </summary>
        private int _currentSpeedMultiplierIndex = 0;

        /// <summary>
        /// The player's current collision radius index.
        /// </summary>
        private int _currentCollisionRadiusIndex = 0;

        /// <summary>
        /// The potential speed multiplier upgrades.
        /// </summary>
        private readonly List<float> _speedMultipliers = new() { 1f, 1.5f, 2f };
        
        /// <summary>
        /// The potential collision radius upgrades.
        /// </summary>
        private readonly List<float> _collisionRadii = new() { 1.5f, 1.75f, 2f };
        
        /// <summary>
        /// The costs of each speed multiplier upgrade.
        /// </summary>
        private readonly List<int> _speedMultiplierCosts = new() { 0, 100, 200 };
        
        /// <summary>
        /// The costs of each collision radius upgrade.
        /// </summary>
        private readonly List<int> _collisionRadiusCosts = new() { 0, 100, 200 };
        
        /// <summary>
        /// True if the upgrade menu is open, false otherwise.
        /// </summary>
        private bool _isMenuOpen = false;

        /// <summary>
        /// Initializes the components.
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
            
            _canvas = GetComponent<Canvas>();
        }

        /// <summary>
        /// Opens/closes the upgrade menu and updates fields accordingly.
        /// </summary>
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            if (!_isMenuOpen)
            {
                OpenUpgradeMenu();
            }
            else
            {
                CloseUpgradeMenu();
            }
        }
        
        /// <summary>
        /// Sets the upgrades to the player's saved data.
        /// </summary>
        /// <param name="speedMultiplier">The player's saved speed multiplier.</param>
        /// <param name="collisionRadius">The player's saved collision radius.</param>
        public void SetUpgrades(float speedMultiplier, float collisionRadius)
        {
            _currentSpeedMultiplierIndex = _speedMultipliers.IndexOf(speedMultiplier);
            _currentCollisionRadiusIndex = _collisionRadii.IndexOf(collisionRadius);
        }

        /// <summary>
        /// Opens the upgrade menu and sets the buttons to the player's saved data.
        /// </summary>
        private void OpenUpgradeMenu()
        {
            _canvas.enabled = true;
            _isMenuOpen = true;
            plasticPointsText.text = $"x {Player.Instance.GetPlasticPoints()}";
            for (int i = 0; i < speedMultiplierButtons.Count; i++)
            {
                speedMultiplierButtons[i].UpdateButton(i <= _currentSpeedMultiplierIndex,
                    _currentSpeedMultiplierIndex < i && 
                    Player.Instance.GetPlasticPoints() >= _speedMultiplierCosts[i]);
            }
            
            for (int i = 0; i < collisionRadiusButtons.Count; i++)
            {
                collisionRadiusButtons[i].UpdateButton(i <= _currentCollisionRadiusIndex, 
                    _currentCollisionRadiusIndex < i && 
                    Player.Instance.GetPlasticPoints() >= _collisionRadiusCosts[i]);
            }
            
            for (int i = 0; i < speedMultiplierCostTexts.Count; i++)
            {
                speedMultiplierCostTexts[i].text = $"x {_speedMultiplierCosts[i]}";
            }
            
            for (int i = 0; i < collisionRadiusCostTexts.Count; i++)
            {
                collisionRadiusCostTexts[i].text = $"x {_collisionRadiusCosts[i]}";
            }
        }

        /// <summary>
        /// Closes the upgrade menu.
        /// </summary>
        public void CloseUpgradeMenu()
        {
            _canvas.enabled = false;
            _isMenuOpen = false;
        }

        /// <summary>
        /// Called by the upgrade buttons to buy a speed multiplier upgrade.
        /// </summary>
        /// <param name="index">The index of the upgrade.</param>
        public void BuySpeedUpgrade(int index)
        {
            _currentSpeedMultiplierIndex = index;
            speedMultiplierButtons[index].UpdateButton(true, false);
            GameEvent.UpgradeSpeed(_speedMultipliers[index]);
            ProgressionSaver.Instance.UpdatePlayerSpeedMultiplier(_speedMultipliers[index]);
        }
        
        /// <summary>
        /// Called by the upgrade buttons to buy a collision radius upgrade.
        /// </summary>
        /// <param name="index">The index of the upgrade.</param>
        public void BuyCollisionRadiusUpgrade(int index)
        {
            _currentCollisionRadiusIndex = index;
            collisionRadiusButtons[index].UpdateButton(true, false);
            GameEvent.UpgradeCollisionRadius(_collisionRadii[index]);
            ProgressionSaver.Instance.UpdatePlayerCollisionRadius(_collisionRadii[index]);
        }
    }
}