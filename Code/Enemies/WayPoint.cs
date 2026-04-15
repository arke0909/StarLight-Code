using UnityEngine;

namespace Code.Enemies
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private Color debugColor;
        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}