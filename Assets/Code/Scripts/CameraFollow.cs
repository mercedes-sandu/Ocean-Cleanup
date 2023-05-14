using UnityEngine;

namespace Code.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The camera's target which it follows. This will be the player.
        /// </summary>
        [SerializeField] private Transform target;
        
        /// <summary>
        /// The offset of the position of the camera behind the target.
        /// </summary>
        [SerializeField] private Vector3 offset;
        
        /// <summary>
        /// The smoothing speed of the camera's movement.
        /// </summary>
        [SerializeField] private float smoothSpeed = 0.125f;
        
        /// <summary>
        /// The camera's rotation speed.
        /// </summary>
        [SerializeField] private float rotateSpeed = 5.0f;

        /// <summary>
        /// The camera's yaw (y-angle).
        /// </summary>
        private float _yaw = 0.0f;
        
        /// <summary>
        /// The camera's pitch (x-angle).
        /// </summary>
        private float _pitch = 0.0f;

        /// <summary>
        /// Initializes the camera's yaw and pitch. Also disables the cursor. Finds the player target if it has not 
        /// been initialized.
        /// </summary>
        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            _yaw = angles.y;
            _pitch = angles.x;
            
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;

            if (!target) target = GameObject.FindWithTag("Player").transform;
        }

        /// <summary>
        /// Update the camera's position and rotation accordingly.
        /// </summary>
        void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // rotate the camera around the player based on mouse input
            _yaw += mouseX * rotateSpeed;
            _pitch -= mouseY * rotateSpeed;
            _pitch = Mathf.Clamp(_pitch, -45f, 45f);

            // update the camera position and rotation based on the player's position and rotation
            Vector3 rotation = new Vector3(_pitch, _yaw, 0f);
            Vector3 desiredPosition = target.position + offset;
            Quaternion rotationQuaternion = Quaternion.Euler(rotation);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.rotation = rotationQuaternion;
        }
    }
}