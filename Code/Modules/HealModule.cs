using Code.Combat;
using Code.Entities;
using Code.Players;
using UnityEngine;

namespace Code.Modules
{
    public class HealModule : Module
    {
        [SerializeField] private float healValue = 250f;
        
        private PlayerHealth _playerHealth;
        private EntityVFX _entityVFX;
        
        public override void InitModule(Player player, ModuleController moduleController)
        {
            base.InitModule(player, moduleController);
            _playerHealth = player.GetCompo<PlayerHealth>();
            _entityVFX = player.GetCompo<EntityVFX>();
        }

        public override void UseModule()
        {
            _entityVFX.PlayVfx("Heal", _player.transform.position, Quaternion.identity);
            _playerHealth.ApplyHeal(healValue);
        }
    }
}