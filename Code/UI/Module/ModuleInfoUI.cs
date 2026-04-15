using System.Collections.Generic;
using System.Linq;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Module
{
    public class ModuleInfoUI : MajorUI
    {
        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private ModuleDataListSO moduleDataList;
        [SerializeField] private TextMeshProUGUI moduleName;
        [SerializeField] private TextMeshProUGUI moduleEnergy;
        [SerializeField] private TextMeshProUGUI moduleDescription;
        [SerializeField] private TextMeshProUGUI equipBtnText;
        [SerializeField] private RectTransform moduleItemTrm;
        [SerializeField] private Image icon;
        [SerializeField] private SelectionUI selectionUI;

        private ModuleItemUI[] _moduleItems;
        private Dictionary<ModuleEquipmentPart, int> _moduleEquipPartIdx = new Dictionary<ModuleEquipmentPart, int>();
        private ModuleEquipmentPart _openingPart;
        private ModuleDataSO _currentModuleData;

        protected override void Awake()
        {
            base.Awake();
            uiChannel.AddListener<ModuleSelectUIClickEvent>(HandleModuleSelect);
            uiChannel.AddListener<GenerateModuleUIEvent>(HandleGenerateModule);
            _moduleItems = moduleItemTrm.GetComponentsInChildren<ModuleItemUI>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            uiChannel.RemoveListener<ModuleSelectUIClickEvent>(HandleModuleSelect);
            uiChannel.RemoveListener<GenerateModuleUIEvent>(HandleGenerateModule);
        }

        private void HandleModuleSelect(ModuleSelectUIClickEvent evt)
        {
            ControlMenu();

            bool dataExist = evt.moduleData.data.equipmentPart != ModuleEquipmentPart.None;
            _openingPart = evt.modulePart;

            List<ModuleDataSO> moduleList = moduleDataList.moduleData
                .Where(moduleDataSo => moduleDataSo.data.equipmentPart == _openingPart ||
                                       moduleDataSo.data.equipmentPart == ModuleEquipmentPart.None).ToList();

            int idx = dataExist
                ? moduleList.FindIndex(moduleData => evt.moduleData.data.moduleName == moduleData.data.moduleName)
                : _moduleEquipPartIdx.GetValueOrDefault(_openingPart);

            for (int i = 0; i < moduleList.Count; i++)
            {
                _moduleItems[i].Initialize(i, moduleList[i], i == idx,
                    selectionUI.MoveAnchorPosition);
            }

            for (int i = moduleList.Count; i < _moduleItems.Length; i++)
            {
                _moduleItems[i].Initialize(i, null);
            }

            _currentModuleData = dataExist ? moduleList[idx] : evt.moduleData;

            GenerateInfo(_currentModuleData);
        }

        private void HandleGenerateModule(GenerateModuleUIEvent evt)
        {
            _moduleEquipPartIdx[_openingPart] = evt.idx;
            _currentModuleData = evt.moduleData;
            selectionUI.MoveAnchorPosition(evt.anchorPosition, true);
            GenerateInfo(_currentModuleData);
        }

        private void GenerateInfo(ModuleDataSO moduleData)
        {
            moduleName.text = moduleData.data.displayName;
            moduleEnergy.text = $"필요 에너지: {moduleData.data.needEnergy}";
            moduleDescription.text = moduleData.data.description;
            Sprite iconResource = Resources.Load<Sprite>(moduleData.data.iconPath);
            icon.sprite = iconResource;
        }

        public void OnEquip()
        {
            ControlMenu();

            SettingModuleEvent evt = GameEvents.SettingModuleEvent;
            evt.moduleData = _currentModuleData;
            evt.modulePart = _openingPart;
            uiChannel.RaiseEvent(evt);
        }
    }
}