using Code.Animators;
using Code.Entities;
using Code.Entities.FSM;
using Code.Modules;

namespace Code.Players.States
{
    public class PlayerState : EntityState
    {
        protected EntityMover _moveCompo;
        protected ModuleController _moduleController;
        protected Player _player;

        protected PlayerState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _moveCompo = entity.GetCompo<EntityMover>();
            _moduleController = entity.GetCompo<ModuleController>();
        }
    }
}