using Code.Patterns.PatternDatas;
using UnityEngine;

namespace Code.Patterns
{
    public class SellBulletPattern : Pattern
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float delay;

        public override void UsePattern(PatternSO patternSO)
        {
            base.UsePattern(patternSO);
        }
    }
}