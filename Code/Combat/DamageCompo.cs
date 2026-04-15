using Code.Core.StatSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Combat
{
    public class DamageCompo : MonoBehaviour, IEntityComponent
    {
        private EntityStat _statCompo;
        
        public void Initialize(Entity entity)
        {
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public DamageData CalculateDamage(StatSO majorStat, float multiplier = 1)
        {
            DamageData data = new DamageData();
            
            data.damage = _statCompo.GetStat(majorStat).Value * multiplier;
            
            return data;
        }
    }
}