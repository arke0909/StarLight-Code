using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Entities;
using Code.Players;
using UnityEngine;

namespace Code.Modules
{
    [Serializable]
    public struct ModuleSetData
    {
        public ModuleData frontModule;
        public ModuleData centralModule;
        public ModuleData rearModule;
    }

    public class ModuleController : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private GameEventChannelSO gameChannel;
        [SerializeField] private float maxEnergy = 100f;

        private Dictionary<string, Module> _moduleDictionary = new Dictionary<string, Module>();
        private Module _frontModule, _centralModule, _rearModule;
        private Player _player;

        private readonly string _playerEnergyKey = "PlayerEnergy";

        [HideInInspector] public bool isUsing = false;
        public float Energy { get; private set; } = 0;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            playerChannel.AddListener<GetEnergyEvent>(HandleGetEnergy);
            gameChannel.AddListener<StageClearEvent>(HandleStageClear);

            GetComponentsInChildren<Module>().ToList()
                .ForEach(module =>
                {
                    module.InitModule(_player, this);
                    _moduleDictionary.Add(module.ModuleName, module);
                });

            SetModule();
        }

        public void AfterInit()
        {
            if (PlayerPrefs.HasKey(_playerEnergyKey))
            {
                Energy = _player.GetPlayerData(_playerEnergyKey);
            }
        }

        private void Start()
        {
            GenerateEnergyBar();
        }

        private void OnDestroy()
        {
            playerChannel.RemoveListener<GetEnergyEvent>(HandleGetEnergy);
        }

        private void OnApplicationQuit()
        {
            DeleteData();
        }

        public void DeleteData()
        {
            _player.DeletePlayerData(_playerEnergyKey);
        }

        public void AttemptUseFrontModule()
            => UseModule(_frontModule);

        public void AttemptUseCentralModule()
            => UseModule(_centralModule);

        public void AttemptUseRearModule()
            => UseModule(_rearModule);

        private void UseModule(Module module)
        {
            if (module == null) return;

            if (module.NeedEnergy <= Energy && !isUsing && module.CanUse())
            {
                Energy -= module.NeedEnergy;
                module.UseModule();
                GenerateEnergyBar();
            }
        }

        private void HandleGetEnergy(GetEnergyEvent evt)
        {
            if (isUsing) return;

            Energy = RoundToDecimalPlace(Mathf.Min(Energy + evt.energy, maxEnergy), 2);
            GenerateEnergyBar();
        }

        private void HandleStageClear(StageClearEvent evt)
        {
            if (!evt.isLastStage)
            {
                _player.SetPlayerData(Energy, _playerEnergyKey);
            }
        }

        float RoundToDecimalPlace(float value, int decimalPlaces)
        {
            float multiplier = Mathf.Pow(10, decimalPlaces);
            return Mathf.Round(value * multiplier) / multiplier;
        }


        private void SetModule()
        {
            if (!PlayerPrefs.HasKey("ModuleSetData")) return;

            string dataJson = PlayerPrefs.GetString("ModuleSetData");
            ModuleSetData moduleSetData = JsonUtility.FromJson<ModuleSetData>(dataJson);

            _frontModule = GetModule(moduleSetData.frontModule);
            _centralModule = GetModule(moduleSetData.centralModule);
            _rearModule = GetModule(moduleSetData.rearModule);
        }

        private Module GetModule(ModuleData moduleData)
        {
            if (!string.IsNullOrEmpty(moduleData.moduleName))
                return _moduleDictionary.GetValueOrDefault(moduleData.moduleName);
            return null;
        }

        private void GenerateEnergyBar()
        {
            EnergyChangeEvent changeEvt = GameEvents.EnergyChangeEvent;
            changeEvt.maxEnergy = maxEnergy;
            changeEvt.currentEnergy = Energy;
            playerChannel.RaiseEvent(changeEvt);
        }
    }
}