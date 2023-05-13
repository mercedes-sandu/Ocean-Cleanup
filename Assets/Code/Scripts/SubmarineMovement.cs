using UnityEngine;

namespace Code.Scripts
{
    public class SubmarineMovement : MonoBehaviour
    {
        [SerializeField] private float speedChangeAmount;
        [SerializeField] private float riseSpeed;
        [SerializeField] private float turnSpeed;

        [SerializeField] private float maxForwardSpeed;
        [SerializeField] private float maxBackSpeed;

        [SerializeField] private float minSpeed;
        //current speed
        private float _curSpeed;
        private Rigidbody _rb;
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // this movement is a bit (a lot) broken idk why
            if (Input.GetKey(KeyCode.W))
                _curSpeed += speedChangeAmount;
            else if (Input.GetKey(KeyCode.S))
                _curSpeed -= speedChangeAmount;
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
