using System;
using System.Collections.Generic;
using System.Linq;
using Code.Combat;
using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Entities
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        public delegate void OnDamageHandler(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, string bulletName);
        public event OnDamageHandler OnDamage;
        [SerializeField] protected GameEventChannelSO gameEventChannel;

        private int _dodgeLayer;
        public int DodgeLayer => _dodgeLayer;
        
        public UnityEvent OnHit;
        public UnityEvent OnDead;
        public UnityEvent OnHeal;
        
        public bool IsDead { get; set; } //사망처리 체크를 위한 거 해놓고
        public int DeadBodyLayer { get; private set; }
        [HideInInspector] public UnityEvent<bool> OnActiveEvent;

        protected Dictionary<Type, IEntityComponent> _components;

        protected virtual void Awake()
        {
            _dodgeLayer = LayerMask.NameToLayer("Dodge");
            gameEventChannel.AddListener<ActiveEvent>(HandleActiveEvent);
            
            DeadBodyLayer = LayerMask.NameToLayer("DeadBody"); //레이어 값 셋팅
            
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        protected virtual void AddComponentToDictionary()
        {
            GetComponentsInChildren<IEntityComponent>(true).ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
        }
        
        protected virtual void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInit());
            OnHit.AddListener(HandleHit);
            OnDead.AddListener(HandleDead);
        }

        protected virtual void OnDestroy()
        {
            gameEventChannel.RemoveListener<ActiveEvent>(HandleActiveEvent);
            OnHit.RemoveListener(HandleHit);
            OnDead.RemoveListener(HandleDead);
        }

        protected virtual void HandleActiveEvent(ActiveEvent evt)
        {
            OnActiveEvent?.Invoke(evt.isActive);
        }

        protected abstract void HandleHit();

        protected virtual void HandleDead()
        {
            IsDead = true;
            gameObject.layer = DeadBodyLayer;
            OnActiveEvent?.Invoke(false);
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent component))
                return (T)component;
            
            if(isDerived == false) return default(T);
            
            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if(findType != null) 
                return (T)_components[findType];
            
            return default(T);
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, string bulletName)
            => OnDamage?.Invoke(damageData, hitPoint, hitNormal, bulletName);
        
    }
}