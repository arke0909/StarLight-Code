using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Sounds
{
    [Provide]
    public class SoundManager : MonoBehaviour, IDependencyProvider
    {
        [Inject] private PoolManagerMono _poolManager;
        [SerializeField] private PoolingItemSO playerSo;

        public SoundPlayer PlaySound(SoundSO sound,Vector3 pos = default)
        {
            SoundPlayer player = _poolManager.Pop<SoundPlayer>(playerSo);

            player.transform.position = pos;
            player.PlaySound(sound);

            return player;
        }
    }
}