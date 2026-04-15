using System.Collections;
using Code.Combat;
using Code.Combat.Bullets;
using Code.Patterns.PatternDatas;
using UnityEngine;

namespace Code.Patterns
{
    public class BarragePattern : Pattern
    {
        private RotateBulletDataSO _data;
        
        public override void UsePattern(PatternSO patternSO)
        {
            base.UsePattern(patternSO);
            _data = patternSO as RotateBulletDataSO;
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            DamageData damageData = _damageCompo.CalculateDamage(_attackStat, damageMultiply);
            float angle = _data.fireAngle / _data.bulletCount;
            angle *= _data.isReverse ? -1 : 1;
            for (int i = 0; i < _data.repeatCnt; i++)
            {
                bool isEven = i % 2 == 0;
                float totalAngle = _data.isReverse ? isEven ? _data.fireAngle / 2 : -(_data.fireAngle / 2) 
                    : isEven ? -(_data.fireAngle / 2) : _data.fireAngle / 2;
                
                for (int j = 0; j < _data.bulletCount; j++)
                {
                    foreach (var firePos in firePosTrm)
                    {
                        Vector3 direction = Quaternion.Euler(0, totalAngle, 0) * Vector3.back;
                        Bullet bullet = _poolManager.Pop<Bullet>(bulletItemSO);
                        bullet.InitBullet(direction, firePos.position, damageData,_data.size,_data.speedMultiply);
                    }
                    totalAngle += angle;

                    yield return new WaitForSeconds(_data.delay);
                }
                angle *= -1;
            }

            _patternComponent.isUsing = false;
        }
    }
}