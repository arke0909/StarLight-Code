using UnityEngine;

namespace Code.Enemies
{
    public class WayPoints : MonoBehaviour
    {
        private WayPoint[] _wayPoints;

        public int Length => _wayPoints.Length;
        
        public Transform this[int index] => _wayPoints[index].transform;
        
        private void Awake()
        {
            _wayPoints = GetComponentsInChildren<WayPoint>();
            transform.SetParent(null);
        }
    }
}