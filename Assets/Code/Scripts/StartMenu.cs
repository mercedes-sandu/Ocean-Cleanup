using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts
{
    public class StartMenu : MonoBehaviour
    {
        /// <summary>
        /// The canvas animator.
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// Gets components.
        /// </summary>
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Starts the menu close animation, which will eventually start the game.
        /// </summary>
        public void StartGameAnimation()
        {
            _animator.Play("StartMenuClosed");
        }
    
        /// <summary>
        /// Called by the start animation. Loads the instructions scene.
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("Instructions");
        }

        /// <summary>
        /// Called by the quit button. Closes the application.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}