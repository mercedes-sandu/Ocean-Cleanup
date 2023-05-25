using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts
{
    public class Instructions : MonoBehaviour
    {
        /// <summary>
        /// The first scene to load.
        /// </summary>
        [SerializeField] private string firstScene = "Area 1";
        
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
        public void ContinueGameAnimation()
        {
            _animator.Play("InstructionsClosed");
        }
    
        /// <summary>
        /// Called by the continue animation. Loads the first area scene.
        /// </summary>
        public void ContinueGame()
        {
            SceneManager.LoadScene(firstScene);
        }
    }
}