using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Enemies.Test
{
    public class MathLaser : MonoBehaviour
    {
        [SerializeField] private GameObject laser;
        [SerializeField] private int cnt = 2;
        [SerializeField] private float zValue = 6f;
        [SerializeField] private float amplitude = 5f;
        [SerializeField] private float duration;

        public Vector3 targetPos;

        private int _abs = 1;
        private float _currentTime = 0;
        private float _easingValue;
        private bool _isUsing = false;

        private void Awake()
        {
            targetPos.z = zValue;
        }

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && !_isUsing)
            {
                _isUsing = true;
                _abs = 1;
            }

            if (_isUsing)
            {
                _currentTime = _currentTime >= duration ? duration : _currentTime + Time.deltaTime;
                CalcEase(_currentTime);
                CalcCircle(_easingValue * duration * cnt);
                if (_currentTime >= duration)
                {
                    _isUsing = false;
                    _currentTime = 0;
                }
            }
        }

        // 원의 위치 구하기
        private void CalcCircle(float value)
        {
            float x = amplitude * Mathf.Cos((2 * Mathf.PI) / duration * value);
            float y = amplitude * Mathf.Sin((2 * Mathf.PI) / duration * value);

            targetPos.x = x * _abs;
            targetPos.y = y * _abs;

            laser.transform.LookAt(transform.localToWorldMatrix * targetPos);
        }

        // 이징 값 구하기
        private void CalcEase(float currentTime)
        {
            // 비율 구하기
            float ratio = Mathf.Clamp01(currentTime / duration);

            _easingValue = ratio == 0 ? 0 : Mathf.Pow(2, 10 * (ratio - 1));
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPos, 0.5f);
        }

#endif
    }
}