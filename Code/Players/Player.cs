using System;
using Code.Core.Events;
using Code.Entities;
using Code.Entities.FSM;
using EL.Dependencies;
using UnityEngine;

namespace Code.Players
{
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO stateList;

        private StateMachine _stateMachine;
        
        public int OriginLayer { get; private set; }
        
        [HideInInspector] public bool isDodging = false;

        protected override void Awake()
        {
            base.Awake();
            OnActiveEvent.AddListener(PlayerInput.OnOffInput);
            gameEventChannel.AddListener<EnemyDeadEvent>(HandleEnemyDead);
            _stateMachine = new StateMachine(this, stateList);
            OriginLayer = gameObject.layer;
        }

        private void Start()
        {
            StateChange("IDLE");
        }
        
        protected override void OnDestroy()
        {
            gameEventChannel.RemoveListener<EnemyDeadEvent>(HandleEnemyDead);   
            OnActiveEvent.RemoveAllListeners();
            _stateMachine.CurrentState.Exit();
            base.OnDestroy();
        }

        [Provide]
        public Player ProvidePlayer() => this;


        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
 
        protected override void HandleHit()
        {
        }

        protected override void HandleDead()
        {
            base.HandleDead();
            var evt = GameEvents.PlayerDeadEvent;
            gameEventChannel.RaiseEvent(evt);
            GetCompo<EntityVFX>().PlayVfx("Explosion", transform.position, Quaternion.identity);
            OnActiveEvent?.Invoke(false);
        }
        
        private void HandleEnemyDead(EnemyDeadEvent evt)
        {
            SetDodge(true);
            OnActiveEvent?.Invoke(false);
        }

        public void SetDodge(bool isDodge)
        {
            if (isDodge)
            {
                gameObject.layer = DodgeLayer;
            }
            else
            {
                gameObject.layer = OriginLayer;
            }
        }
        
        public void StateChange(string newState)
           => _stateMachine.ChangeState(newState);

        public void SetPlayerData(float saveValue, string key) => PlayerPrefs.SetFloat(key, saveValue);
        
        public float GetPlayerData(string key) => PlayerPrefs.GetFloat(key);
        
        public void DeletePlayerData(string key) => PlayerPrefs.DeleteKey(key);
    }
}