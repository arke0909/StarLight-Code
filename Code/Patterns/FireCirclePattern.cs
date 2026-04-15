using System.Collections;
using Code.Combat;
using Code.Combat.Bullets;
using Code.Patterns.PatternDatas;
using UnityEngine;

namespace Code.Patterns
{
    public class FireArcPattern : Pattern
    {
        private CircleBulletDataSO _data;
        
        public override void UsePattern(PatternSO patternSO)
        {
            base.UsePattern(patternSO);
            
            _data = patternSO as CircleBulletDataSO;
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            DamageData damageData = _damageCompo.CalculateDamage(_attackStat, damageMultiply);
            float angle = 360f / _data.bulletCnt;
            for (int i = 0; i < _data.repeatCount; i++)
            {
                float offset = angle / 2;
                float totalAngle = angle;
                
                if (i % 2 == 1)
                    totalAngle += offset;

                for (int j = 0; j < _data.bulletCnt; ++j)
                {
                    float x = Mathf.Cos(totalAngle * Mathf.Deg2Rad);
                    float y = Mathf.Sin(totalAngle * Mathf.Deg2Rad);

                    Vector3 direction = new Vector3(x, 0, y);

                    Bullet bullet = _poolManager.Pop<Bullet>(bulletItemSO);
                    bullet.InitBullet(direction, firePosTrm[0].position + direction * _data.distance, damageData,
                        _data.size, _data.speedMultiply);

                    totalAngle += angle;
                }

                yield return new WaitForSeconds(_data.delay);
            }
            _patternComponent.isUsing = false;
        }
    }
}