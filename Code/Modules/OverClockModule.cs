using System;
using Code.Core.StatSystem;
using Code.Entities;
using Code.Players;
using DG.Tweening;
using UnityEngine;

namespace Code.Modules
{
    public class OverClockModule : Module
    {
        [SerializeField] private float duration = 12f;
        [SerializeField] private float buffValue = 2f;
        [SerializeField] private StatSO attackSpeedSO;
        private EntityStat _statCompo;
        private EntityVFX _entityVFX;
        
        private Tween _buffTween;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            _statCompo = player.GetCompo<EntityStat>();
            _entityVFX = player.GetCompo<EntityVFX>();
        }

        private void OnDestroy()
        {
            if(_buffTween != null)
                _buffTween.Kill();
        }

        public override void UseModule()
        {
            _entityVFX.PlayVfx("Overclock", _player.transform.position, Quaternion.identity);
            StatSO targetSO = _statCompo.GetStat(attackSpeedSO);
            _moduleController.isUsing = true;
            targetSO.AddModifier(this, buffValue);
            _buffTween = DOVirtual.DelayedCall(duration, () =>
            {
                targetSO.RemoveModifier(this);
                _moduleController.isUsing = false;
            });
        }
    }
}