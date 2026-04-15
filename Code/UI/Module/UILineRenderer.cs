using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Module
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRenderer : MaskableGraphic
    {
        [SerializeField] private float thickness = 1f;
        [SerializeField] private bool center = true;
        public Color lineColor;
        [field: SerializeField] public Vector2[] Points { get; set; } = new Vector2[1];

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            // UI 버텍스를 만들거 uv를 지정할 수 있는 함수
            vh.Clear();

            if (Points.Length < 2) return;

            for (int i = 0; i < Points.Length - 1; i++)
            {
                CreateLineSegment(Points[i], Points[i + 1], vh);
                int index = i * 5;

                vh.AddTriangle(index, index + 1, index + 3);
                vh.AddTriangle(index + 3, index + 2, index);

                if (i != 0)
                {
                    vh.AddTriangle(index, index - 1, index - 3);
                    vh.AddTriangle(index + 1, index - 1, index - 2);
                }
            }
        }

        private void CreateLineSegment(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta * 0.5f) : Vector2.zero;
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = lineColor;

            Quaternion point1Rot = Quaternion.Euler(0f, 0f, RotateToPointToward(point1, point2) + 90f);
            vertex.position = point1Rot * new Vector3(-thickness * 0.5f, 0f);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);

            vertex.position = point1Rot * new Vector3(thickness * 0.5f, 0f);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);

            Quaternion point2Rot = Quaternion.Euler(0, 0, RotateToPointToward(point2, point1) - 90f);
            vertex.position = point2Rot * new Vector3(-thickness * 0.5f, 0f);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);

            vertex.position = point2Rot * new Vector3(thickness * 0.5f, 0f);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);

            vertex.position = point2 - offset;
            vh.AddVert(vertex);
        }

        private float RotateToPointToward(Vector2 vertex, Vector2 target)
            => Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * Mathf.Rad2Deg;
    }
}