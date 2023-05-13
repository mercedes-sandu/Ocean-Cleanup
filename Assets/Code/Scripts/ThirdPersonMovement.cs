using UnityEngine;

namespace Code.Scripts
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        /// <summary>
        /// The character controller component.
        /// </summary>
        [SerializeField] private CharacterController controller;
        
        /// <summary>
        /// The main camera.
        /// </summary>
        [SerializeField] private Transform cam;
        
        /// <summary>
        /// The player movement speed.
        /// </summary>
        [SerializeField] private float speed = 6f;

        /// <summary>
        /// The turn smooth time.
        /// </summary>
        [SerializeField] private float turnSmoothTime = 0.1f;
        
        /// <summary>
        /// The turn smooth velocity.
        /// </summary>
        private float _turnSmoothVelocity;

        /// <summary>
        /// Moves the player according to input.
        /// </summary>
        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal,0f, vertical).normalized;
            Debug.Log("Direction: " + direction);
            
            if (!(direction.magnitude > -0.1f)) return;
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime)); // rider said to reorder operations
        }
    }
}