using Code.Core.EventSystems;
using Code.Modules;
using UnityEngine;

namespace Code.Core.Events
{
    public static class GameEvents
    {
        public static GetEnergyEvent GetEnergyEvent = new GetEnergyEvent();
        public static EnergyChangeEvent EnergyChangeEvent = new EnergyChangeEvent();
        public static HpChangeEvent HpChangeEvent = new HpChangeEvent();
        public static ModuleSelectUIClickEvent ModuleSelectUIClickEvent = new ModuleSelectUIClickEvent();
        public static GenerateModuleUIEvent GenerateModuleUIEvent = new GenerateModuleUIEvent();
        public static SettingModuleEvent SettingModuleEvent = new SettingModuleEvent();
        public static FadeEvent FadeEvent = new FadeEvent();
        public static StageClearEvent StageClearEvent = new StageClearEvent();
        public static ActiveEvent ActiveEvent = new ActiveEvent();
        public static PlayerDeadEvent PlayerDeadEvent = new PlayerDeadEvent();
        public static EnemyDeadEvent EnemyDeadEvent = new EnemyDeadEvent();
        public static CameraShakeEvent CameraShakeEvent = new CameraShakeEvent();
    }

    public class GetEnergyEvent : GameEvent
    {
        public float energy;
    }
    public class EnergyChangeEvent : GameEvent
    {
        public float maxEnergy;
        public float currentEnergy;
    }

    public class HpChangeEvent : GameEvent
    {
        public uint id;
        public float maxHp;
        public float currentHp;
    }

    public class ModuleSelectUIClickEvent : GameEvent
    {
        public ModuleEquipmentPart modulePart;
        public ModuleDataSO moduleData;
    }
    
    public class GenerateModuleUIEvent : GameEvent
    {
        public int idx;
        public ModuleDataSO moduleData;
        public Vector2 anchorPosition;
    }
    public class SettingModuleEvent : GameEvent
    {
        public ModuleDataSO moduleData;
        public ModuleEquipmentPart modulePart;
    }

    public class FadeEvent : GameEvent
    {
        public bool isFadeIn;
        public string sceneName;
    }
    
    public class ActiveEvent : GameEvent
    {
        public bool isActive;
    }

    public class StageClearEvent : GameEvent
    {
        public bool isLastStage;
    }

    public class PlayerDeadEvent : GameEvent
    { }
    
    public class EnemyDeadEvent : GameEvent
    { }

    public class CameraShakeEvent : GameEvent
    {
        public float shakePower;
    }
}