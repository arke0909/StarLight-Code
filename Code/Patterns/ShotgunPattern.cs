using System.Collections;
using System.Collections.Generic;
using Code.Combat;
using Code.Combat.Bullets;
using Code.Patterns.PatternDatas;
using UnityEngine;

namespace Code.Patterns
{
    public class ShotgunPattern : Pattern
    {
        private Dictionary<Transform, Vector3> _fireDirections = new();
        private BulletDataListSO _data;

        public override void UsePattern(PatternSO patternSO)
        {
            base.UsePattern(patternSO);

            _data = patternSO as BulletDataListSO;
            StartCoroutine(PatternCoroutine());
        }

        private IEnumerator PatternCoroutine()
        {
            DamageData damageData = _damageCompo.CalculateDamage(_attackStat, damageMultiply);

            bool isTargeting = _data.isTargeting;
            float delay = _data.delay;

            foreach (var firePos in firePosTrm)
            {
                Vector3 dir = isTargeting && _data.isConstDir
                    ? (_player.transform.position - firePos.position).normalized
                    : Vector3.back;
                _fireDirections[firePos] = dir;
            }


            for (int i = 0; i < _data.bulletData.Length; i++)
            {
                int bulletCount = _data.bulletData[i].bulletCnt;
                float size = _data.bulletData[i].size;
                float speedMultiply = _data.bulletData[i].speedMultiply;
                if (bulletCount == 1)
                {
                    foreach (var firePos in firePosTrm)
                    {
                        Vector3 direction = _data.isConstDir
                            ? _fireDirections[firePos]
                            : (_player.transform.position - firePos.position).normalized;
                        Bullet bullet = _poolManager.Pop<Bullet>(_data.bulletData[i].poolingItem);
                        bullet.InitBullet(direction, firePos.position, damageData, size, speedMultiply);
                    }

                    yield return new WaitForSeconds(delay);
                }
                else
                {
                    float fireAngle = _data.bulletData[i].angle;
                    float startAngle = -(fireAngle * (bulletCount - 1)) / 2f;

                    for (int l = 0; l < _data.bulletData[i].repeat; l++)
                    {
                        for (int j = 0; j < bulletCount; ++j)
                        {
                            float angle = startAngle + fireAngle * j;

                            foreach (var firePos in firePosTrm)
                            {
                                Vector3 direction = _data.isConstDir
                                    ? _fireDirections[firePos]
                                    : (_player.transform.position - firePos.position).normalized;
                                Vector3 rotatedDir = Quaternion.Euler(0, angle, 0) * direction;

                                Bullet bullet = _poolManager.Pop<Bullet>(_data.bulletData[i].poolingItem);
                                bullet.InitBullet(rotatedDir, firePos.position, damageData,
                                    size, speedMultiply);
                            }
                        }

                        yield return new WaitForSeconds(delay);
                    }
                }
            }

            _patternComponent.isUsing = false;
        }
    }
}