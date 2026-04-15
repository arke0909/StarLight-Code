using System;
using System.Reflection;
using Code.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Module
{
    public class InGameModuleUI : MonoBehaviour
    {
        [SerializeField] private ModuleEquipmentPart modulePart;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI energyText;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("ModuleSetData"))
            {
                string dataJson = PlayerPrefs.GetString("ModuleSetData");
            
                ModuleSetData moduleSetData = JsonUtility.FromJson<ModuleSetData>(dataJson);
                Type type = typeof(ModuleSetData);
                string module = $"{modulePart}Module";
                FieldInfo moduleData = type.GetField(module, BindingFlags.Instance | 
                                                             BindingFlags.Public | BindingFlags.IgnoreCase);
                ModuleData data = moduleData.GetValue(moduleSetData) as ModuleData;
                Sprite iconResource = Resources.Load<Sprite>(data.iconPath);
                icon.sprite = iconResource;
                energyText.text = data.needEnergy.ToString();
            }
        }
    }
}