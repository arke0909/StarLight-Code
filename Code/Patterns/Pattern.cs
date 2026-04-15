using System.Collections.Generic;
using Code.Combat;
using Code.Core.StatSystem;
using Code.Enemies;
using Code.Patterns.PatternDatas;
using Code.Players;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        [field:SerializeField] public string PatternName {get; private set;}
        [SerializeField] protected List<Transform> firePosTrm = new List<Transform>();
        [SerializeField] protected PoolingItemSO bulletItemSO;
        [SerializeField] protected float damageMultiply;

        [Inject] protected PoolManagerMono _poolManager;
        [Inject] protected Player _player;
        protected Enemy _enemy;
        protected DamageCompo _damageCompo;
        protected StatSO _attackStat;
        protected PatternComponent _patternComponent;
        
        public virtual void InitPattern(Enemy enemy,PatternComponent patternComponent, StatSO attackStat)
        {
            _enemy = enemy;
            _damageCompo = enemy.GetCompo<DamageCompo>();
            _attackStat = attackStat;
            _patternComponent = patternComponent;
        }

        public virtual void UsePattern(PatternSO patternSO)
        {
            
        }

        public virtual void StopPattern()
        {
            StopAllCoroutines();
        }
    }
}