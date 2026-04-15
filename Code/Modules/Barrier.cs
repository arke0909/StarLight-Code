using Code.Combat;
using Code.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Modules
{
    public class Barrier : MonoBehaviour, IDamageable
    {
        [SerializeField] private ActionData actionData;

        public UnityEvent OnHit;

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, string bulletName)
        {
            actionData.HitPoint = hitPoint;
            actionData.HitNormal = hitNormal;
            actionData.BulletName = bulletName;
            
            OnHit?.Invoke();
        }
    }
}