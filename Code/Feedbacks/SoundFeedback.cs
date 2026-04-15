using Code.Sounds;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Feedbacks
{
    public class SoundFeedback : Feedback
    {
        [SerializeField] private PoolingItemSO soundPlayerItem;
        [SerializeField] private SoundSO soundSO;
        [field: SerializeField] public string HitBulletName { get; private set; }

        [Inject] private PoolManagerMono _poolManager;
        
        public override void CreateFeedback()
        {
            SoundPlayer player = _poolManager.Pop<SoundPlayer>(soundPlayerItem);
            player.PlaySound(soundSO);
        }

        public override void StopFeedback()
        {
        }
    }
}