using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.Combat.Bullets
{
    public class PlayerBullet : Bullet
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float energy = 0.2f;

        public override void InitBullet(Vector3 direction, Vector3 position, DamageData damageData, float size = 1, float speedMultiply = 1)
        {
            base.InitBullet(direction, position, damageData, size, speedMultiply);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            GetEnergyEvent evt = GameEvents.GetEnergyEvent;
            evt.energy = energy;
            playerChannel.RaiseEvent(evt);
        }
    }
}