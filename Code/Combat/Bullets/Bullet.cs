using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Combat.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float moveSpeed = 8f;
        [SerializeField] protected float lifeTime = 5f;
        [SerializeField] protected ParticleSystem particle;
        [SerializeField] protected string bulletName;
        protected Rigidbody _rbCompo;
        protected DamageCaster _damageCaster;
        protected DamageData _damageData;
        protected Vector3 _originSize;
        protected float _currentLifeTime = 0;
        protected float _originSpeed;

        private Pool _pool;
        [field: SerializeField] public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public virtual void ResetItem()
        {
            moveSpeed = _originSpeed;
            _currentLifeTime = 0;
        }

        protected virtual void Awake()
        {
            _originSpeed = moveSpeed;
            _rbCompo = GetComponent<Rigidbody>();
            _damageCaster = GetComponentInChildren<DamageCaster>();
            _originSize = transform.localScale;
        }

        public virtual void InitBullet(Vector3 direction, Vector3 position, DamageData damageData
            , float size = 1f,float speedMultiply = 1f)
        {
            _damageData = damageData;
            moveSpeed *= speedMultiply;
            transform.localScale = _originSize * size;
            transform.position = position;
            transform.forward = direction.normalized;
            _rbCompo.linearVelocity = transform.forward * moveSpeed;
            if (particle != null)
            {
                var main = particle.main;
                main.startLifetime = lifeTime + 0.3f;                
                particle.Play();
            }
        }

        protected virtual void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if (lifeTime <= _currentLifeTime)
                _pool.Push(this);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            _damageCaster.CastDamage(_damageData, transform.position, transform.forward, bulletName);
            _pool.Push(this);
        }
    }
}