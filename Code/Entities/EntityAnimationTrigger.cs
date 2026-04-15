using System;
using UnityEngine;

namespace Code.Entities
{
    public class EntityAnimationTrigger : MonoBehaviour, IEntityComponent
    {
        public event Action OnAnimationEnd;
        public event Action OnDeadTrigger;
        
        private Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void AnimationEnd() => OnAnimationEnd?.Invoke();
        private void DeadTrigger() => OnDeadTrigger?.Invoke();
    }
}