using System;
using UnityEngine;

namespace Code.Modules
{
    public enum ModuleEquipmentPart
    {
        None = -1,
        Front = 0,
        Central = 1,
        Rear = 2
    }
    
    [CreateAssetMenu(fileName = "ModuleDataSO", menuName = "SO/Module/Data", order = 0)]
    public class ModuleDataSO : ScriptableObject
    {
        public ModuleData data;
    }

    [Serializable]
    public class ModuleData
    {
        public ModuleEquipmentPart equipmentPart;
        public string moduleName;
        public string displayName;
        [TextArea] public string description;
        public string iconPath;
        public float needEnergy;
    }
}