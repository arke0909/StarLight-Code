using UnityEngine;

namespace Code.Combat.Bullets
{
    public class BarrageBullet : PlayerBullet
    {
        [SerializeField] private TrailRenderer[] trails;
        public override void InitBullet(Vector3 direction, Vector3 position, DamageData damageData, float size = 1, float speedMultiply = 1)
        {
            base.InitBullet(direction, position, damageData, size, speedMultiply);
            
            foreach (var trail in trails)
            {
                trail.Clear();
            }
        }
    }
}