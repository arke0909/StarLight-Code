using Code.Effects;
using Code.Entities;
using DG.Tweening;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Feedbacks
{
    public class HitEffectFeedback : MonoBehaviour
    {
        [SerializeField] private PoolingItemSO effectItem;
        [SerializeField] private float playDuration = 0.5f;
        [field: SerializeField] public string HitBulletName { get; private set; }

        [Inject] private PoolManagerMono _poolManager;

        private PoolingEffect _effect;
        
        public void CreateFeedback(Vector3 position, Vector3 normal)
        {
            _effect = _poolManager.Pop<PoolingEffect>(effectItem);
            
            Quaternion rotation = Quaternion.LookRotation(normal * -1);
            _effect.PlayVFX(position, rotation, playDuration);    
        }

        public void StopFeedback()
        { }
    }
}