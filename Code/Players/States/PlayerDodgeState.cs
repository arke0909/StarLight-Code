using Code.Animators;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerDodgeState : PlayerState
    {
        private int DODGEHASH;
        private EntityAnimationTrigger _triggerCompo;

        public PlayerDodgeState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _triggerCompo = entity.GetCompo<EntityAnimationTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _triggerCompo.OnAnimationEnd += AnimationEndTrigger;
            DODGEHASH = _renderer.GetHash("ISRIGHT");
            _player.isDodging = true;

            float moveX = _player.PlayerInput.InputVector.x;
            bool isRight = moveX > 0;

            if (moveX == 0)
            {
                int isTrue = Random.Range(0, 2);
                isRight = isTrue == 1;
            }

            _renderer.SetParam(DODGEHASH, isRight);
            _player.SetDodge(true);
            _moveCompo.SetMoveSpeedMultiplier(1.5f);
        }

        public override void Update()
        {
            if (_isTriggerCall)
            {
                _player.StateChange("IDLE");
            }
            
            Vector2 moveDir = _player.PlayerInput.InputVector;
            _moveCompo.SetMoveDirection(moveDir);
        }

        public override void Exit()
        {
            _player.SetDodge(false);
            _player.isDodging = false;
            _moveCompo.SetMoveSpeedMultiplier(1);
            _triggerCompo.OnAnimationEnd -= AnimationEndTrigger;
            base.Exit();
        }
    }
}
