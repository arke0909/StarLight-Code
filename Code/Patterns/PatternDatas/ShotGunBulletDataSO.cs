using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Patterns.PatternDatas
{
    [CreateAssetMenu(fileName = "ShotGunData", menuName = "SO/Pattern/BulletData", order = 0)]
    public class ShotGunBulletDataSO : ScriptableObject
    {
        public PoolingItemSO poolingItem;
        public int repeat = 1;
        public float angle = 35f;
        public float speedMultiply = 1f;
        public float size = 1f;
        public int bulletCnt = 4;
    }
}