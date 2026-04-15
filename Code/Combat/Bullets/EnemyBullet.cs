using UnityEngine;
using DG.Tweening;

namespace Code.Combat.Bullets
{
    public class EnemyBullet : Bullet
    {
        [SerializeField] private bool isDeceleration = false;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float deceleratedSpeed = 4f;

        public override void InitBullet(Vector3 direction, Vector3 position, DamageData damageData, float size = 1f,
            float speedMultipy = 1f)
        {
            base.InitBullet(direction, position, damageData, size);

            if (isDeceleration)
            {
                DOTween.To(() => moveSpeed, x => moveSpeed = x, deceleratedSpeed, duration);
            }
        }

        private void FixedUpdate()
        {
            if(isDeceleration)
                _rbCompo.linearVelocity *= moveSpeed;
        }
    }
}