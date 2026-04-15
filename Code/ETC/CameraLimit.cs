using UnityEngine;
using UnityEngine.Serialization;

namespace Code.ETC
{
    public class CameraLimit : MonoBehaviour
    {
        [SerializeField] private float offsetX = 0.5f;
        [SerializeField] private float offsetZ = 0.5f;
        private Camera _mainCam;

        private float _leftX;
        private float _rightX;
        private float _leftZ;
        private float _rightZ;
        
        private void Awake()
        {
            _mainCam = Camera.main;

            _leftX = _mainCam.ViewportToWorldPoint(new Vector3(0, 0, 40)).x + offsetX;
            _rightX = _mainCam.ViewportToWorldPoint(new Vector3(1, 1,40)).x - offsetX;
            _leftZ = _mainCam.ViewportToWorldPoint(new Vector3(0, 0,40)).z + offsetZ;
            _rightZ = _mainCam.ViewportToWorldPoint(new Vector3(1, 0.6f,40)).z - offsetZ;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _leftX, _rightX),
                transform.position.y,Mathf.Clamp(transform.position.z, _leftZ, _rightZ));
        }
    }
}