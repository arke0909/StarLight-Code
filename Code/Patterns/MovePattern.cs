using System.Collections;
using Code.Core.StatSystem;
using Code.Enemies;
using Code.Patterns.PatternDatas;
using UnityEngine;

namespace Code.Patterns
{
    public class MovePattern : Pattern
    {
        [SerializeField] private WayPoints wayPoints;
        private MoveDataSO _moveData;

        private float _speed;

        public override void UsePattern(PatternSO patternSO)
        {
            _moveData = patternSO as MoveDataSO;
            _speed = _moveData.speed;
            StartCoroutine(MoveCoroutine(wayPoints[_moveData.moveIdx].position));
        }


        private IEnumerator MoveCoroutine(Vector3 position)
        {
            Vector3 startPos = transform.position;
            float distance = Vector3.Distance(transform.position, position);
            float totalTime = distance / _speed;
            float currentTime = 0f;
            float percent = 0;

            while (percent < 1)
            {
                currentTime += Time.deltaTime;
                percent = currentTime / totalTime;
                _enemy.transform.position = Vector3.Lerp(startPos, position, percent);
                yield return null;
            }
            
            _patternComponent.isUsing = false;
        }
    }
}