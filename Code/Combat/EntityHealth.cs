using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] protected GameEventChannelSO uiChannel;
        [SerializeField] protected StatSO hpStat;
        [SerializeField] protected ActionData actionData;
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float currentHealth;
        [SerializeField] protected float invincibilityDuration = 0.1f;
        [SerializeField] protected bool isInvincibility = false;
        [SerializeField] protected uint id;
        
        protected Entity _entity;
        protected EntityStat _statCompo;
        protected float _lastHitTime;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
            _entity.OnDamage += ApplyDamage;
            _lastHitTime = Time.time;
        }

        public virtual void AfterInit()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            target.OnValueChange += HandleMaxHPChange;
            currentHealth = maxHealth = target.Value;
        }

        protected void OnDestroy()
        {
            _entity.OnDamage -= ApplyDamage;
            StatSO target = _statCompo.GetStat(hpStat);
            target.OnValueChange -= HandleMaxHPChange;
            
        }

        protected void HandleMaxHPChange(StatSO stat, float currentValue, float previousValue)
        {
            float changed = currentValue - previousValue;
            maxHealth = currentValue;
            currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, string bulletName)
        {
            if(_entity.IsDead) return;

            actionData.HitNormal = hitNormal;
            actionData.HitPoint = hitPoint;
            actionData.BulletName = bulletName;
            _entity.OnHit?.Invoke();
            
            if (isInvincibility)
            {
                if(Time.time - _lastHitTime < invincibilityDuration)
                    return;
            }

            DecreaseCurrentHp(damageData.damage);

            if (currentHealth <= 0)
            {
                _entity.OnDead?.Invoke();
            }

            _lastHitTime = Time.time;
        }

        public void DecreaseCurrentHp(float damage, bool isSelfHarm = false)
        {
            if(isSelfHarm)
                currentHealth = Mathf.Max(currentHealth - damage, 1);
            else
                currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            RaiseHpChangeEvent();
        }

        protected void RaiseHpChangeEvent()
        {
            HpChangeEvent evt = GameEvents.HpChangeEvent;
            evt.id = id;
            evt.maxHp = maxHealth;
            evt.currentHp = currentHealth;
            uiChannel.RaiseEvent(evt);
        }

        public void ApplyHeal(float heal)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
            _entity.OnHeal?.Invoke();
            RaiseHpChangeEvent();
        }
    }
}