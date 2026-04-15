using UnityEngine;

namespace Code.Patterns.PatternDatas 
{
    [CreateAssetMenu(fileName = "BulletDataList", menuName = "SO/Pattern/DataList", order = 0)]
    public class BulletDataListSO : PatternSO
    {
        public float delay = 0.2f;
        public bool isTargeting = false;
        public bool isConstDir = false;
        public ShotGunBulletDataSO[] bulletData;
    }
}