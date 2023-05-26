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
        /// The speed at which the sub returns back to a normal rotation.
        /// </summary>
        [SerializeField] private float stabilizationSmoothing;
        
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
        /// The player's animator component.
        /// </summary>
        private Animator _anim;

        /// <summary>
        /// The cached "Speed" property of the animator. If greater than 0.1, the flippers start rotating.
        /// </summary>
        private static readonly int Speed = Animator.StringToHash("Speed");

        /// <summary>
        /// The cached "AnimSpeed" property of the animator. Controls the speed of the flippers' rotation.
        /// </summary>
        private static readonly int AnimSpeed = Animator.StringToHash("AnimSpeed");

        /// <summary>
        /// Subscribes to game events.
        /// </summary>
        private void Awake()
        {
            GameEvent.OnSpeedUpgrade += UpdateSpeed;
        }
        
        /// <summary>
        /// Initializes components.
        /// </summary>
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
            _anim.SetFloat(Speed, 0.0f);
            _anim.SetFloat(AnimSpeed, 0.0f);
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
            _anim.SetFloat(Speed, _curSpeed);
            _anim.SetFloat(AnimSpeed, _curSpeed / maxForwardSpeed);

            _rb.AddForce(transform.forward * _curSpeed);

            if (Input.GetKey(KeyCode.D))
                _rb.AddTorque(transform.up * turnSpeed);
            else if (Input.GetKey(KeyCode.A))
                _rb.AddTorque(transform.up * -turnSpeed);
        
            if (Input.GetKey(KeyCode.LeftShift))
                _rb.AddTorque(transform.right * turnSpeed);
            else if (Input.GetKey(KeyCode.LeftControl))
                _rb.AddTorque(transform.right * -turnSpeed);
            
            // slowly stabilize rotation
            var rotation = _rb.rotation;
            // _rb.MoveRotation(Quaternion.Slerp(rotation, Quaternion.Euler(new Vector3(0, 0, rotation.eulerAngles.z)), stabilizationSmoothing));
        }
        
        /// <summary>
        /// Updates the speed with the new multiplier.
        /// </summary>
        /// <param name="multiplier">The speed multiplier.</param>
        public void UpdateSpeed(float multiplier)
        {
            speed *= multiplier;
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDestroy()
        {
            GameEvent.OnSpeedUpgrade -= UpdateSpeed;
        }
    }
}