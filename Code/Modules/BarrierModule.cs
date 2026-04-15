using System;
using Code.Players;
using DG.Tweening;
using UnityEngine;

namespace Code.Modules
{
    public class BarrierModule : Module
    {
        [SerializeField] private float duration;
        [SerializeField] private GameObject barrierObject;

        private Tween _barrierTween;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            barrierObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if(_barrierTween != null)
                _barrierTween.Kill();
        }

        public override void UseModule()
        {
            _moduleController.isUsing = true;
            _player.SetDodge(true);
            barrierObject.SetActive(true);
            
            _barrierTween = DOVirtual.DelayedCall(duration, () =>
            {
                _moduleController.isUsing = false;
                _player.SetDodge(false);
                barrierObject.SetActive(false);
            });
        }
    }
}