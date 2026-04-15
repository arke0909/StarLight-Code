using UnityEngine;

namespace Code.Entities
{
    public class ActionData : MonoBehaviour
    {
        public Vector3 HitPoint { get; set; }
        public Vector3 HitNormal { get; set; }
        public string BulletName { get; set; }
    }
}