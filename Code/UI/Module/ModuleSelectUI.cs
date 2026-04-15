using System;
using System.Reflection;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Modules;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Module
{
    public class ModuleSelectUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private Image icon;
        [SerializeField] private ModuleEquipmentPart modulePart;

        [field: SerializeField] public ModuleDataSO CurrentDataSO { get; private set; }
        public ModuleDataSO SelectedDataSO { get; private set; }

        public UnityEvent OnClick;
        
        private void Awake()
        {
            uiChannel.AddListener<SettingModuleEvent>(HandleSettingModule);
            uiChannel.AddListener<GenerateModuleUIEvent>(HandleGenerateModule);

            if (PlayerPrefs.HasKey("ModuleSetData"))
            {
                string dataJson = PlayerPrefs.GetString("ModuleSetData");

                ModuleSetData moduleSetData = JsonUtility.FromJson<ModuleSetData>(dataJson);
                Type type = typeof(ModuleSetData);
                string module = $"{modulePart}Module";
                FieldInfo moduleData = type.GetField(module, BindingFlags.Instance |
                                                             BindingFlags.Public | BindingFlags.IgnoreCase);
                SelectedDataSO = CurrentDataSO = ScriptableObject.CreateInstance<ModuleDataSO>();
                SelectedDataSO.data = CurrentDataSO.data = moduleData.GetValue(moduleSetData) as ModuleData;
                GenerateSelectedUI(CurrentDataSO);
            }
            else
            {
                SelectedDataSO = CurrentDataSO;
            }
        }


        private void OnDestroy()
        {
            uiChannel.RemoveListener<SettingModuleEvent>(HandleSettingModule);
            uiChannel.RemoveListener<GenerateModuleUIEvent>(HandleGenerateModule);
        }

        private void HandleSettingModule(SettingModuleEvent evt)
        {
            if (evt.modulePart == modulePart)
            {
                SelectedDataSO = CurrentDataSO = evt.moduleData;
                GenerateSelectedUI(CurrentDataSO);
            }
        }

        private void HandleGenerateModule(GenerateModuleUIEvent evt)
        {
            if(evt.moduleData.data.equipmentPart == modulePart)
                CurrentDataSO = evt.moduleData;
        }


        private void GenerateSelectedUI(ModuleDataSO moduleData)
        {
            Sprite iconResource = Resources.Load<Sprite>(moduleData.data.iconPath);            
            icon.sprite = iconResource;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnClick?.Invoke();
                
                ModuleSelectUIClickEvent evt = GameEvents.ModuleSelectUIClickEvent;
                evt.moduleData = CurrentDataSO;
                evt.modulePart = modulePart;
                uiChannel.RaiseEvent(evt);
            }
        }
    }
}