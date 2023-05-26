using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        /// The canvas component of the menu that loads the next scene on level complete.
        /// </summary>
        [SerializeField] private Canvas nextSceneCanvas;
        
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
        /// The total number of plastics the player can collect in this area.
        /// </summary>
        private int _totalPlastics;

        /// <summary>
        /// The name of the next scene, which is set on level complete.
        /// </summary>
        private string _nextSceneName;

        /// <summary>
        /// Gets and initializes components, subscribes to game events.
        /// </summary>
        private void Awake()
        {
            _pauseButtonImage = pauseButton.GetComponent<Image>();
            _pauseButton = pauseButton.GetComponent<Button>();
            pauseMenuCanvas.enabled = false;
            nextSceneCanvas.enabled = false;
            
            GameEvent.OnPlasticCollect += PlasticCollected;
            GameEvent.OnLevelComplete += LevelComplete;
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void Start()
        {
            _numPlastics = 0;
            _plasticPoints = ProgressionSaver.Instance.playerPlasticPoints;
            _totalPlastics = FindObjectsOfType<Plastic>().Length;
            numPlasticsText.text = $"x {_numPlastics} / {_totalPlastics}";
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
            numPlasticsText.text = $"x {_numPlastics} / {_totalPlastics}";
            plasticPointsText.text = $"x {_plasticPoints}";
        }

        /// <summary>
        /// Called by update and the pause button. Pauses the game.
        /// </summary>
        public void PauseButton()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuCanvas.enabled = false;
            _pauseButtonImage.enabled = true;
            _pauseButton.interactable = true;
            Time.timeScale = 1;
            _isPaused = false;
        }
        
        /// <summary>
        /// Enables the next scene canvas and sets the next scene name.
        /// </summary>
        /// <param name="nextLevel">The name of the next scene.</param>
        private void LevelComplete(string nextLevel)
        {
            _nextSceneName = nextLevel;
            nextSceneCanvas.enabled = true;
            Time.timeScale = 0;
        }
        
        /// <summary>
        /// Called by the next level button, loads the next level.
        /// </summary>
        public void NextLevelButton()
        {
            ProgressionSaver.Instance.playerPlasticPoints = _plasticPoints;
            Time.timeScale = 1;
            SceneManager.LoadScene(_nextSceneName);
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDestroy()
        {
            GameEvent.OnPlasticCollect -= PlasticCollected;
            GameEvent.OnLevelComplete -= LevelComplete;
        }
    }
}