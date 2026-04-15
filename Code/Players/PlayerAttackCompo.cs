using System;
using Code.Combat;
using Code.Combat.Bullets;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Core.StatSystem;
using Code.Entities;
using EL.Dependencies;
using EL.ObjectPool.Runtime;
using UnityEngine;

namespace Code.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private GameEventChannelSO gameEventChannelSO;
        [SerializeField] private Transform firePosTrm;
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private PoolingItemSO bulletPoolItemSO;
        [SerializeField] private StatSO attackStat;
        [SerializeField] private StatSO attackSpeedStat;

        [Inject] private PoolManagerMono _poolManager;
        private float _currentTime = 0f;
        private float _originFireRate;
        private float _attackSpeed;
        private bool _canFire = false;
        private Player _player;
        private DamageCompo _damageCompo;
        private EntityStat _statCompo;
        
        private readonly string _playerAtkKey = "PlayerAtk";
        
        public Transform FirePosTrm => firePosTrm;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.OnActiveEvent.AddListener(SetCanFire);
            
            _damageCompo = entity.GetCompo<DamageCompo>();
            _statCompo = entity.GetCompo<EntityStat>();
            
            gameEventChannelSO.AddListener<StageClearEvent>(HandleStageClear);
        }

        private void OnDestroy()
        {
            attackStat.ClearAllModifier();
            attackSpeedStat.ClearAllModifier();
        }

        private void OnApplicationQuit()
        {
            DeleteData();
        }

        private void SetCanFire(bool isActive)
        => _canFire = isActive;

        public void AfterInit()
        {
            StatSO targetAspStat = _statCompo.GetStat(attackSpeedStat);
            StatSO targetAtkStat = _statCompo.GetStat(attackStat); 
            _attackSpeed = targetAspStat.Value;
            _originFireRate = fireRate;
            fireRate /= _attackSpeed;
            targetAspStat.OnValueChange += HandleAttackSpeedChange;
            
            if (PlayerPrefs.HasKey(_playerAtkKey))
            {
                targetAtkStat.AddModifier(_playerAtkKey ,_player.GetPlayerData(_playerAtkKey));
            }
        }
        
        private void HandleStageClear(StageClearEvent evt)
        {
            StatSO targetAtkStat = _statCompo.GetStat(attackStat); 
            if(!evt.isLastStage)
            {
                _player.SetPlayerData(Mathf.Max(targetAtkStat.Value - targetAtkStat.BaseValue, 1), _playerAtkKey);
            }
        }
        
        public void DeleteData()
        {
            _player.DeletePlayerData(_playerAtkKey);
        }

        private void HandleAttackSpeedChange(StatSO stat, float current, float previous)
        {
            _attackSpeed = current;
            fireRate = _originFireRate / _attackSpeed;
        }

        private void Update()
        {
            if (!_canFire) return;
                
            _currentTime += Time.deltaTime;
            if (fireRate <= _currentTime && !_player.isDodging)
            {
                _currentTime = 0;
                Bullet bullet = _poolManager.Pop<Bullet>(bulletPoolItemSO);
                DamageData data = _damageCompo.CalculateDamage(attackStat);
                bullet.InitBullet(firePosTrm.forward, firePosTrm.position, data);
            }
        }
    }
}