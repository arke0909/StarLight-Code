using UnityEngine;

namespace Code.Combat
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField, Range(0.1f, 3f)] private float castRadius = 1f;
        [SerializeField, Range(0, 3f)] private float castInterpolation = 0.5f;
        [SerializeField, Range(0, 30f)] private float castingRange = 1f;
        
        public override void CastDamage(DamageData damageData, Vector3 position, Vector3 direction, string bulletName)
        {
            Vector3 startPos = position + direction * -castInterpolation * 2; //- 붙어있음.
            
            bool isHit = Physics.SphereCast(
                startPos, castRadius, 
                transform.forward, 
                out RaycastHit hit, 
                castingRange,
                whatIsEnemy);

            if (isHit)
            {
                if(hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damageData, hit.point, hit.normal, bulletName);
                }
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 startPos = transform.position + transform.forward * -castInterpolation * 2;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPos, castRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startPos + transform.forward*castingRange, castRadius);
            
        }
#endif
    }
}