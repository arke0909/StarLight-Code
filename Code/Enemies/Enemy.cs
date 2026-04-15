using Code.Combat;
using Code.Core;
using Code.Core.Events;
using Code.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemies
{
    public class Enemy : Entity
    {
        private readonly int _deadHash = Animator.StringToHash("Dead");

        public UnityEvent OnDeadTrigger;
        
        protected override void Awake()
        {
            base.Awake();
            gameEventChannel.AddListener<PlayerDeadEvent>(HandlePlayerDead);
            GetCompo<EntityAnimationTrigger>().OnDeadTrigger += HandleDeadTrigger;
        }

        protected override void OnDestroy()
        {
            gameEventChannel.RemoveListener<PlayerDeadEvent>(HandlePlayerDead);
            GetCompo<EntityAnimationTrigger>().OnDeadTrigger -= HandleDeadTrigger;
            base.OnDestroy();
        }

        private void HandlePlayerDead(PlayerDeadEvent evt)
        {
            gameObject.layer = DodgeLayer;
            OnActiveEvent?.Invoke(false);
        }

        protected override void HandleHit()
        {
        }

        protected override void HandleDead()
        {
            base.HandleDead();
            EnemyDeadEvent evt = GameEvents.EnemyDeadEvent;
            gameEventChannel.RaiseEvent(evt);
            GetCompo<EntityRenderer>().SetParam(_deadHash, true);
            GetCompo<EntityVFX>().PlayVfx("Explosion", transform.position, Quaternion.identity);
            OnActiveEvent?.Invoke(false);
        }

        private void HandleDeadTrigger()
        => OnDeadTrigger?.Invoke();
    }
}