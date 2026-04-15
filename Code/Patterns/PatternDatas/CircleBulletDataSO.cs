using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Patterns.PatternDatas
{
    [CreateAssetMenu(fileName = "CircleBullet", menuName = "SO/Pattern/CircleBulletData", order = 0)]
    public class CircleBulletDataSO : PatternSO
    {
        public PoolingItemSO poolingItem;
        public int bulletCnt = 12;
        public int repeatCount = 4;
        public float delay = 0.8f;
        public float speedMultiply = 1f;
        public float size = 1f;
        public float distance = 3f;
    }
}