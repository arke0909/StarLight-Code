using System;
using Code.Combat;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Entities;
using UnityEngine;

namespace Code.Players
{
    public class PlayerHealth : EntityHealth
    {
        [SerializeField] private GameEventChannelSO gameChannel;
        private Player _player;
        
        private readonly string _playerHpKey = "PlayerHp"; 

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = _entity as Player;
            gameChannel.AddListener<StageClearEvent>(HandleStageClear);
        }

        public override void AfterInit()
        {
            base.AfterInit();

            if (PlayerPrefs.HasKey(_playerHpKey))
            {
                currentHealth = _player.GetPlayerData(_playerHpKey);
            }
        }

        private void Start()
        {
            RaiseHpChangeEvent();
        }

        private void HandleStageClear(StageClearEvent evt)
        {
            if(!evt.isLastStage)
            {
                _player.SetPlayerData(currentHealth, _playerHpKey);
            }
        }
        
        private void OnApplicationQuit()
        {
            DeleteData();
        }

        public void DeleteData()
        {
            _player.DeletePlayerData(_playerHpKey);
        }
    }
}