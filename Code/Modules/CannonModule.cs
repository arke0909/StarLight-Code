using Code.Combat;
using Code.Combat.Bullets;
using Code.Core.StatSystem;
using Code.Players;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Modules
{
    public class CannonModule : Module
    {
        [FormerlySerializedAs("knockOutBulletItemSO")]
        [SerializeField] private PoolingItemSO CannonBulletItemSO;
        [SerializeField] private float damageMultiplier = 10f;
        [SerializeField] private StatSO attackStat;
        [Inject] private PoolManagerMono _poolManager;
        private DamageCompo _damageCompo;
        private PlayerAttackCompo _playerAttackCompo;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            _damageCompo = player.GetCompo<DamageCompo>();
            _playerAttackCompo = player.GetCompo<PlayerAttackCompo>();
        }

        public override void UseModule()
        {
            DamageData data = _damageCompo.CalculateDamage(attackStat, damageMultiplier);
            Bullet cannonBullet = _poolManager.Pop<Bullet>(CannonBulletItemSO);
            cannonBullet.InitBullet(_playerAttackCompo.FirePosTrm.forward,
                _playerAttackCompo.FirePosTrm.position, data);
        }
    }
}