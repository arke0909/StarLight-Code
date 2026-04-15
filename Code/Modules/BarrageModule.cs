using System;
using System.Collections;
using Code.Combat;
using Code.Combat.Bullets;
using Code.Core.StatSystem;
using Code.Players;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Modules
{
    public class BarrageModule : Module
    {
        [SerializeField] private Transform[] firePos;
        [SerializeField] private int bulletCnt = 3;
        [SerializeField] private float delay = 0.1f;
        [SerializeField] private PoolingItemSO barrageBulletItemSO;
        [SerializeField] private float damageMultiplier = 10f;
        [SerializeField] private StatSO attackStat;
        [Inject] private PoolManagerMono _poolManager;
        private DamageCompo _damageCompo;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            _damageCompo = player.GetCompo<DamageCompo>();
        }
        
        public override void UseModule()
        {
            StartCoroutine(UseModuleCoroutine());
        }

        private IEnumerator UseModuleCoroutine()
        {
            _moduleController.isUsing = true;
            for (int i = 0; i < bulletCnt; i++)
            {
                foreach (var pos in firePos)
                {
                    DamageData data = _damageCompo.CalculateDamage(attackStat, damageMultiplier);
                    Bullet barrageBullet = _poolManager.Pop<Bullet>(barrageBulletItemSO);
                    barrageBullet.InitBullet(pos.forward,
                        pos.position, data);
                }

                yield return new WaitForSeconds(delay);
            }
            _moduleController.isUsing = false;
        }

      
    }
}