using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class InGameUI : MonoBehaviour
    {
        /// <summary>
        /// The text displaying the number of plastics the player has collected.
        /// </summary>
        [SerializeField] private TextMeshProUGUI numPlasticsText;
        
        /// <summary>
        /// The text displaying the number of plastic points the player has collected.
        /// </summary>
        [SerializeField] private TextMeshProUGUI plasticPointsText;
        
        /// <summary>
        /// The canvas component of the pause menu.
        /// </summary>
        [SerializeField] private Canvas pauseMenuCanvas;
        
        /// <summary>
        /// The pause button game object.
        /// </summary>
        [SerializeField] private GameObject pauseButton;
        
        /// <summary>
        /// The pause button sprite renderer.
        /// </summary>
        private Image _pauseButtonImage;
        
        /// <summary>
        /// The pause button button component.
        /// </summary>
        private Button _pauseButton;
        
        /// <summary>
        /// True if the game is paused, false otherwise.
        /// </summary>
        private bool _isPaused = false;

        /// <summary>
        /// The initial number of plastics the player has collected.
        /// </summary>
        private int _numPlastics;

        /// <summary>
        /// The initial number of plastic points the player has collected.
        /// </summary>
        private int _plasticPoints;

        /// <summary>
        /// Gets and initializes components, subscribes to game events.
        /// </summary>
        private void Awake()
        {
            _pauseButtonImage = pauseButton.GetComponent<Image>();
            _pauseButton = pauseButton.GetComponent<Button>();
            pauseMenuCanvas.enabled = false;
            
            GameEvent.OnPlasticCollect += PlasticCollected;
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void Start()
        {
            _numPlastics = ProgressionSaver.Instance.playerPlasticsCollected;
            _plasticPoints = ProgressionSaver.Instance.playerPlasticPoints;
            numPlasticsText.text = $"x {_numPlastics}";
            plasticPointsText.text = $"x {_plasticPoints}";
        }
        
        /// <summary>
        /// Listens for the escape key to pause/unpause the game.
        /// </summary>
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }

        /// <summary>
        /// Updates UI whenever plastic is collected by the player.
        /// </summary>
        /// <param name="plasticPoints">The number of plastic points gained by the player.</param>
        private void PlasticCollected(int plasticPoints)
        {
            _numPlastics++;
            _plasticPoints += plasticPoints;
            numPlasticsText.text = $"x {_numPlastics}";
            plasticPointsText.text = $"x {_plasticPoints}";
        }

        /// <summary>
        /// Called by update and the pause button. Pauses the game.
        /// </summary>
        public void PauseButton()
        {
            _pauseButtonImage.enabled = false;
            _pauseButton.interactable = false;
            pauseMenuCanvas.enabled = true;
            Time.timeScale = 0;
            _isPaused = true;
        }

        /// <summary>
        /// Called by update and the resume button. Unpauses the game.
        /// </summary>
        public void ResumeButton()
        {
            pauseMenuCanvas.enabled = false;
            _pauseButtonImage.enabled = true;
            _pauseButton.interactable = true;
            Time.timeScale = 1;
            _isPaused = false;
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDestroy()
        {
            GameEvent.OnPlasticCollect -= PlasticCollected;
        }
    }
}