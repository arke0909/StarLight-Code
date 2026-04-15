using System;
using Code.Players;
using UnityEngine;

namespace Code.Modules
{
    public abstract class Module : MonoBehaviour
    {
        [SerializeField] private ModuleDataSO moduleData;
        [field: SerializeField] public string ModuleName { get; private set; }
        [field: SerializeField] public float NeedEnergy { get; private set; }

        protected ModuleController _moduleController;
        protected Player _player;

        
        
        public virtual void InitModule(Player player ,ModuleController moduleController)
        {
            _player = player;
            _moduleController = moduleController;
        }

        public virtual bool CanUse()
        {
            return true;
        }
        
        public abstract void UseModule();

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (moduleData != null)
            {
                ModuleName = moduleData.data.moduleName;
                NeedEnergy = moduleData.data.needEnergy;
            }
        }
#endif
    }
}