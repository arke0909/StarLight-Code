using Code.Animators;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _moveCompo.StopImmediately();
            _player.PlayerInput.OnSpaceEvent += HandleDodgeEvent;
            _player.PlayerInput.OnFrontModuleEvent += HandleFrontModuleEvent;
            _player.PlayerInput.OnCentralModuleEvent += HandleCentralModuleEvent;
            _player.PlayerInput.OnRearModuleEvent += HandleRearModuleEvent;
        }


        public override void Update()
        {
            if(_player.PlayerInput.InputVector.magnitude > 0.1f)
                _player.StateChange("MOVE");
        }

        public override void Exit()
        {
            _player.PlayerInput.OnSpaceEvent -= HandleDodgeEvent;
            _player.PlayerInput.OnFrontModuleEvent -= HandleFrontModuleEvent;
            _player.PlayerInput.OnCentralModuleEvent -= HandleCentralModuleEvent;
            _player.PlayerInput.OnRearModuleEvent -= HandleRearModuleEvent;
            base.Exit();
        }

        private void HandleDodgeEvent()
        {
            if (!_player.isDodging)
                _player.StateChange("DODGE");
        }
        
        private void HandleFrontModuleEvent()
            => _moduleController.AttemptUseFrontModule();
        private void HandleCentralModuleEvent()
            => _moduleController.AttemptUseCentralModule();
        private void HandleRearModuleEvent()
            => _moduleController.AttemptUseRearModule();
    }
}