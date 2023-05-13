using UnityEngine;

namespace Code.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        // target should be the player
        [SerializeField] private Transform target;
        // for these two, higher = faster
        [SerializeField] private float movSmoothing;
        [SerializeField] private float rotSmoothing;
    
        // Start is called before the first frame update
        void Start()
        {
            var transform1 = transform;
            transform1.position = target.position;
            transform1.rotation = target.rotation;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // cache for efficiency (idk rider told me to do this)
            Transform transform1;
            // linear interpolation to create smooth movement/rotation
            (transform1 = transform).position = Vector3.Lerp(transform1.position, target.position, movSmoothing);
            transform.rotation = Quaternion.Slerp(transform1.rotation, target.rotation, rotSmoothing);
        }
    }
}
