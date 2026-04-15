using System;
using Code.Combat;
using Code.Core.StatSystem;
using Code.Entities;
using Code.Players;
using UnityEngine;

namespace Code.Modules
{
    public class OverloadModule : Module
    {
        [Range(0.1f, 0.9f),SerializeField] private float hpDecreasePercent = 0.4f;
        [SerializeField] private float attackIncrease = 4f;
        [SerializeField] private StatSO attackSO;

        private EntityStat _statCompo;
        private PlayerHealth _playerHealth;
        private EntityVFX _entityVFX;
        private int _buffIdx = 0;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            _statCompo = player.GetCompo<EntityStat>();
            _playerHealth = player.GetCompo<PlayerHealth>();
            _entityVFX = player.GetCompo<EntityVFX>();
        }

        private void OnDestroy()
        {
            StatSO targetSO = _statCompo.GetStat(attackSO);
            targetSO.ClearAllModifier();
        }

        public override bool CanUse()
        {
            return _playerHealth.CurrentHealth > 1;
        }

        public override void UseModule()
        {
            _entityVFX.PlayVfx("Overload", _player.transform.position, Quaternion.identity);
            _playerHealth.DecreaseCurrentHp(_playerHealth.MaxHealth * hpDecreasePercent, true);
            StatSO targetSO = _statCompo.GetStat(attackSO);
            targetSO.AddModifier(_buffIdx++,attackIncrease);
        }
    }
}