using System;
using System.Linq;
using UnityEngine;

namespace Code.UI.Module
{
    public class ModulePartLineRenderer : MonoBehaviour
    {
        [SerializeField] private UILineRenderer lineRenderer;
        [SerializeField] private RectTransform[] pointRectTrm;

        public RectTransform RectTrm => transform as RectTransform;

        [ContextMenu("Set Line Points")]
        private void SetLinePoints()
        {
            Camera cam = Camera.main;
            
            Vector2[] points = pointRectTrm.Select(rectTrm => 
                (Vector2)RectTrm.InverseTransformPoint(rectTrm.position)).ToArray();

            foreach (var pos in points)
            {
                Debug.Log(pos);
            }
            lineRenderer.Points = points;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (var pos in lineRenderer.Points)
            {
                Gizmos.DrawWireSphere(pos, 5f);
            }
        }
#endif
    }
}