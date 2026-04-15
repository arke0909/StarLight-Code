using Code.Modules;
using UnityEngine;

namespace Code.UI.Module
{
    public class ModuleSettingUI : MajorUI
    {
        [SerializeField] private ModuleSelectUI frontModuleSelect;
        [SerializeField] private ModuleSelectUI centralModuleSelect;
        [SerializeField] private ModuleSelectUI rearModuleSelect;

        public void SetModuleDataToJson()
        {
            ModuleSetData data = new ModuleSetData
            {
                frontModule = frontModuleSelect.SelectedDataSO.data,
                centralModule = centralModuleSelect.SelectedDataSO.data,
                rearModule = rearModuleSelect.SelectedDataSO.data
            };
            
            string dataJson = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString("ModuleSetData", dataJson);
        }

        [ContextMenu("DeleteData")]
        private void DeleteData()
        {
            PlayerPrefs.DeleteKey("ModuleSetData");
        }
    }
}