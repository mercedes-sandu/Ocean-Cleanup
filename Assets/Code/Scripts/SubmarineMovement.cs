using UnityEngine;

namespace Code.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        /// <summary>
        /// The player's speed. 
        /// </summary>
        [SerializeField] private float speed;
        
        /// <summary>
        /// The player's turn speed.
        /// </summary>
        [SerializeField] private float turnSpeed;
        
        /// <summary>
        /// The maximum speed the player can achieve while moving forward.
        /// </summary>
        [SerializeField] private float maxForwardSpeed;
        
        /// <summary>
        /// The maximum speed the player can achieve while moving backward.
        /// </summary>
        [SerializeField] private float maxBackSpeed;
        
        /// <summary>
        /// The player's minimum speed.
        /// </summary>
        [SerializeField] private float minSpeed;
        
        /// <summary>
        /// The player's current speed.
        /// </summary>
        private float _curSpeed;
        
        /// <summary>
        /// The player's rigidbody component.
        /// </summary>
        private Rigidbody _rb;

        /// <summary>
        /// Initializes components.
        /// </summary>
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Moves the player accordingly.
        /// </summary>
        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W))
                _curSpeed += speed;
            else if (Input.GetKey(KeyCode.S))
                _curSpeed -= speed;
            else if (Mathf.Abs(_curSpeed) <= minSpeed)
                _curSpeed = 0;

            _curSpeed = Mathf.Clamp(_curSpeed, -maxBackSpeed, maxForwardSpeed);
            
            _rb.AddForce(transform.forward * _curSpeed);

            if (Input.GetKey(KeyCode.D))
                _rb.AddTorque(transform.up * turnSpeed);
            else if (Input.GetKey(KeyCode.A))
                _rb.AddTorque(transform.up * -turnSpeed);
        
            if (Input.GetKey(KeyCode.LeftShift))
                _rb.AddTorque(transform.right * turnSpeed);
            else if (Input.GetKey(KeyCode.LeftControl))
                _rb.AddTorque(transform.right * -turnSpeed);
        }
    }
}