using UnityEngine;

namespace Code.Patterns.PatternDatas
{
    [CreateAssetMenu(fileName = "RotateBulletData", menuName = "SO/Pattern/RotateBulletData", order = 0)]
    public class RotateBulletDataSO : PatternSO
    {
        public bool isReverse;
        public float fireAngle = 45f;
        public float delay = 0.1f;
        public int repeatCnt = 3;
        public int bulletCount = 8;
        public float speedMultiply = 1f;
        public float size = 1f;
    }
}