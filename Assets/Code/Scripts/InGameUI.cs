using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numPlasticsText;
        [SerializeField] private TextMeshProUGUI plasticPointsText;
        [SerializeField] private Canvas pauseMenuCanvas;
        [SerializeField] private GameObject pauseButton;
        
        private SpriteRenderer _pauseButtonSpriteRenderer;
        private Button _pauseButton;
        private bool _isPaused = false;

        private void Awake()
        {
            _pauseButtonSpriteRenderer = pauseButton.GetComponent<SpriteRenderer>();
            _pauseButton = pauseButton.GetComponent<Button>();
            pauseMenuCanvas.enabled = false;
            numPlasticsText.text = $"x {ProgressionSaver.Instance.playerPlasticsCollected}";
            plasticPointsText.text = $"x {ProgressionSaver.Instance.playerPlasticPoints}";
        }

        private void Update()
        {
            
        }
        
        public void PauseButton()
        {
            
        }

        public void ResumeButton()
        {
            
        }
    }
}